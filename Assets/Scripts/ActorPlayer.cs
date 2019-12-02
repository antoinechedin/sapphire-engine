using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class ActorPlayer : Actor
{
    public Animator animator;
    private SpriteRenderer spriteRenderer;

    public float maxSpeed;
    public float groundAcceleration;
    public float airAcceleration;
    public float gravity;
    public float jumpHeight;

    public bool grounded;
    public bool alive;

    public Vector2 velocity;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        velocity = Vector2.zero;
        alive = true;
    }

    private void Update()
    {
        if (alive && !TimeManager.Instance.rewind)
        {
            Physics();
            Animation();
        }
    }

    private void Physics()
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

    private void Animation()
    {
        animator.SetBool("moving", velocity.x != 0);
        animator.SetBool("grounded", grounded);
        if (velocity.x > 0) spriteRenderer.flipX = false;
        if (velocity.x < 0) spriteRenderer.flipX = true;
    }

    public void Die()
    {
        if (alive)
        {
            alive = false;
            velocity = Vector3.zero;
            animator.SetTrigger("die");
        }
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
        Die();
    }
}
