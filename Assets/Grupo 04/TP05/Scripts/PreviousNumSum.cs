using System.Collections;
using System.Collections.Generic;

public static class PreviousNumSum
{
    public static int SumAllPreviousNum(int n)
    {
        if (n <= 1)
        {
            return 0;
        }
        else
        {
            return (n - 1) + SumAllPreviousNum(n - 1);
        }
    }
}
