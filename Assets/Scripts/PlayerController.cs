using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class PlayerController : MonoBehaviour
{
    private Actor actor;

    public float maxSpeed;
    public float groundAcceleration;
    public float airAcceleration;
    public float gravity;
    public float jumpHeight;

    public bool grounded;

    public Vector2 velocity;

    private void Awake()
    {
        actor = GetComponent<Actor>();
        velocity = Vector2.zero;
    }

    private void Update()
    {
        // Check ground
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            transform.position + Vector3.down,
            actor.boxCollider.size,
            0,
            actor.solidLayer
        );
        if (hits.Length > 0)
            grounded = true;
        else 
            grounded = false; 

        // Player Input
        float moveInput = Input.GetAxisRaw("Horizontal");
        float targetMove = maxSpeed * moveInput / 60f;
        float acceleration = grounded ? groundAcceleration : airAcceleration;
        velocity.x = Mathf.MoveTowards(velocity.x, targetMove, acceleration * Time.deltaTime);

        if (grounded)
        {
            velocity.y = 0;
            if (Input.GetButtonDown("Jump"))
            {
                grounded = false;
                velocity.y = Mathf.Sqrt(2 * jumpHeight * gravity);
            }
        }

        velocity.y -= gravity * Time.deltaTime;

        actor.MoveX(velocity.x, CollideX);
        actor.MoveY(velocity.y, CollideY);
    }

    private void CollideX()
    {
        velocity.x = 0;
    }

    private void CollideY()
    {
        if (velocity.y < 0) grounded = true;
        velocity.y = 0;
    }
}
