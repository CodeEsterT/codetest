using System;
using Xunit;
using PromoEngine;
using System.Collections.Generic;

namespace PromoEngine.Tests
{
    public class PromotionEngineTests
    {
        Dictionary<string, double> default_prices;

        List<IPromotion> default_promotions;

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

            default_promotions = new List<IPromotion>() {
                new BulkPromotion(new List<string>() {"A", "A", "A"}, 130),
                new BulkPromotion(new List<string>() {"B", "B"}, 45),
                new BulkPromotion(new List<string>() {"C", "D"}, 30),
            };
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


         [Fact]
        public void TestPromotions()
        {
            // Empty cart
            double price = promoEngine.CalculatePrice(default_promotions,
                                        default_prices,
                                        new List<string>() { }
                                        );
            Assert.Equal(0, price);

            // Single item, no promotion applies
            price = promoEngine.CalculatePrice(default_promotions,
                                        this.default_prices,
                                        new List<string>() {"A"}
                                        );
            Assert.Equal(default_prices["A"], price);

            // 1 promotion applies
            price = promoEngine.CalculatePrice(default_promotions,
                                        this.default_prices,
                                        new List<string>() {"B", "B"}
                                        );
            Assert.Equal(45, price);

            // 1 promotion applies plus a standalone item
            price = promoEngine.CalculatePrice(default_promotions,
                                        this.default_prices,
                                        new List<string>() {"B", "B", "A"}
                                        );
            Assert.Equal(45 + default_prices["A"], price);

            // Multiple promotions apply
            price = promoEngine.CalculatePrice(default_promotions,
                                        this.default_prices,
                                        new List<string>() {"A", "A", "A", "C", "D"}
                                        );
            Assert.Equal(130 + 30, price);

            // Promotions with overlap are mutually exlusive
            price = promoEngine.CalculatePrice(new List<IPromotion>(){
                                                    new BulkPromotion(new List<string>() {"A", "A"}, 10),
                                                    new BulkPromotion(new List<string>() {"B", "A"}, 2),
                                                },
                                                this.default_prices,
                                                new List<string>() {"A", "A", "B"}
                                        );
            Assert.Equal(10 + default_prices["B"], price);
        }
    }
}
