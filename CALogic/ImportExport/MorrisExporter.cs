using System;
using System.Collections.Generic;
using System.Text;

using FileHelpers;

namespace ChannelAdvisor
{
    public static class MorrisExporter
    {
        public static void ExportMorrisDataToTextFile(List<MorrisExportRow> lList, string aFileName)
        {
            FileHelperEngine lEngine = new FileHelperEngine(typeof(MorrisExportRow));
            lEngine.WriteFile(aFileName, lList);
        }
    }
}
