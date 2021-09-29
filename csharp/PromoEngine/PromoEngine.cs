using System;
using System.Collections.Generic;

namespace PromoEngine
{
    public interface IPromotion
    {
        /// <summary>
        /// Returns true if the promotion applies to the cart. False otherwise.
        /// </summary>
        public bool IsApplicable(List<string> cart);

        /// <summary>
        /// Applies the promotion to the cart and returns a tuple of the updated cart 
        /// and the price.
        /// </summary>
        public Tuple<List<string>, double> Apply(List<string> cart);
    }

    public class BulkPromotion : IPromotion
    {
        /// <summary>
        /// Containes the list of items required to be present in the cart for the
        /// promotion to apply.
        /// </summary>
        List<string> requirements;

        /// <summary>
        /// The price of the promotion 
        /// </summary>
        double price;

        public BulkPromotion(List<string> requirements, double price)
        {
            this.requirements = requirements;
            this.price = price;
        }

        public bool IsApplicable(List<string> items)
        {
            // Iterates over the required items and check that they all are present
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
            // Remove the promotion items from the cart and return it in a new list
            // together with the price
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
    }

    public class PromoEngine 
    {
        /// <summary>
        /// Recursively iterate over the cart and apply promotions when possible.
        /// If no promotions are applicable, sum up the rest of the items and return the price.
        /// </summary>
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
