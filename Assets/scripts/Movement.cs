using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    private float jumpHeight = 400;
    [SerializeField] private bool inAir;
    [SerializeField] private bool useArrowKeys; // Если true - стрелки, если false - WASD

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        int x = 0;
        KeyCode left = useArrowKeys ? KeyCode.LeftArrow : KeyCode.A;
        KeyCode right = useArrowKeys ? KeyCode.RightArrow : KeyCode.D;
        KeyCode jump = useArrowKeys ? KeyCode.UpArrow : KeyCode.W;

        if (true)
        {
            if (Input.GetKey(left))
            {
                x = -1;
            }
            if (Input.GetKey(right))
            {
                x = 1;
            }
            if (Input.GetKeyDown(jump) && !inAir)
            {
                inAir = true;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpHeight);
            }
        }
        rb.velocity = new Vector2(x * speed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (collision.contacts[0].normal.y > 0.1f)
            {
                inAir = false;
            }
        }
    }
}
