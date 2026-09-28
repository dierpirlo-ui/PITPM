namespace FractionLib
{
  public readonly struct Fraction : IEquatable<Fraction>, IComparable<Fraction>
  {
    public int Numerator { get; }
    public int Denominator { get; }

    public Fraction(int numerator, int denominator)
    {
      if (denominator == 0)
        throw new DivideByZeroException("Denominator cannot be zero.");

      int gcd = GCD(Math.Abs(numerator), Math.Abs(denominator));
      numerator /= gcd;
      denominator /= gcd;

      if (denominator < 0)
      {
        numerator *= -1;
        denominator *= -1;
      }

      Numerator = numerator;
      Denominator = denominator;
    }

    public static Fraction operator +(Fraction a, Fraction b) =>
        new(a.Numerator * b.Denominator + b.Numerator * a.Denominator,
            a.Denominator * b.Denominator);

    public static Fraction operator -(Fraction a, Fraction b) =>
        new(a.Numerator * b.Denominator - b.Numerator * a.Denominator,
            a.Denominator * b.Denominator);

    public static Fraction operator *(Fraction a, Fraction b) =>
        new(a.Numerator * b.Numerator, a.Denominator * b.Denominator);

    public static Fraction operator /(Fraction a, Fraction b)
    {
      if (b.Numerator == 0)
        throw new DivideByZeroException("Cannot divide by zero.");

      return new(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
    }

    public double ToDouble() => (double)Numerator / Denominator;
    public int ToInt() => Numerator / Denominator;

    public override string ToString() => $"{Numerator}/{Denominator}";

    public bool Equals(Fraction other) =>
        Numerator == other.Numerator && Denominator == other.Denominator;

    public override bool Equals(object? obj) =>
        obj is Fraction other && Equals(other);


    public override int GetHashCode() => HashCode.Combine(Numerator, Denominator);

    private static int GCD(int a, int b) =>
        b == 0 ? a : GCD(b, a % b);

    public int CompareTo(Fraction other)
    {
      return (this - other).Numerator;
    }
  }
}
