using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowEj4 : MonoBehaviour
{
    [SerializeField] PlayerEj4 player;

    private Rigidbody2D rb;
    private float moveSpeed = 5;

    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (player.TriggerActivated) 
        {
            if (player.queueInputs.TryDequeue(out Vector2 input))
            {
                rb.velocity = input * moveSpeed;
            }
        }

    }

    private void OnEnable()
    {
        transform.position = player.transform.position;
    }

}


