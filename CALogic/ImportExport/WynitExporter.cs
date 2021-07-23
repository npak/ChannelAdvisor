using System;
using System.Collections.Generic;
using System.Text;

using FileHelpers;

namespace ChannelAdvisor
{
    public static class WynitExporter
    {
        /// <summary>
        /// Exports Wynit inventary data to tab delimeted text file
        /// </summary>
        /// <param name="aList">List of rows for exporting</param>
        /// <param name="aFileName">Output file name</param>
        public static void ExportWynitDataToTextFile(List<WynitExportRow> aList, string aFileName)
        {
            FileHelperEngine lEngine = new FileHelperEngine(typeof(WynitExportRow));
            lEngine.WriteFile(aFileName, aList);
        }
    }
}
