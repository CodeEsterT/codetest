using System;
using Xunit;
using PromoEngine;
using System.Collections.Generic;

namespace PromoEngine.Tests
{
    public class PromotionEngineTests
    {
        Dictionary<string, double> default_prices;
        PromoEngine promoEngine;

        public PromotionEngineTests()
        {
            default_prices = new Dictionary<string, double>() {
                                            {"A", 50},
                                            {"B", 30},
                                            {"C", 20},
                                            {"D", 15},
                                        };

            promoEngine = new PromoEngine();
        }


        [Fact]
        public void TestNoPromotions()
        {
            // Empty cart
            double price = promoEngine.CalculatePrice(new List<IPromotion>(),
                                        this.default_prices,
                                        new List<string>() { }
                                        );
            Assert.Equal(0, price);

            // Single item
            price = promoEngine.CalculatePrice(new List<IPromotion>(),
                                        this.default_prices,
                                        new List<string>() {"A"}
                                        );
            Assert.Equal(default_prices["A"], price);

            // Same item multiple times
            price = promoEngine.CalculatePrice(new List<IPromotion>(),
                                        this.default_prices,
                                        new List<string>() {"A", "A", "A"}
                                        );
            Assert.Equal(default_prices["A"] * 3, price);

            // Multiple different items
            price = promoEngine.CalculatePrice(new List<IPromotion>(),
                                        this.default_prices,
                                        new List<string>() {"A", "A", "B", "C"}
                                        );
            Assert.Equal(default_prices["A"] * 2 + default_prices["B"] + default_prices["C"], price);

            // Unknown items
            price = promoEngine.CalculatePrice(new List<IPromotion>(),
                                        this.default_prices,
                                        new List<string>() {"C", "Z"}
                                        );
            Assert.Equal(default_prices["C"], price);
        }

        
    }
}
