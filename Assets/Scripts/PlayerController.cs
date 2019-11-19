using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    private Collider2D c2D;

    public float maxSpeed;
    public float acceleration;
    //public float airModifier;
    public float gravity;

    public bool isGrounded;


    public Vector2 velocity;

    private void Awake()
    {
        c2D = GetComponent<Collider2D>();
        velocity = Vector2.zero;
    }

    private void Update()
    {
        if (c2D.Cast(Vector2.down, null, 0.01f) > 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        Vector2 targetVelocity = Vector2.zero;

        float moveInput = Input.GetAxisRaw("Horizontal");
        targetVelocity += Vector2.right * moveInput * maxSpeed;
        velocity.x = Mathf.MoveTowards(velocity.x, targetVelocity.x, acceleration);

        transform.Translate(velocity * Time.deltaTime);
    }
}
