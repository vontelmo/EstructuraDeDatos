using System.Collections;
using System.Collections.Generic;

public static class PreviousNumSum
{
    public static int SumAllPreviousNum(int n)
    {
        if (n <= 1)
        {
            return n;
        }
        else
        {
            return n + SumAllPreviousNum(n - 1); 
        }
    }
}
