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
            List<string> item_check = new List<string>(items); 

            foreach(string item in requirements)
            {
                if(item_check.Contains(item)) {
                    item_check.Remove(item);
                } else {
                    return false;
                }
            }

            return true;
        }

        public Tuple<List<string>, double> Apply(List<string> cart)
        {
            List<string> new_cart = new List<string>(cart); 
 
            foreach(string item in requirements)
            {
                if(new_cart.Remove(item) == false)
                {
                    throw new KeyNotFoundException($"Cart does not contain promotion item: {item}");
                }
            }            

            return new Tuple<List<string>, double>(
                new_cart, price
            );
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
