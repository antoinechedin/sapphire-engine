using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Trapdoor))]
public class TETrapdoor : TimeEntity
{
    private Trapdoor trapdoor;
    private Stack<Instant> history;

    private void Awake()
    {
        trapdoor = GetComponent<Trapdoor>();
        history = new Stack<Instant>();
    }

    public override void Record()
    {
        AnimatorStateInfo info = trapdoor.animator.GetCurrentAnimatorStateInfo(0);
        Instant instant = new Instant(
            !trapdoor.c2D.enabled,
            info.shortNameHash,
            info.normalizedTime
        );
        history.Push(instant);
    }

    public override void Rewind()
    {
        Instant instant;
        if (history.Count > 1)
            instant = history.Pop();
        else
            instant = history.Peek();

        trapdoor.animator.Play(instant.stateHash, 0, instant.normalizedTime);
        trapdoor.c2D.enabled = !instant.open;
    }

    public struct Instant
    {
        public bool open;
        public int stateHash;
        public float normalizedTime;

        public Instant(bool open, int stateHash, float normalizedTime)
        {
            this.open = open;
            this.stateHash = stateHash;
            this.normalizedTime = normalizedTime;
        }
    }
}
