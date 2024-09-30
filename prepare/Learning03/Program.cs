using System;
public class Fraction
{
    private int _top;
    private int _bottom;

    // Constructors
    public Fraction()
    {
        _top = 1;
        _bottom = 1;
    }

    public Fraction(int top)
    {
        _top = top;
        _bottom = 1;
    }

    public Fraction(int top, int bottom)
    {
        _top = top;
        _bottom = bottom;
    }

    // Getters and Setters
    public int GetTop()
    {
        return _top;
    }

    public void SetTop(int top)
    {
        _top = top;
    }

    public int GetBottom()
    {
        return _bottom;
    }

    public void SetBottom(int bottom)
    {
        _bottom = bottom;
    }

    // Methods to get string and decimal representation
    public string GetFractionString()
    {
        return $"{_top}/{_bottom}";
    }

    public double GetDecimalValue()
    {
        return (double)_top / _bottom;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Using the first constructor
        Fraction fraction1 = new Fraction();
        Console.WriteLine(fraction1.GetFractionString()); // Output: 1/1
        Console.WriteLine(fraction1.GetDecimalValue());   // Output: 1

        // Using the second constructor
        Fraction fraction2 = new Fraction(5);
        Console.WriteLine(fraction2.GetFractionString()); // Output: 5/1
        Console.WriteLine(fraction2.GetDecimalValue());   // Output: 5

        // Using the third constructor
        Fraction fraction3 = new Fraction(3, 4);
        Console.WriteLine(fraction3.GetFractionString()); // Output: 3/4
        Console.WriteLine(fraction3.GetDecimalValue());   // Output: 0.75

        // Using setters and getters
        fraction3.SetTop(1);
        fraction3.SetBottom(3);
        Console.WriteLine(fraction3.GetFractionString()); // Output: 1/3
        Console.WriteLine(fraction3.GetDecimalValue());   // Output: 0.333...
    }
}