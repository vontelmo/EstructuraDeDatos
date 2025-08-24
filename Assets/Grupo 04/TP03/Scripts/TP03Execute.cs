using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TP03Execute : MonoBehaviour
{
    private MyStack<object> stack = new MyStack<object>();

    [SerializeField] GameObject listSquare;
    [SerializeField] Transform gridLayout;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] private TMP_Dropdown typeDropdown;

    private Type currentType = typeof(int);

    void DrawList()
    {
        foreach (Transform child in gridLayout)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < stack.Count; i++)
        {
            GameObject newItem = Instantiate(listSquare, gridLayout);


            TMP_Text childText = newItem.GetComponentInChildren<TMP_Text>();
            if (childText != null)
            {
                if (childText.text == default) return;

            }

        }
    }


    void Start()
    {
        typeDropdown.onValueChanged.AddListener(OnTypeChanged);
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        Debug.Log(stack.Count);
        DrawList();
    }



    void OnTypeChanged(int index)
    {
        switch (index)
        {
            case 0: currentType = typeof(int); break;
            case 1: currentType = typeof(float); break;
            case 2: currentType = typeof(string); break;
        }
        stack.Clear();
        DrawList();

    }

    public void PushValue()
    {
        object input = inputField.text;
        try
        {
            object value = Convert.ChangeType(input, currentType);
            stack.Push(value);
        }
        catch
        {
            Debug.Log($"El valor '{input}' no es válido para {currentType.Name}");
        }
        DrawList();

    }

    public void PopValue()
    {
        if (stack.Count > 0)
        {
            object value = stack.Pop();
            Debug.Log($"POP: {value} , ({value.GetType().Name})");
        }
        DrawList();

    }

    public void PeekValue()
    {
        object value = stack.Peek();
        Debug.Log($"PEEK: {value} ({value.GetType().Name})");
    }

    public void ClearStack()
    {
        stack.Clear();
        DrawList();

    }

    public void ToArrayStack() 
    {
        object[] array = stack.ToArray();
        Debug.Log($"STACK ARRAY : {array}");

    }

    public void ToStringStack()
    {
        string stringStack = stack.ToString();
        Debug.Log(stringStack);
    }

    public void TryPopStack()
    {
        object value;
        bool tryPop = stack.TryPop(out value);
        Debug.Log($"TRYPOP : {tryPop} , {value} ");
        DrawList();

    }


    public void TryPeekStack()
    {
        object value;
        bool tryPop = stack.TryPeek(out value);
        Debug.Log($"TRYPEEK : {tryPop} , {value} ");
    }

}
