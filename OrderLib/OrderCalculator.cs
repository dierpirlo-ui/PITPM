namespace OrderLib
{
    public class OrderCalculator
    {
        public static decimal CalculateOrder(
            decimal amount,
            bool isRegistered,
            bool hasPromoCode,
            bool isFirstOrder,
            int age)
        {
            if (amount <= 0)
                throw new ArgumentException("Некорректная сумма заказа");

            decimal discount = 0;

            if (isRegistered && amount >= 5000)
                discount += 10;

            if (hasPromoCode && (amount >= 3000 || isFirstOrder))
                discount += 5;

            if (age >= 60 || age <= 18)
                discount += 5;

            if (discount > 15)
                discount = 15;

            return amount * (1 - discount / 100);
        }

    }
}
