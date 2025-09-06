using System.Collections;
using System.Collections.Generic;

public class Factorial
{
    public int GetFactorial(int n)
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
<<<<<<< Updated upstream
<<<<<<< Updated upstream
}
=======
}
>>>>>>> Stashed changes
=======
}
>>>>>>> Stashed changes
