using System.Collections;
using System.Collections.Generic;

public static class Factorial
{
    public static int GetFactorial(int n)
    {
        if (n == 0 || n == 1)
        {
            return 1;
        }
        else
        {
            return n * GetFactorial(n - 1);
        }
    }
}
