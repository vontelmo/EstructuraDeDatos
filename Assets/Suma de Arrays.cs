using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumadeArrays : MonoBehaviour
{
    public int GetArraySum(int n, int[] array)
    {
        if (n == array.Length)
        {
            return 0;
        }
        else
        {
            return array[n] + GetArraySum(++n, array);
        }
    }

    private void Start()
    {
        int[] startArray = { 2, 3, 7, 6, 6};

        Debug.Log(GetArraySum(0, startArray));
    }
}
