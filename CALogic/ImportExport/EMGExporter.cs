using System;
using System.Collections.Generic;
using System.Text;

using FileHelpers;

namespace ChannelAdvisor
{
    public static class EMGExporter
    {
        public static void ExportEMGDataToTextFile(IList<EMGExportRow> lList, string aFileName)
        {
            FileHelperEngine lEngine = new FileHelperEngine(typeof(EMGExportRow));
            lEngine.WriteFile(aFileName, lList);
        }
    }
}
