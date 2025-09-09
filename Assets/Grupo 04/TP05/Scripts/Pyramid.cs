using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Pyramid
{
    char x = 'x';
    char space = ' ';  

    public string Create(int num)
    {
        int quantityX = 1;
        int quantitySpace = num - quantityX;

        string pyramidStr = "";

        for (int i = 0; i < num; i++) 
        {
            string lineX = new string(x, quantityX);
            string lineSpace = new string(space, quantitySpace);

            pyramidStr += lineSpace + lineX + "\n";

            quantityX += 2;
            quantitySpace--;

        }

        
        return pyramidStr;
    }
}
