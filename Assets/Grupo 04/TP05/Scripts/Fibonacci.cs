using System.Collections;
using System.Collections.Generic;

public class Fibonacci
{
    public int GetFibonacciSeries(int n)
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
<<<<<<< Updated upstream
<<<<<<< Updated upstream
}
=======
}
>>>>>>> Stashed changes
=======
}
>>>>>>> Stashed changes
