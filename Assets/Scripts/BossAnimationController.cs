using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationController : MonoBehaviour
{
    public static BossAnimationController instance;
    private Animator animator;
    private Transform playerTransform;
    public float walkingSpeed = 2.0f;
    public bool isWalking = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

    }
    private void Start()
    {
        //Walk();
    }

    public void Update()
    {

    }
    public void MoveTowardsPlayer()
    {
        // Move the boss towards the player's position
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.position += direction * walkingSpeed * Time.deltaTime;

        // Rotate the boss to face the player's direction
        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
        Walk();
        AttackPlayer();
    }

    public void AttackPlayer()
    {
        isWalking = false;
        animator.SetTrigger("Attack");
    }

    public void Walk()
    {
        isWalking = true;
        animator.SetTrigger("Walk");
    }

    public void Idle()
    {
        isWalking = false;
        animator.SetTrigger("PlayerWin");
        Die();
    }

    public void Die()
    {
        isWalking = false;
        animator.SetTrigger("Die");
    }
}
