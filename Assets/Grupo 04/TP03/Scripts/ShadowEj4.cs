using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowEj4 : MonoBehaviour
{
    [SerializeField] PlayerEj4 player;
    private Rigidbody2D rb;
    private MyQueue<Vector2> shadowQueue = new MyQueue<Vector2>();
    private float moveSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.TriggerActivated) 
        {
            if (player.queueInputs.TryDequeue(out Vector2 input))
            {
                rb.velocity = input * moveSpeed;
                Debug.Log("input???" + input);
            }
        }

    }

}


