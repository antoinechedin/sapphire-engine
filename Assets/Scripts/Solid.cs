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
            foreach (Actor actor in allActors)
            {

            }

            if (moveX != 0)
            {
                xRemainer -= moveX;
                transform.Translate(moveX, 0, 0);

                foreach (Actor actor in allActors)
                {
                    ColliderDistance2D dist = boxCollider.Distance(actor.boxCollider);
                    if (dist.isOverlapped)
                    {
                        boxCollider.enabled = false;
                        actor.MoveX(
                            (dist.pointA - dist.pointB).x + Mathf.Sign(moveX),
                            null
                        );
                        boxCollider.enabled = true;
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
                transform.Translate(0, moveY, 0);

                foreach (Actor actor in allActors)
                {
                    ColliderDistance2D dist = boxCollider.Distance(actor.boxCollider);
                    if (dist.isOverlapped)
                    {
                        boxCollider.enabled = false;
                        actor.MoveY(
                            (dist.pointA - dist.pointB).y + Mathf.Sign(moveY),
                            null
                        );
                        boxCollider.enabled = true;
                    }
                    else if (ridingActor.Contains(actor))
                    {
                        actor.MoveY(moveY, null);
                    }
                }
            }


        }
    }
}
