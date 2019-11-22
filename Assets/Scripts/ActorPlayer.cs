using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class ActorPlayer : Actor
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public float maxSpeed;
    public float groundAcceleration;
    public float airAcceleration;
    public float gravity;
    public float jumpHeight;

    public bool grounded;

    public Vector2 velocity;

    public bool isRewinding = false;
    private List<Instant> history;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        history = new List<Instant>();
        velocity = Vector2.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            isRewinding = true;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            isRewinding = false;

        if (!isRewinding)
            Physics();
        Animation();
    }

    private void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();
    }

    private void Record()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        
        history.Insert(0, new Instant(transform.position,velocity, info.normalizedTime, info.shortNameHash));
    }

    private void Rewind()
    {
        animator.Play(history[0].stateNameHash, 0, history[0].normalisedTime);
        transform.position = history[0].position;
        velocity = history[0].velocity;
        history.RemoveAt(0);
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

    public struct Instant
    {
        public Vector3 position;
        public Vector3 velocity;
        public float normalisedTime;
        public int stateNameHash;

        public Instant(Vector3 position, Vector3 velocity, float normalisedTime, int stateNameHash)
        {
            this.position = position;
            this.velocity = velocity;
            this.normalisedTime = normalisedTime;
            this.stateNameHash = stateNameHash;
        }
    }
}
