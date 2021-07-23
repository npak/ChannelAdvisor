using System;
using System.Collections.Generic;
using System.Text;

using FileHelpers;

namespace ChannelAdvisor
{
    public static class SunpentownExporter
    {
        public static void ExportSunpentownData(string aFileName, List<SunpentownExportRow> aList)
        {
            FileHelperEngine<SunpentownExportRow> lEngine = new FileHelperEngine<SunpentownExportRow>();
            lEngine.WriteFile(aFileName, aList);
        }
    }
}
