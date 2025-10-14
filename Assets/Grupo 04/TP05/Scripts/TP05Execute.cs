using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Rendering;


public class TP05Execute : MonoBehaviour
{
    Pyramid pyramid = new Pyramid();
    Factorial factorial = new Factorial();

    [SerializeField] GameObject listSquare;
    [SerializeField] Transform gridLayout;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] private TMP_Dropdown typeDropdown;

    private Type currentType = typeof(int);

    void Start()
    {
        typeDropdown.onValueChanged.AddListener(OnTypeChanged);
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
            if (childText.text == default) return;


        }

    }



    void OnTypeChanged(int index)
    {
        switch (index)
        {
            case 0: currentType = typeof(int); break;
            case 1: currentType = typeof(float); break;
            case 2: currentType = typeof(string); break;
        }
    }

    public void FactorialButton()
    {
        object input = inputField.text;
        try
        {
            object value = Convert.ChangeType(input, currentType);
            value = factorial.GetFactorial((int)value);
            Debug.Log(value);
            DrawResult();
        }
        catch
        {
            Debug.Log($"El valor '{input}' no es valido para {currentType.Name}");
        }
    }


    public void FibonacciButton()
    {
        object input = inputField.text;
        try
        {
            object value = Convert.ChangeType(input, currentType);
            value = Fibonacci.GetFibonacciSeries((int)value);
            Debug.Log(value);
            DrawResult();
        }
        catch
        {
            Debug.Log($"El valor '{input}' no es valido para {currentType.Name}");
        }
    }

    public void PalindromButton()
    {
        object input = inputField.text;
        try
        {
            object value = Convert.ChangeType(input, currentType);
            value = Palindromo.IsPalindrom(value.ToString());
            Debug.Log(input);
            DrawResult();

        }
        catch
        {
            Debug.Log($"El valor '{input}' no es valido para {currentType.Name}");
        }
    }

    public void PreviousNumSumButton()
    {
        object input = inputField.text;
        try
        {
            object value = Convert.ChangeType(input, currentType);
            value = PreviousNumSum.SumAllPreviousNum((int)value);
            Debug.Log(value);
            DrawResult();
        }
        catch
        {
            Debug.Log($"El valor '{input}' no es valido para {currentType.Name}");
        }
    }

    public TMP_Text text;

    public void PyramidButton()
    {
        string input = inputField.text;
        
        if (int.TryParse(input, out int inputInt))
        {
            string output = pyramid.CreateRecursive(inputInt);
            text.text = output;
            Debug.Log(output);
        }

        try
        {
        }
        catch
        {
            Debug.Log($"El valor '{input}' no es valido para {currentType.Name}");
        }
    }

}
