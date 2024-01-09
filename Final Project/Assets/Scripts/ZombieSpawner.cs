using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefabMain;
    public GameObject zombiePrefabFast;
    public GameObject player;
    public int numberOfZombies = 7;
    public float spawnRadius = 30f;
    public float spawnInterval = 4f;

    void Start()
    {
        StartCoroutine(SpawnZombies());
    }

    IEnumerator SpawnZombies()
    {
        while (true)
        {

            if (!player.GetComponent<PlayerController>().isCrouch)
            {


                for (int i = 0; i < numberOfZombies; i++)
                {
                    // Calculate a random angle
                    float randomAngle = Random.Range(0f, 360f);

                    // Convert angle to radians
                    float angleInRadians = randomAngle * Mathf.Deg2Rad;
                    float randomRadius = Random.Range(5f, spawnRadius);

                    // Calculate spawn position based on player's position and random angle
                    float spawnX = transform.position.x + randomRadius * Mathf.Cos(angleInRadians);
                    float spawnZ = transform.position.z + randomRadius * Mathf.Sin(angleInRadians);
                    float spawnY = transform.position.y - 1f;

                    // Create a random position within the spawn radius
                    Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);

                    // Spawn the zombie at the calculated position
                    float randomValue = Random.value;

                    // Determine if it's a faster zombie based on a 1/10 chance
                    if (randomValue < 0.1f)
                    {
                        Instantiate(zombiePrefabFast, spawnPosition, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(zombiePrefabMain, spawnPosition, Quaternion.identity);
                    }


                    // Wait for the specified spawn interval before spawning the next zombie

                }
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

}
