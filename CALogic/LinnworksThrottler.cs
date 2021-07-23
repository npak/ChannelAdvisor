using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using ChannelAdvisor.VendorServices;
using log4net;

namespace ChannelAdvisor
{
    /// <summary>
    /// Provides methods for throttling the API access rate.
    /// </summary>
    public static class LinnworksThrottler
    {
        private const String MUTEX_NAME = @"Global\LinnworksThrottlingMutex";
        private const int AVERAGE_WAIT_TIME = 3000;
        private const int MAX_CALLS_PER_PERIOD = 18;// 20 according to requirements, but actually more than 18 can lead to 403 error
        private const double THROTTLING_PERIOD_SECONDS = 60;
        private const int MIN_DELAY_MSECONDS = 500;
        private const int RAND_DISPERSION = 3;

        private static Mutex _mutex;
        private static bool _mutexTaken = false;
        private static LinkedList<DateTime> _callTimes = new LinkedList<DateTime>();
        private static Object sync = new Object();
        private static Random random = new Random();
        private static String processName;
        private static int threadId;
        public static readonly ILog log = LogManager.GetLogger(typeof(LinnworksService));

        /// <summary>
        /// Waits optimal time for API call according to the rate limit.
        /// </summary>
        /// <remarks>
        /// Call it before each API call.
        /// </remarks>
        public static bool WaitForAllowedApiCall()
        {
            lock (sync)
            {
                try
                {
                    if (_mutex == null)
                    {
                        _mutex = GetOrCreateMutex();
                    }
                    if (_mutex != null)
                    {
                        if (!_mutexTaken)
                        {
                            TakeMutex();
                        }
                        int iterationDelay = MIN_DELAY_MSECONDS;
                        do
                        {
                            Thread.Sleep(iterationDelay);
                            removeOldCalls();

                            iterationDelay = CalculateIterationDelay();

                        } while (_callTimes.Count > MAX_CALLS_PER_PERIOD+1);

                        _callTimes.AddFirst(DateTime.Now);
                    }
                    else
                    {
                        SafetyWait();
                    }
                }
                catch (Exception ex)
                {
                    SafetyWait();
                }
                return true;
            }
        }

        /// <summary>
        /// Stops blocking calls from other threads.
        /// </summary>
        /// <remarks>
        /// It's necessary to call this method immidiately after throttled series of API calls is ended. Use in try-finally block.
        /// </remarks>
        public static void StopThrottling()
        {
            lock (sync)
            {
                do
                {
                    removeOldCalls();
                    SafetyWait();
                } while (_callTimes.Count > 0);

                if (_mutex != null)
                {
                    var curProcessName = Process.GetCurrentProcess().ProcessName;
                    var curThreadId = Thread.CurrentThread.ManagedThreadId;
                    if (processName != curProcessName || curThreadId != threadId)
                        Thread.Sleep(10);
                    log.Debug(String.Format("Releasing Linnworks mutex in process {0}.", curProcessName));
                    try
                    {
                        _mutex.ReleaseMutex();
                    }
                    finally
                    {
                        _mutexTaken = false;
                    }
                }
            }
        }

        private static int CalculateIterationDelay()
        {
            int iterationDelay = MIN_DELAY_MSECONDS;
            if (_callTimes.Count > MAX_CALLS_PER_PERIOD+1)
            {
                var next = _callTimes.Min();
                var current = DateTime.Now;
                int expectation = (int)current.Subtract(next).TotalMilliseconds;
                iterationDelay += expectation;
            }
            return iterationDelay;
        }

        private static void removeOldCalls()
        {
            foreach (var outOfPeriod in
                _callTimes.Where(
                    t => DateTime.Compare(t, DateTime.Now.AddSeconds(-THROTTLING_PERIOD_SECONDS)) < 0).
                    ToList())
            {
                _callTimes.Remove(outOfPeriod);
            }
        }

        private static void TakeMutex()
        {
            bool mutexSuccessfullyTaken = false;
            bool firstRun = true;
            do
            {
                if (firstRun)
                {
                    firstRun = false;
                }
                else
                {
                    SafetyWait();
                }
                mutexSuccessfullyTaken = _mutex.WaitOne(AVERAGE_WAIT_TIME);
            } while (!mutexSuccessfullyTaken);
            _mutexTaken = true;
            processName = Process.GetCurrentProcess().ProcessName;
            threadId = Thread.CurrentThread.ManagedThreadId;
            log.Debug(String.Format("Mutex taken in process: {0}.", processName));
        }

        private static Mutex GetOrCreateMutex()
        {
            Mutex mutex = null;
            try
            {
                mutex = Mutex.OpenExisting(MUTEX_NAME);
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                //the mutex wasn't created yet, need to create one
                /*try
                {
                    string user = Environment.UserDomainName
                                  + "\\"
                                  + Environment.UserName;

                    MutexSecurity mSec = new MutexSecurity();

                    MutexAccessRule rule = new MutexAccessRule(user,
                                                               MutexRights.FullControl,
                                                               AccessControlType.Allow);
                    mSec.AddAccessRule(rule);

                    bool successfull;
                    mutex = new Mutex(false, MUTEX_NAME, out successfull, mSec);
                }
                catch*/
                {
                    mutex = new Mutex(false, MUTEX_NAME);
                }
            }
            catch (UnauthorizedAccessException)
            {
                //Access denied, try to get rights
                /*try
                {
                    mutex = Mutex.OpenExisting(MUTEX_NAME,
                                                MutexRights.ReadPermissions | MutexRights.ChangePermissions);

                    MutexSecurity mSec = mutex.GetAccessControl();
                    string user = Environment.UserDomainName
                                  + "\\"
                                  + Environment.UserName;

                    MutexAccessRule rule = new MutexAccessRule(user,
                                                               MutexRights.Synchronize | MutexRights.Modify,
                                                               AccessControlType.Deny);
                    mSec.RemoveAccessRule(rule);

                    rule = new MutexAccessRule(user,
                                               MutexRights.Synchronize | MutexRights.Modify,
                                               AccessControlType.Allow);
                    mSec.AddAccessRule(rule);

                    mutex.SetAccessControl(mSec);
                    mutex = Mutex.OpenExisting(MUTEX_NAME);
                }
                catch (UnauthorizedAccessException ex)*/
                {
                    //mutex can't be taken
                    //can be logged but no need actually
                    SafetyWait();
                }
            }
            return mutex;
        }

        /// <summary>
        /// Wait in case of necessary wait time is unpredictable.
        /// </summary>
        private static void SafetyWait()
        {
            int length = random.Next(AVERAGE_WAIT_TIME, AVERAGE_WAIT_TIME * RAND_DISPERSION);
            Thread.Sleep(length);
        }

    }
}
