using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{

    public bool zombieBossDead;
    
    // Start is called before the first frame update
    void Start()
    {
        zombieBossDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && zombieBossDead)
        {
            // Assuming the player script is named PlayerController
            other.GetComponent<PlayerController>().WinGame();
        }
        else if (other.CompareTag("Zombie"))
        {
            other.GetComponent<ZombieController>().Die();
        }
    }
}
