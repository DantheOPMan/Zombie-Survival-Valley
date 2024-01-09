using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{

    private bool notHit;
    // Start is called before the first frame update
    void Start()
    {
        notHit = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && notHit)
        {
            // Assuming the player script is named PlayerController
            other.GetComponent<PlayerController>().SetSpawn();
            other.GetComponent<PlayerController>().Heal(100);
            other.GetComponent<PlayerController>().AddBullets(10);
            other.GetComponent<PlayerController>().AddTimer(30);
            notHit = false;
        }else if (other.CompareTag("Zombie"))
        {
            other.GetComponent<ZombieController>().Die();
        }
    }

}
