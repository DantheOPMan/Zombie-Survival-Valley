using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Set the damage value for the bullet
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            ZombieController zombie = collision.gameObject.GetComponent<ZombieController>();
            if (zombie != null)
            {
                zombie.TakeDamage(damage);
            }

            
        } else if (collision.gameObject.CompareTag("ZombieBoss"))
        {
            ZombieBossController zombie = collision.gameObject.GetComponent<ZombieBossController>();
            if (zombie != null)
            {
                zombie.TakeDamage(damage);
            }
        }

        // Destroy the bullet upon collision
        Destroy(gameObject);
    }

}
