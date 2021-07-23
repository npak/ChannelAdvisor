﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class WaitDialogWithWork : Form
    {
        private Action work;
        public WaitDialogWithWork()
        {
            InitializeComponent();
        }

        public void ShowWithWork(Action work)
        {            
            this.work = work;
            ShowDialog();
        }

        public static WaitDialogWithWork Current { get; private set; }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            ThreadPool.QueueUserWorkItem(_ =>
            {
                var currentDialog = Current;
                Current = this;
                try
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
                    work();
                }
                catch (Exception ex)
                {
                    this.BeginInvoke((Action)(() => MessageBox.Show(ex.Message, "Ecommerce Updater")));
                }
                finally
                {
                    Done();
                    Current = currentDialog;
                }
            }
                );
        }

        public void ShowMessage(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)delegate { ShowMessage(message); });
                return;
            }
            lMessage.Text = message;           
        }

        public void Done()
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)delegate { Done(); });
                return;
            }
            Close();
        }
    }
}
