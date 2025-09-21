using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Pyramid
{
    char x = 'x';
    char space = ' ';  

    public string Create(int height)
    {
        int quantityX = 1;
        int quantitySpace = height - quantityX;

        string pyramidStr = "";

        for (int i = 0; i < height; i++) 
        {
            string lineX = new string(x, quantityX);
            string lineSpace = new string(space, quantitySpace);

            pyramidStr += lineSpace + lineX + "\n";

            quantityX += 2;
            quantitySpace--;

        }

        
        return pyramidStr;
    }

    public string CreateRecursive(int maxHeight, int currentHeight = 1)
    {
        //Caso Base 
        if (currentHeight > maxHeight)
            return "";

        //Paso Recursivo
        string spaces = new string(' ', maxHeight - currentHeight);
        string line = new string('X', currentHeight * 2 - 1);

        //completar line con los espacios y X correspondientes
        return spaces + line + "\n"+ CreateRecursive(maxHeight, currentHeight + 1);

    }
}

///5 -> 1, 3, 5, 7, 9
///    X       (4) = 1
///   XXX      (3) = 3
///  XXXXX     (2) = 5
/// XXXXXXX    (1) = 7
///XXXXXXXXX   (0) = 9