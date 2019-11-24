using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActorPlayer))]
public class TEActorPlayer : TimeEntity
{
    private ActorPlayer actorPlayer;
    private Stack<Instant> history;

    private void Awake()
    {
        actorPlayer = GetComponent<ActorPlayer>();
        history = new Stack<Instant>();
    }

    public override void Record()
    {
        AnimatorStateInfo animInfo = actorPlayer.animator.GetCurrentAnimatorStateInfo(0);
        Instant instant = new Instant(
            actorPlayer.transform.position,
            actorPlayer.velocity,
            animInfo.shortNameHash,
            animInfo.normalizedTime
        );
        history.Push(instant);
    }

    public override void Rewind()
    {
        Instant instant;
        if (history.Count > 1)
        {
            instant = history.Pop();
        }
        else
        {
            instant = history.Peek();
        }
        actorPlayer.transform.position = instant.position;
        actorPlayer.velocity = instant.velocity;
        actorPlayer.animator.Play(instant.stateHashName, 0, instant.animNormalisedTime);
    }

    private struct Instant
    {
        public Vector2 position;
        public Vector2 velocity;
        public int stateHashName;
        public float animNormalisedTime;

        public Instant(Vector2 position, Vector2 velocity, int stateHashName, float animNormalisedTime)
        {
            this.position = position;
            this.velocity = velocity;
            this.stateHashName = stateHashName;
            this.animNormalisedTime = animNormalisedTime;
        }
    }
}

