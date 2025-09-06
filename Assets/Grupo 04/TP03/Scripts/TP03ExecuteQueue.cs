using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TP03ExecuteQueue : MonoBehaviour
{
    private MyQueue<object> queue = new MyQueue<object>();

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

        for (int i = 0; i < queue.Count; i++)
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
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        Debug.Log(queue.Count);
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
        queue.Clear();
        DrawList();

    }

    public void EnqueueValue()
    {
        object input = inputField.text;
        try
        {
            object value = Convert.ChangeType(input, currentType);
            queue.Enqueue(value);
        }
        catch
        {
            Debug.Log($"El valor '{input}' no es válido para {currentType.Name}");
        }
        DrawList();

    }

    public void DequeueValue()
    {
        if (queue.Count > 0)
        {
            object value = queue.Dequeue();
            Debug.Log($"DEQUEUE : {value} , ({value.GetType().Name})");
        }
        DrawList();

    }

    public void PeekValue()
    {
        object value = queue.Peek();
        Debug.Log($"PEEK: {value} ({value.GetType().Name})");
    }

    public void ClearQueue()
    {
        queue.Clear();
        DrawList();

    }

    public void ToArrayQueue()
    {
        object[] array = queue.ToArray();
        Debug.Log($"QUEUE ARRAY : {array}");

    }

    public void ToStringQueue()
    {
        string stringqueue = queue.ToString();
        Debug.Log(stringqueue);
    }

    public void TryDequeue()
    {
        object value;
        bool tryPop = queue.TryDequeue(out value);
        Debug.Log($"TRYDEQUEUE : {tryPop} , {value} ");
        DrawList();

    }


    public void TryPeekqueue()
    {
        object value;
        bool tryPop = queue.TryPeek(out value);
        Debug.Log($"TRYPEEK : {tryPop} , {value} ");
    }

}