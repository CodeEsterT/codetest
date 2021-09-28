using System;
using System.Collections.Generic;

namespace PromoEngine
{
    public interface IPromotion
    {
        public bool IsApplicable(List<string> items);
        public Tuple<List<string>, double> Apply(List<string> cart);
    }

    public class BulkPromotion : IPromotion
    {
        List<string> requirements;
        double price;

        public bool IsApplicable(List<string> items)
        {
            return false;
        }

        public Tuple<List<string>, double> Apply(List<string> cart)
        {
            var ret = new Tuple<List<string>, double>(
                cart, 0
            );
            return ret;  
        }
    }

    public class PromoEngine 
    {
        List<IPromotion> promotions;
        Dictionary<string, double> prices;

//        public PromoEngine(List<IPromotion> promotions, Dictionary<string, double> prices)

        public double CalculatePrice(List<IPromotion> promotions, 
                                    Dictionary<string, double> prices,
                                    List<string> cart)
        {
            return 0;            
        }
    }
}
