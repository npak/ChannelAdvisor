namespace EMGOrderService
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
            this.EMGOrderServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.EMGOrderServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // CAServiceProcessInstaller
            // 
            this.EMGOrderServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.EMGOrderServiceProcessInstaller.Password = null;
            this.EMGOrderServiceProcessInstaller.Username = null;
            // 
            // CAServiceInstaller
            // 
            this.EMGOrderServiceInstaller.ServiceName = "EMGOrderService";
            this.EMGOrderServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.EMGOrderServiceInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller1_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.EMGOrderServiceProcessInstaller,
            this.EMGOrderServiceInstaller});
        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller EMGOrderServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller EMGOrderServiceInstaller;
    }
}