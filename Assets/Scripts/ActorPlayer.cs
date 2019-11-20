using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorPlayer : Actor
{
    public float maxSpeed;
    public float groundAcceleration;
    public float airAcceleration;
    public float gravity;
    public float jumpHeight;

    public bool grounded;

    public Vector2 velocity;

    protected override void Awake()
    {
        velocity = Vector2.zero;
        base.Awake();
    }

    private void Update()
    {
        // Check ground
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            transform.position + Vector3.down,
            boxCollider.size,
            0,
            solidLayer
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

        MoveX(velocity.x, CollideX);
        MoveY(velocity.y, CollideY);
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

    public override bool IsRinging(Solid solid)
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            transform.position + Vector3.down,
            boxCollider.size,
            0,
            solidLayer
        );

        foreach (Collider2D collider in hits)
            if (collider == solid.boxCollider) return true;
        return false;
    }

    public override void Squish()
    {
        throw new System.NotImplementedException();
    }
}
