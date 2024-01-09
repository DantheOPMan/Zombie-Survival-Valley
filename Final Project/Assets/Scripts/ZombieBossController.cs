using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBossController : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    public float moveSpeed = 3f;
    public Transform player;
    public float chaseDistance = 10f; // Set this to the desired chasing distance
    private bool isChasing = false;
    public float disapearDistance = 30f;

    public int damageAmount = 10; // Set the amount of damage the zombie deals
    private bool hasHitPlayer = false;

    public int maxHealth = 4;
    public int currentHealth;
    public GameObject bulletPickupPrefab;
    public GameObject endHouse;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        maxHealth = maxHealth + GameManager.instance.maxHealthIncrease;
        currentHealth = maxHealth;
        damageAmount = damageAmount * GameManager.instance.damageAmountMultiplier;
        moveSpeed = moveSpeed + GameManager.instance.moveSpeedIncrease;

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float crouchMultiplier = 1f;
            if (player.GetComponent<PlayerController>().isCrouch)
            {
                crouchMultiplier = 0.05f;
            }
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= chaseDistance * crouchMultiplier && distance > 1f)
            {
                isChasing = true;
            }else if(disapearDistance <= distance)
            {
                Destroy(gameObject);
            }
            else
            {
                isChasing=false;
            }
            if (isChasing)
            {
                animator.SetTrigger("IsWalking");
                transform.LookAt(player.position);
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
            else
            {
                animator.SetTrigger("IsIdle");
            }
            // Move towards the player
            
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasHitPlayer)
        {
            isChasing = false;
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                animator.SetTrigger("IsAttacking");
                player.TakeDamage(damageAmount);
                hasHitPlayer = true; // Prevent multiple hits in a short time if needed
            }
        }
    }


    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasHitPlayer = false;
        }
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Check if the zombie is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // Add any logic here for the zombie's death, such as playing death animation, particle effects, etc.
        // You can also destroy the zombie GameObject if needed.
        animator.SetTrigger("IsDead");

        Instantiate(bulletPickupPrefab, transform.position, Quaternion.identity);
        endHouse.GetComponent<EndGame>().zombieBossDead = true;
        Destroy(gameObject);
    }


}
