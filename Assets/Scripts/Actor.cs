using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Actor : MonoBehaviour
{
    [HideInInspector]
    public BoxCollider2D boxCollider;
    public LayerMask solidLayer;

    private float xRemainer = 0f;
    private float yRemainer = 0f;

    protected virtual void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void MoveX(float amount, System.Action onCollide)
    {
        xRemainer += amount;
        int move = Mathf.RoundToInt(xRemainer);

        if (move != 0)
            xRemainer -= move;
        int sign = move < 0 ? -1 : 1;


        while (move != 0)
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(
                transform.position + new Vector3(sign, 0, 0),
                boxCollider.size, 0, solidLayer
            );
            if (hits.Length == 0)
            {
                transform.Translate(sign, 0, 0);
                move -= sign;
            }
            else
            {
                if (onCollide != null)
                    onCollide();
                break;
            }
        }
    }

    public void MoveY(float amount, System.Action onCollide)
    {
        yRemainer += amount;
        int move = Mathf.RoundToInt(yRemainer);

        if (move != 0)
            yRemainer -= move;
        int sign = move < 0 ? -1 : 1;

        while (move != 0)
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(
                transform.position + new Vector3(0, sign, 0),
                boxCollider.size, 0, solidLayer
            );
            if (hits.Length == 0)
            {
                transform.Translate(0, sign, 0);
                move -= sign;
            }
            else
            {
                if (onCollide != null)
                    onCollide();
                break;
            }
        }
    }

    public virtual bool IsRinging(Solid solid) { return false; }

    public virtual void Squish() { }
}
