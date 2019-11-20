using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Solid : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    private float xRemainer = 0f;
    private float yRemainer = 0f;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Move(Vector3 move)
    {
        Move(move.x, move.y);
    }

    public void Move(float x, float y)
    {
        xRemainer += x;
        yRemainer += y;

        int moveX = Mathf.RoundToInt(xRemainer);
        int moveY = Mathf.RoundToInt(yRemainer);

        if (moveX != 0 || moveY != 0)
        {
            Actor[] allActors = GameObject.FindObjectsOfType<Actor>();
            List<Actor> ridingActor = new List<Actor>();
            boxCollider.enabled = false;

            if (moveX != 0)
            {
                xRemainer -= moveX;
                transform.Translate(moveX, 0, 0);

                foreach (Actor actor in allActors)
                {
                    if (boxCollider.IsTouching(actor.boxCollider))
                    {
                        actor.MoveX(
                            boxCollider.size.x / 2f - actor.boxCollider.size.x / 2f,
                            null
                        );
                    }
                    else if (ridingActor.Contains(actor))
                    {
                        actor.MoveX(moveX, null);
                    }
                }
            }

            if (moveY != 0)
            {
                yRemainer -= moveY;
                transform.Translate(moveY, 0, 0);

                foreach (Actor actor in allActors)
                {
                    if (boxCollider.IsTouching(actor.boxCollider))
                    {
                        actor.MoveY(
                            boxCollider.size.y / 2f - actor.boxCollider.size.y / 2f,
                            null
                        );
                    }
                    else if (ridingActor.Contains(actor))
                    {
                        actor.MoveY(moveY, null);
                    }
                }
            }

            boxCollider.enabled = true;
        }
    }
}
