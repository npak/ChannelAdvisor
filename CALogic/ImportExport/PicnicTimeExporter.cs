using System;
using System.Collections.Generic;
using System.Text;

using FileHelpers;

namespace ChannelAdvisor
{
    /// <summary>
    /// Class for exporting Picnic Time data to text file
    /// </summary>
    public static class PicnicTimeExporter
    {
        /// <summary>
        /// Export Picnic Time data to tab-delimited text file
        /// </summary>
        /// <param name="aFileName">Name of file for exporting</param>
        /// <param name="aList">List of rows to export</param>
        public static void ExportPicnicTimeData(string aFileName, List<PicnicTimeExportRow> aList)
        {
            FileHelperEngine<PicnicTimeExportRow> lEngine = new FileHelperEngine<PicnicTimeExportRow>();
            lEngine.WriteFile(aFileName, aList);
        }
    }
}
