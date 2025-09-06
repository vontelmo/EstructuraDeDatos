using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TP01Execute : MonoBehaviour
{
    SimpleList<int> list = new SimpleList<int>();


    [SerializeField] GameObject listSquare;
    [SerializeField] Transform gridLayout;
    [SerializeField] TMP_InputField inputField;
    private string inputText;



    private void Start()
    {

    }

    void DrawList()
    {
        foreach (Transform child in gridLayout)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < list.Count; i++)
        {
            GameObject newItem = Instantiate(listSquare, gridLayout);


            TMP_Text childText = newItem.GetComponentInChildren<TMP_Text>();
            if (childText != null)
            {
                if (childText.text == default) return;
                childText.text = list[i].ToString();

            }

        }
    }

    public void AddToList()
    {
        string inputText = inputField.text;

        if (int.TryParse(inputText, out int value))
        {
            list.Add(value);
            inputField.text = ""; 
        }
        else
        {
            Debug.LogWarning("Valor no valido");
        }
        DrawList();
        
    }

    public void RemoveToList() 
    {
        string inputText = inputField.text;

        if (int.TryParse(inputText, out int value))
        {
            list.Remove(value);
            inputField.text = "";

        }
        else
        {
            Debug.LogWarning("Valor no valido");
        }
        DrawList();

    }

    public void ClearList() 
    {
        list.Clear();
        DrawList();
    }

    public void AddRangeToList()
    {
        list.AddRange(new int[] {1,2,3});
        DrawList();
    }

    public override string ToString()
    {
        return string.Join(", ", list); 
    }

    public void SortByBubble()
    {
        BubbleSort.BubbleSorting(list);
    }

    public void SortBySelection()
    {
        SelectionSort.SelectionSorting(list);
    }

    public void SortByQuick()
    {
        QuickSort.QuickSorting(list);
    }
}