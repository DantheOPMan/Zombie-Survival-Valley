using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10;
    public float fireRate = 0.2f; // Adjust the fire rate as needed
    public int damagePerShot = 1;
    public float bulletLifetime = 4f;
    public int maxBullets = 100; // Set the maximum number of bullets the player can carry
    public int currentBullets;
    public TextMeshProUGUI bulletCountText;
    public AudioSource gunshotAudio;

    private bool canShoot = true;

    private void Start()
    {
        currentBullets = maxBullets/10;
        UpdateBulletUI();
        gunshotAudio = GetComponent<AudioSource>();
    } 

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        //Needs to have more than 1 bullet in order to fire
        if(currentBullets > 0) 
        {
            canShoot = false;

            // Remove 1 from bullet count
            currentBullets--;

            gunshotAudio.Play();

            // Instantiate a bullet
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);


            // Get the bullet's script (assuming you have a Bullet script attached to the bulletPrefab)
            Bullet bulletScript = bullet.GetComponent<Bullet>();

            // Get the player's view direction
            Vector3 playerViewDirection = Camera.main.transform.forward;

            // Set bullet's initial velocity based on the player's view direction
            bullet.GetComponent<Rigidbody>().velocity = playerViewDirection * bulletSpeed;

            // Set bullet damage
            bulletScript.SetDamage(damagePerShot);

            Destroy(bullet, bulletLifetime);

            // Wait for the next shot
            yield return new WaitForSeconds(fireRate);

            // Update Bullets to reflect the change
            UpdateBulletUI();

            canShoot = true;
            
        }

    }

    public int GetBullets()
    {
        return currentBullets;
    }
    public void AddBullets(int amount)
    {
        currentBullets += amount;
        currentBullets = Mathf.Clamp(currentBullets, 0, maxBullets);

        // Update your UI or any other logic to reflect the change in bullet count
        UpdateBulletUI();
    }

    void UpdateBulletUI()
    {
        // Assuming your UI Text displays the bullet count as plain text
        bulletCountText.text = "Bullets: " + currentBullets.ToString();
    }


}
