using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public bool GettingKnockedBack { get; private set; }

    [SerializeField] private float knockBackTime = .2f;
    private Rigidbody2D rb;
    private Animator enemyAnimator;
    private EnemyHealth enemyHealth;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        enemyAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack(Transform damageSource, float knockBackThrust)
    {
        GettingKnockedBack = true;
        //ForceMode2D have 2 mode: Force (used for effects like wind blows, or objects slightly increase speed) and Impulse (used for jump, smash or knockedback effect)
        //Calculate the direction from player to enemy * thrust * enemy's mass (knockedback distance depends on light or heavy enemy)
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        if (enemyHealth.CurrentHealth > 0)
        {
            enemyAnimator.SetBool("IsHurt", true);
        }
        yield return new WaitForSeconds(knockBackTime);
        rb.linearVelocity = Vector2.zero;
        GettingKnockedBack = false;
        enemyAnimator.SetBool("IsHurt", false);
    }
}
