using OrderLib;
using Xunit;

namespace OrderLIb.Tests
{
    public class OrderCalculatorTests
    {
        // 5.1
        [Fact]
        public void ST_01_NegativeAmount_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                OrderCalculator.CalculateOrder(-1m, false, false, false, 30));
        }

        [Fact]
        public void ST_02_AllConditionsTrue_Returns4250()
        {
            var expected = 4250m;
            var result = OrderCalculator.CalculateOrder(5000m, true, true, true, 70);
            Assert.Equal(expected, result); 
        }

        // 5.2
        [Fact]
        public void C_01_AmountZero_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                OrderCalculator.CalculateOrder(0, false, false, false, 30));
        }

        [Fact]
        public void C_02_AllConditionsTrue_Returns8500()
        {
            var expected = 8500m;
            var result = OrderCalculator.CalculateOrder(10000, true, true, true, 70);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void C_03_PromoAndUnder18_Returns2700()
        {
            var expected = 2700m;
            var result = OrderCalculator.CalculateOrder(3000, false, true, false, 18);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void C_04_FirstOrderNoDiscount_Returns4000()
        {
            decimal expected = 4000m;
            decimal result = OrderCalculator.CalculateOrder(4000, false, false, true, 30);
            Assert.Equal(expected, result);
        }

        // 5.3 
        [Theory]
        [InlineData(6000, true, 5400)]
        [InlineData(1000, true, 1000)]
        [InlineData(6000, false, 6000)]
        [InlineData(1000, false, 1000)]
        public void D2_TruthTable(double amount, bool isRegistered, double expected)
        {
            var result = OrderCalculator.CalculateOrder((decimal)amount, isRegistered, false, false, 30);
            Assert.Equal((decimal)expected, result);
        }
 
        [Theory]
        [InlineData(6000, true, true, true, 5700)]
        [InlineData(6000, true, true, false, 5700)]
        [InlineData(2000, true, false, true, 1900)]
        [InlineData(2000, true, false, false, 2000)]
        [InlineData(6000, false, true, true, 6000)]
        [InlineData(6000, false, true, false, 6000)]
        [InlineData(2000, false, false, true, 2000)]
        [InlineData(2000, false, false, false, 2000)]
        public void D3_TruthTable(double amount, bool hasPromoCode, bool amountGreaterThanOrEqual3000,
            bool isFirstOrder, double expected)
        {
            var result = OrderCalculator.CalculateOrder((decimal)amount, false, hasPromoCode, isFirstOrder, 30);
            Assert.Equal((decimal)expected, result);
        }
 
        [Theory]
        [InlineData(65, 950)]
        [InlineData(10, 950)]
        [InlineData(30, 1000)]
        public void D4_TruthTable(int age, double expected)
        {
            var result = OrderCalculator.CalculateOrder(1000m, false, false, false, age);
            Assert.Equal((decimal)expected, result);
        }
    }
}
