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

        public BulkPromotion(List<string> requirements, double price)
        {
            this.requirements = requirements;
            this.price = price;
        }

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

        public override string ToString()
        {
            return $"Promotion: {string.Join(',', requirements)} for {price}";
        }
    }

    public class PromoEngine 
    {
        List<IPromotion> promotions;
        Dictionary<string, double> prices;

        public double CalculatePrice(List<IPromotion> promotions, 
                                    Dictionary<string, double> prices,
                                    List<string> cart)
        {
            foreach(IPromotion promotion in promotions)
            {
                if(promotion.IsApplicable(cart))
                {
                    Console.WriteLine($"Promotion applicable: {promotion}");
                    (List<string> new_cart, double sub_total) = promotion.Apply(cart);
                    return sub_total + CalculatePrice(promotions, prices, new_cart);
                }
            }

            // No promotions applicable. Sum up the items individually and return the sum
            double sum = 0;
            foreach(string item in cart)
            {
                sum += prices[item];
            }

            return sum;
        }
    }
}
