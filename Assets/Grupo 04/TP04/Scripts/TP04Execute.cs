using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TP04Execute : MonoBehaviour
{
    private SimpleList<object> mySimpleList = new SimpleList<object>();

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

        for (int i = 0; i < mySimpleList.Count; i++)
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
        Debug.Log(mySimpleList.Count);
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
        mySimpleList.Clear();
        DrawList();

    }

    /*
    public void SortByBubble()
    {
        SimpleList<int> list = 
        BubbleSort.BubbleSorting();
    }

    public void SortBySelection()
    {
        SelectionSort.SelectionSorting(list);
    }

    public void SortByQuick()
    {
        QuickSort.QuickSorting(list);
    }
    */
}

