using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerEj4 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    public MyQueue<Vector2> queueInputs = new MyQueue<Vector2>();
    private Rigidbody2D rb;
    private Vector2 movement;

    [SerializeField] private bool triggerActivated = false;
    [SerializeField] private GameObject shadow;

    public bool TriggerActivated => triggerActivated;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        queueInputs.Enqueue(input);

        rb.velocity = input * moveSpeed;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (rb != null) 
        {
            triggerActivated = true;
            shadow.SetActive(true);
        }
    }
}


