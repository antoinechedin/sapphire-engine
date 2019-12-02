using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActorPlayer))]
public class TEPlayer : TimeEntity
{
    private ActorPlayer player;
    private Stack<Instant> history;

    public int numTrail;

    private GameObject[] trails;

    private void Awake()
    {
        GameObject childPrefab = new GameObject();
        childPrefab.AddComponent<SpriteRenderer>();

        player = GetComponent<ActorPlayer>();
        history = new Stack<Instant>();
        trails = new GameObject[numTrail];
        float alpha = ((float)numTrail) / (2 * (numTrail + 1));
        for (int i = 0; i < numTrail; i++)
        {
            GameObject child = Instantiate(childPrefab);
            child.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
            alpha -= 1f / (2f * (numTrail + 1f));

            trails[i] = child;
        }
    }

    public override void Record()
    {
        foreach (GameObject trail in trails)
        {
            trail.GetComponent<SpriteRenderer>().sprite = null;
        }

        AnimatorStateInfo info = player.animator.GetCurrentAnimatorStateInfo(0);
        Instant instant = new Instant(
            player.transform.position,
            player.velocity,
            info.shortNameHash,
            info.normalizedTime,
            player.alive,
            player.GetComponent<SpriteRenderer>().sprite
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

        for (int i = numTrail - 1; i > 0; i--)
        {
            trails[i].GetComponent<SpriteRenderer>().sprite = trails[i - 1].GetComponent<SpriteRenderer>().sprite;
            trails[i].transform.position = trails[i - 1].transform.position;
        }
        trails[0].GetComponent<SpriteRenderer>().sprite = player.GetComponent<SpriteRenderer>().sprite;
        trails[0].transform.position = player.transform.position;

        player.transform.position = instant.position;
        player.velocity = instant.velocity;
        player.animator.Play(instant.stateHash, 0, instant.normalizedTime);
        player.alive = instant.alive;
    }

    public struct Instant
    {
        public Vector2 position;
        public Vector2 velocity;
        public int stateHash;
        public float normalizedTime;
        public bool alive;
        public Sprite sprite;

        public Instant(Vector2 position, Vector2 velocity, int stateHash,
            float normalizedTime, bool alive, Sprite sprite)
        {
            this.position = position;
            this.velocity = velocity;
            this.stateHash = stateHash;
            this.normalizedTime = normalizedTime;
            this.alive = alive;
            this.sprite = sprite;
        }
    }
}
