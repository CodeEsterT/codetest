using System;
using Xunit;

namespace PromoEngine.Tests
{
    public class PromotionEngineTests 
    {
        [Fact]
        public void Test1()
        {
            PromotionEngine promotionEngine = new PromotionEngine();
            
            Assert.False(true, "1 should not be prime");
        }
    }
}
