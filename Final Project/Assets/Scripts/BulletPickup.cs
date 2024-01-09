using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : MonoBehaviour
{
    public int bulletAmount = 10; // Set the number of bullets the pickup provides

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,30);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Assuming the player has a script named PlayerController
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                // Add bullets to the player's inventory or weapon
                player.AddBullets(bulletAmount);

                // Destroy the pickup
                Destroy(gameObject);
            }
        }
    }

}
