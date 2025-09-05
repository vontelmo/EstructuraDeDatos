using System.Collections;
using System.Collections.Generic;

public class PreviousNumSum
{
    public int SumAllPreviousNum(int n)
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
