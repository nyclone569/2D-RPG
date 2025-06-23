using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //Set up state machine for enemies
    private enum State
    {
        Idle,
        Roaming,
        Hurt,
        Death
    }

    private State state;
    private EnemyPathfinding enemyPathfinding;
    private Rigidbody2D rb;
    private Animator enemyAnimator;
    private Vector2 lastMoveDir = Vector2.down; //Idle dir default

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        enemyAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        state = State.Idle;
        StartCoroutine(RoamingRoutine());
    }

    //Give a delay time between each routine
    private IEnumerator RoamingRoutine()
    {
        while (true)
        {
            if (state == State.Idle)
            {
                enemyPathfinding.StopMoving();
                enemyAnimator.SetFloat("MoveX", lastMoveDir.x);
                enemyAnimator.SetFloat("MoveY", lastMoveDir.y);
                enemyAnimator.SetBool("IsMoving", false);
                yield return new WaitForSeconds(2f);
                state = State.Roaming;
            }
            else if (state == State.Roaming)
            {
                Vector2 roamPosition = GetRoamingPosition();
                lastMoveDir = roamPosition;
                enemyAnimator.SetFloat("MoveX", roamPosition.x);
                enemyAnimator.SetFloat("MoveY", roamPosition.y);
                enemyAnimator.SetBool("IsMoving", true);
                enemyPathfinding.MoveTo(roamPosition);
                yield return new WaitForSeconds(2f);
                state = State.Idle;
            }
        }
    }

    //Get a random direction for new routine
    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
