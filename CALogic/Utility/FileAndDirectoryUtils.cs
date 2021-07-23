using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ChannelAdvisor
{
    /// <summary>
    /// This class represents utilities for working with files and folders
    /// </summary>
    public static class FileAndDirectoryUtils
    {
        /// <summary>
        /// Get list of files
        /// </summary>
        /// <param name="aDirectoryName">Folder name where files will be searching</param>
        /// <param name="aExtension">Extension of files</param>
        /// <returns></returns>
        public static List<FileInfo> GetFiles(string aDirectoryName, string aExtension)
        {
            if (!Directory.Exists(aDirectoryName))
                return null;
            DirectoryInfo lDirectory = new DirectoryInfo(aDirectoryName);
            List<FileInfo> lResult = new List<FileInfo>(lDirectory.GetFiles("*." + aExtension, SearchOption.TopDirectoryOnly));
            return lResult;
        }

        /// <summary>
        /// Get name of last created file
        /// </summary>
        /// <param name="aDirectoryName">Folder name for searching file</param>
        /// <param name="aExtension">Extension of file</param>
        /// <returns>Name of last created file</returns>
        public static string GetNewestFile(string aDirectoryName, string aExtension)
        {
            List<FileInfo> lFiles = GetFiles(aDirectoryName, aExtension);

            if ((lFiles == null) || (lFiles.Count == 0))
                return null;

            FileInfo lResult = lFiles[0];
            foreach (FileInfo lFile in lFiles)
                if (lFile.CreationTime > lResult.CreationTime)
                    lResult = lFile;

            return lResult.FullName;
        }

        /// <summary>
        /// Validates prefix for file name
        /// </summary>
        /// <param name="aPrefix">File name prefix</param>
        /// <returns>True if prefix is valid, othervise False</returns>
        public static bool ValidateFileNamePrefix(string aPrefix)
        {
            char[] lInvalidChars = new char[] { '\\', '/', '*', '<', '>', '|', '?', ':', '"' };
            if (aPrefix.IndexOfAny(lInvalidChars) > -1)
                return false;

            return true;
        }
    }
}
