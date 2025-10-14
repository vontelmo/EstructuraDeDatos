using System.Collections;
using System.Collections.Generic;

public static class Fibonacci
{
    public static int GetFibonacciSeries(int n)
    {
        if (n <= 1)
        {
            return n;
        }
        else
        {
            return GetFibonacciSeries(n - 1) + GetFibonacciSeries(n - 2);
        }
    }
}
