using System;
using System.Collections.Generic;
using System.Text;

using FileHelpers;

namespace ChannelAdvisor
{
    public static class CWRExporter
    {
        public static void ExportCWRDataToTextFile(IList<CWRExportRow> lList, string aFileName)
        {
            FileHelperEngine lEngine = new FileHelperEngine(typeof(CWRExportRow));
            lEngine.WriteFile(aFileName, lList);
        }
    }
}
