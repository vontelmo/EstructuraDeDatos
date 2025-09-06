using System.Collections;
using System.Collections.Generic;

public static class Piramid
{
    public static string BuildPiramid(int levels)
    {
        if (levels <= 1)
        {
            return "*";
        }
        else
        {
            return BuildPiramid(levels - 1) + "\n" + new string('*', levels);
        }
    }
}