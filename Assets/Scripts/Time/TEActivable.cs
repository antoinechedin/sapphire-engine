using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Activable))]
public class TEActivable : TimeEntity
{
    private Activable activable;
    private Stack<Instant> history;

    private void Awake()
    {
        activable = GetComponent<Activable>();
        history = new Stack<Instant>();
    }

    public override void Record()
    {
        AnimatorStateInfo info = activable.animator.GetCurrentAnimatorStateInfo(0);
        Instant instant = new Instant(
            info.shortNameHash,
            info.normalizedTime,
            activable.powered
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

        activable.animator.Play(instant.stateHash, 0, instant.normalizedTime);
        activable.powered = instant.powered;
    }

    public struct Instant
    {
        public int stateHash;
        public float normalizedTime;
        public bool powered;

        public Instant(int stateHash, float normalizedTime, bool powered)
        {
            this.stateHash = stateHash;
            this.normalizedTime = normalizedTime;
            this.powered = powered;
        }
    }
}
