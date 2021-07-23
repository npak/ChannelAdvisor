using System;
using System.Collections.Generic;
using System.Text;
using Sgml;
using System.Xml;
using System.IO;
using FileHelpers;
using System.Windows.Forms;
using System.Data;
using System.ComponentModel;

namespace ChannelAdvisor
{
    public static class CAUtil
    {
        /// <summary>
        /// Method to create XML document
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static XmlDocument CreateXMLDocFromHTML(string html)
        {
            Sgml.SgmlReader sgmlReader = new SgmlReader();
            sgmlReader.DocType = "HTML";
            StringReader s = new StringReader(html);

            sgmlReader.InputStream = s;

            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.XmlResolver = null;
            doc.Load(sgmlReader);

            return doc;
        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool ExportDuplicateSKUs(string fileName, DuplicateSKU[] duplicateSKUArray)
        {
            FileHelperEngine engine = new FileHelperEngine(typeof(DuplicateSKU));
            engine.WriteFile(fileName, duplicateSKUArray);

            return true;
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static DuplicateSKU[] ImportDuplicateSKUs(string fileName)
        {
            FileHelperEngine engine = new FileHelperEngine(typeof(DuplicateSKU));
            DuplicateSKU[] duplicateSKUArray = engine.ReadFile(fileName) as DuplicateSKU[]; 

            return duplicateSKUArray;
        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string GetSaveFileDialogFileName(string extension, string title)
        {
            string fileName = "";

            // Create new SaveFileDialog object
            SaveFileDialog DialogSave = new SaveFileDialog();

            // Default file extension
            DialogSave.DefaultExt = "csv";

            // Available file extensions
            DialogSave.Filter = extension;//"Text file (*.txt)|*.txt|XML file (*.xml)|*.xml|All files (*.*)|*.*";

            // Adds a extension if the user does not
            DialogSave.AddExtension = true;

            // Restores the selected directory, next time
            DialogSave.RestoreDirectory = true;

            // Dialog title
            DialogSave.Title = title; //;

            // Startup directory
            DialogSave.InitialDirectory = @"C:/";

            // Show the dialog and process the result
            if (DialogSave.ShowDialog() == DialogResult.OK)
            {
                fileName = DialogSave.FileName;
            }
            DialogSave.Dispose();
            DialogSave = null;

            return fileName;
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string GetOpenFileDialogFileName(string extension, string title)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = extension; //txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.InitialDirectory = @"C:/";
            dialog.Title = title; 
            return (dialog.ShowDialog() == DialogResult.OK)
               ? dialog.FileName : null;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public static string SaveXMLAsVendorFile(XmlDocument xmlDoc, int vendorID)
        {
            //Get current time as fileName
            string fileName = DateTime.Now.ToString().Replace("/","-").Replace(":",".") + ".xml";

            //Get Vendor folder
            string vendorFolder = CAUtil.GetVendorFolder(vendorID); //@"C:\";//new DAL().GetVendorFolder();

            if (vendorFolder != null)
            {
                vendorFolder += SettingsConstant.VendorFile_Folder_Name + "\\";
                if (!Directory.Exists(vendorFolder)) Directory.CreateDirectory(vendorFolder);

                //Save file
                xmlDoc.Save(vendorFolder + fileName);

                System.Diagnostics.Debug.WriteLine("Vendor file saved for Vendor - " + vendorID.ToString() + " at location " + vendorFolder + fileName);

                return fileName;
            }
            else
            {
                return null;
            }
            
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="excelPath"></param>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public static string SaveExcelFileAsVendorFile(string excelPath, int vendorID)
        {
            // get the input file extention
            var extention = excelPath.Substring(excelPath.Length - 4);
            if (extention != "")
                extention = ".xls";
            //Get current time as fileName
            string fileName = DateTime.Now.ToString().Replace("/", "-").Replace(":", ".") + extention;

            //Get Vendor folder
            string vendorFolder = CAUtil.GetVendorFolder(vendorID);

            if (vendorFolder != null)
            {
                vendorFolder += SettingsConstant.VendorFile_Folder_Name + "\\";
                if (!Directory.Exists(vendorFolder)) Directory.CreateDirectory(vendorFolder);

                //Save file
                File.Copy(excelPath, vendorFolder + fileName,true);

                return fileName;
            }
            else
            {
                return null;
            }

        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="csvPath"></param>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public static string SaveScvFileAsVendorFile(string csvPath, int vendorID)
        {
            // get the input file extention
            var extention = csvPath.Substring(csvPath.Length - 4);
            if (extention != "")
                extention = ".csv";
            //Get current time as fileName
            string fileName = DateTime.Now.ToString().Replace("/", "-").Replace(":", ".") + extention;

            //Get Vendor folder
            string vendorFolder = CAUtil.GetVendorFolder(vendorID);

            if (vendorFolder != null)
            {
                vendorFolder += SettingsConstant.VendorFile_Folder_Name + "\\";
                if (!Directory.Exists(vendorFolder)) Directory.CreateDirectory(vendorFolder);

                //Save file
                File.Copy(csvPath, vendorFolder + fileName);

                return fileName;
            }
            else
            {
                return null;
            }

        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="excelPath"></param>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public static string SaveMultipleCSVFilesAsVendorFile(List<string> excelFiles, 
                                                                int vendorID)
        {
            //Get current time as fileName
            string tempFileTemplate = DateTime.Now.ToString().Replace("/", "-").Replace(":", ".") + " ({0}).csv";

            //Get Vendor folder
            string vendorFolder = CAUtil.GetVendorFolder(vendorID);

            string tempFileName = "";
            string fileNames = "";

            if (vendorFolder != null)
            {
                vendorFolder += SettingsConstant.VendorFile_Folder_Name + "\\";
                if (!Directory.Exists(vendorFolder)) Directory.CreateDirectory(vendorFolder);

                //loop list and copy file
                for (int x = 0; x < excelFiles.Count; x++)
                {
                    tempFileName = String.Format(tempFileTemplate, (x + 1).ToString());

                    //Save file
                    File.Copy(excelFiles[x], vendorFolder + tempFileName);

                    fileNames += tempFileName + ",";
                }//end for

                fileNames = fileNames.Substring(0, fileNames.Length - 1);

                return fileNames;
            }
            else
            {
                return null;
            }

        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public static string SaveHTMLFileAsVendorFile(string html, int vendorID)
        {
            //Get current time as fileName
            string fileName = DateTime.Now.ToString().Replace("/", "-").Replace(":", ".") + ".html";

            //Get Vendor folder
            string vendorFolder = CAUtil.GetVendorFolder(vendorID);

            if (vendorFolder != null)
            {
                vendorFolder += SettingsConstant.VendorFile_Folder_Name + "\\";
                if (!Directory.Exists(vendorFolder)) Directory.CreateDirectory(vendorFolder);

                //write to file
                StreamWriter file = new StreamWriter(vendorFolder + fileName);
                file.WriteLine(html);
                file.Close();

                return fileName;
            }
            else
            {
                return null;
            }

        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public static string GetVendorFolder(int vendorID)
        {
            string folder = null;

            Vendor vendor = Vendor.Load(vendorID);

            if (vendor != null)
            {
                if (!string.IsNullOrEmpty(vendor.FileArchive))
                {
                    folder = vendor.FileArchive;
                }
            }

            return folder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string SelectFileArchiveFolder()
        {
            string folder = "";

            FolderBrowserDialog browse = new FolderBrowserDialog();

            browse.Description = "Please select a File Archive folder";
            browse.ShowNewFolderButton = true;
            browse.RootFolder = Environment.SpecialFolder.MyComputer;
            browse.SelectedPath = Environment.SpecialFolder.MyComputer.ToString();

            if (browse.ShowDialog() == DialogResult.OK)
            {
                folder = browse.SelectedPath;

                if (folder.Substring(folder.Length - 2, 1) != "\\")
                    folder += "\\";

            }//end if

            return folder;
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vendorID"></param>
        /// <param name="profileID"></param>
        /// <param name="invDTO"></param>
        public static string CreateCAFile(int vendorID, 
                                    int profileID, 
                                    BindingList<Inventory> invDTO)
        {
            System.Diagnostics.Debug.WriteLine("Trying to create CA file for Vendor - "
                                + vendorID.ToString() + " and profile - "
                                + profileID.ToString() + " ...");

            //Get folder and profile name
            DAL dal = new DAL();
            DataTable dt = dal.GetCASaveDetails(vendorID, profileID).Tables[0];

            string caFile = "";

            if(dt.Rows.Count > 0)
            {
                string fileArchive = dt.Rows[0]["FileArchive"] == null ? null : dt.Rows[0]["FileArchive"].ToString();
                string profile = dt.Rows[0]["Profile"] == null ? null : dt.Rows[0]["Profile"].ToString();

                if (!String.IsNullOrEmpty(fileArchive))
                {
                    string folder = fileArchive + SettingsConstant.CAFiles_Folder_Name + "\\";
                    
                    //check if folder exists
                    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                    //Get current time as fileName
                    caFile = DateTime.Now.ToString().Replace("/", "-").Replace(":", ".") + ".csv";

                    CreateCACsvFile(invDTO, folder + caFile);

                }//end if

            }//end if

            return caFile;
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invDTO"></param>
        /// <param name="filePath"></param>
        public static void CreateCACsvFile(BindingList<Inventory> invDTO, string filePath)
        {
            InventoryExportRow[] invDTOFiles = new InventoryExportRow[invDTO.Count +1];

            invDTOFiles[0] = InventoryExportRow.CreateHeader();

            //loop and create ca file object
            for(int x =0; x <invDTO.Count ; x++)
            {
                invDTOFiles[x+1] = InventoryExportRow.CreateInventoryExportRow(invDTO[x]);
            }//end for

            //create file
            FileHelperEngine engine = new FileHelperEngine(typeof(InventoryExportRow));
            engine.WriteFile(filePath, invDTOFiles);

            System.Diagnostics.Debug.WriteLine("Created CA file at location " + filePath);
        }//end method

    }//end class

}//end namespace
