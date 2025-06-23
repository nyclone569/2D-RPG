using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Vector2 moveDir;
    private KnockBack knockBack;

    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        //if enemy is knocked back, stop moving
        if (knockBack.GettingKnockedBack) { return; }
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.deltaTime));
    }

    public void StopMoving()
    {
        moveDir = Vector2.zero;
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = targetPosition;
    }
}
