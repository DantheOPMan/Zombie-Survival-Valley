using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform player;
    public float stoppingDistance = 2f;

    void Update()
    {
        if (player != null)
        {
            // Move towards the player
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > stoppingDistance)
            {
                transform.LookAt(player.position);
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
            // You can add other behaviors when the player is within stopping distance
        }
    }
}
