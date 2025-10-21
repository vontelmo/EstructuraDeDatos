using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Rendering.CameraUI;


public class TP05Execute : MonoBehaviour
{
    Pyramid pyramid = new Pyramid();
    Factorial factorial = new Factorial();

    [SerializeField] GameObject listSquare;
    [SerializeField] Transform gridLayout;
    [SerializeField] TMP_InputField inputField;
    string input;
    int value;
    string result;


    void Start()
    {
    }

    void DrawResult()
    {
        foreach (Transform child in gridLayout)
        {
            Destroy(child.gameObject);
        }

        GameObject newItem = Instantiate(listSquare, gridLayout);
        TMP_Text childText = newItem.GetComponentInChildren<TMP_Text>();
        if (childText != null)
        {
            childText.text = result;
        }

    }




    public void FactorialButton()
    {
        input = inputField.text;
        int.TryParse(input, out value);

        result = factorial.GetFactorial(value).ToString();
        DrawResult();

    }


    public void FibonacciButton()
    {
        input = inputField.text;
        int.TryParse(input, out value);

        result = Fibonacci.GetFibonacciSeries(value).ToString();

        DrawResult();

    }

    public void PalindromButton()
    {
        input = inputField.text;

        result = input + Palindromo.IsPalindrom(input).ToString();
        DrawResult();
    }

    public void PreviousNumSumButton()
    {
        input = inputField.text;
        int.TryParse(input, out value);

        result = PreviousNumSum.SumAllPreviousNum(value).ToString();

        DrawResult();

    }


    public void PyramidButton()
    {
        input = inputField.text;
        int.TryParse(input, out value);


        result = pyramid.CreateRecursive(value);
        DrawResult();


    }

}
