using System.Collections;
using System.Collections.Generic;

public static class Palindrom
{
    public static string IsPalindrom(string word)
    {
        word.ToLower();

        if (word.Length <= 1)
        {
            return word + " es un palindromo.";
        }
        else if (word[0] != word[word.Length - 1])
        {
            return word + " no es un palindromo.";
        }
        else
        {
            return IsPalindrom(word.Substring(1, word.Length - 2));
        }
    }
}