using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateAi : MonoBehaviour
{
    [Header("Patrolling Settings")]
    [SerializeField] private float patrolSpeed = 2.0f;
    [SerializeField] private float patrolDistance = 5.0f;
    private float patrolDirection = 1.0f;
    private Vector2 initialPosition;

    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 3.0f;
    [SerializeField] private float attackSpeed = 4.0f;

    [Header("Player Reference")]
    [SerializeField] private Transform player;

    // AI State management
    delegate void AIState();
    private AIState currentState;
    private float stateTime = 0;
    private bool justChangedState = false;

    private enum State { Patrolling, Attacking }
    private State currentStateEnum = State.Patrolling;

    private Vector2 patrolTargetPosition;
    private bool isReturningToPatrol = false;

    private void Start()
    {
        initialPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        patrolTargetPosition = initialPosition; // Set initial position as patrol target
        ChangeState(PatrolState);
    }

    private void Update()
    {
        AITick();
    }

    private void AITick()
    {
        if (justChangedState)
        {
            stateTime = 0;
            justChangedState = false;
        }
        currentState();
        stateTime += Time.deltaTime;
    }

    private void ChangeState(AIState newState)
    {
        currentState = newState;
        justChangedState = true;
    }

    private void PatrolState()
    {
        if (stateTime == 0)
        {
            currentStateEnum = State.Patrolling;
        }

        if (isReturningToPatrol)
        {
            ReturnToPatrolPosition();
        }
        else
        {
            Patrol();
        }

        CheckForPlayer();

        // Switch to attacking if the player is within range
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            ChangeState(AttackState);
        }
    }

    private void AttackState()
    {
        if (stateTime == 0)
        {
            currentStateEnum = State.Attacking;
        }

        AttackPlayer();
        CheckPlayerDistance();

        // Switch back to patrol if the player moves out of range
        if (Vector2.Distance(transform.position, player.position) > attackRange)
        {
            ChangeState(PatrolState);
        }
    }

    private void Patrol()
    {
        transform.Translate(Vector2.right * patrolDirection * patrolSpeed * Time.deltaTime);

        // Reverse direction if the enemy reaches patrol bounds
        if (Mathf.Abs(transform.position.x - initialPosition.x) >= patrolDistance)
        {
            patrolDirection *= -1;
            Flip();
        }
    }

    private void AttackPlayer()
    {
        // Move towards the player
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * attackSpeed * Time.deltaTime);

        // Flip enemy to face the player
        if ((direction.x > 0 && transform.localScale.x < 0) || (direction.x < 0 && transform.localScale.x > 0))
        {
            Flip();
        }
    }

    private void CheckForPlayer()
    {
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            currentStateEnum = State.Attacking;
        }
    }

    private void CheckPlayerDistance()
    {
        if (Vector2.Distance(transform.position, player.position) > attackRange)
        {
            ChangeState(PatrolState);
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // When returning to patrol after losing sight of the player, return to the original patrol position
    private void ReturnToPatrolPosition()
    {
        // Move back to the original patrol position or near it
        transform.position = Vector2.MoveTowards(transform.position, initialPosition, patrolSpeed * Time.deltaTime);

        // Once we return to the initial position, stop returning and resume normal patrol
        if (Vector2.Distance(transform.position, initialPosition) < 0.1f)
        {
            isReturningToPatrol = false;
            ChangeState(PatrolState); // Resume patrolling
        }
    }

    private void OnPlayerLostSight()
    {
        isReturningToPatrol = true; // Start returning to the original patrol position when player is lost
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("wall"))
        {
            // Reverse patrol direction
            patrolDirection *= -1;
            Flip();
        }
    }
}
