using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelAdvisor
{
    public class VendorServiceFactory
    {
        public static IVendorService GetVendorService(int vendorId)
        {
            switch (vendorId)
            {
                case 1: return new EMGService();
                case 2: return new WynitService();
                case 3: return new PacificCycleService();
                case 4: return new DressUpAmericaService();
                case 5: return new PicnicTimeService();
                case 6: return new SumdexService();
                case 7: return new CWRService();
                case 8: return new HaierService();
                case 9: return new RJTService();
                case 10: return new KwikTekService();
                case 11: return new RocklineService();
                case 12: return new MorrisService();
                case 13: return new PetraService();
                case 14: return new MorrisCompleteService();
                case 15: return new BenchmarkService();
                case 16: return new MorrisNightlyService();
                case 17: return new MorrisChangesService();
                case 18: return new AZService();
                case 19: return new MorrisDailySummaryService();
                case 20: return new GreenSupplyService();
                case 21: return new VikingService();
                case 22: return new NearlyNaturalService();
                case 23: return new MotengService();
                case 25: return new PetraOrderService();
                case 26: return new PetraOrderReformatService();
                case 27: return new SeawideService();
                case 28:return new TWHService();
               // case 29: return new morrisw
                case 53: return new PetGearService();
                case 61: return new OceanStarService();
                default: return new GenericPriceService(vendorId);
            }
        }
    }
}
