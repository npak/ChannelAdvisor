namespace ChannelAdvisor
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CAServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.CAServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // CAServiceProcessInstaller
            // 
            this.CAServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.CAServiceProcessInstaller.Password = null;
            this.CAServiceProcessInstaller.Username = null;
            // 
            // CAServiceInstaller
            // 
            this.CAServiceInstaller.ServiceName = "CAService2";
            this.CAServiceInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller1_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.CAServiceProcessInstaller,
            this.CAServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller CAServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller CAServiceInstaller;
    }
}