using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    //https://www.youtube.com/watch?v=f473C43s8nE&t=347s video where movement was taken from
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public bool isCrouch = false;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;
    public HealthBar healthBar;

    [Header("Spawn")]
    public Vector3 spawnPos;
    public Quaternion spawnRot;
    public GameObject zombiePrefab;

    [Header("Weapon")]
    public GameObject gun;

    [Header("Timer")]
    public float startTimerDuration = 90f; // Set the maximum timer duration in seconds
    private float currentTimer;
    public TextMeshProUGUI timerText;

    [Header("Weather")]
    private bool isSnowing; // Flag to check if it's snowing
    private float snowDuration; // Duration of snow effect
    private float snowTimer; // Timer to track the remaining snow duration
    public ParticleSystem snowParticles;
    public TextMeshProUGUI weatherText;

    [Header("Game Control UI")]
    public GameObject explanationPanel;
    public TextMeshProUGUI explanationText;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation= true;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        SetSpawn();

        // Show the start panel with the explanation
        explanationPanel.SetActive(true);

        // Set the explanation text (customize this based on your game)
        explanationText.text = "Welcome to Zombie Valley! Make it across the valley and get to your home before the timer runs out to win! Left click to shoot the zombies, P to pause, WASD to walk, space bar to jump, shift to crouch (makes you quieter to zombies). Campfires will become a new spawnpoint.";

        // Pause the game while waiting for input
        Time.timeScale = 0f;

        // Start the coroutine to handle player input
        StartCoroutine(WaitForPlayerInput());


        currentTimer = startTimerDuration;
        isSnowing = false;
        snowParticles.Stop();
        StartCoroutine(WeatherSystem());
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        MyInput();
        SpeedControl();
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        UpdateTimer();
        UpdateWeather();
        MoveSnow();

        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
        if (Time.timeScale == 0f && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ToggleSpeed();
        }

    }
    IEnumerator WaitForPlayerInput()
    {
        // Wait until any button is pressed
        while (!Input.anyKey)
        {
            yield return null;
        }

        // Hide the start panel
        explanationText.text = "";
        explanationPanel.SetActive(false);

        // Resume the game
        Time.timeScale = 1f;
    }
    void TogglePause()
    {
        // Toggle the game's pause state
        if (Time.timeScale == 0f)
        {
            // If the game is paused, resume it
            Time.timeScale = 1f;
            explanationText.text = "";
            explanationPanel.SetActive(false);
        }
        else
        {
            // If the game is not paused, pause it
            Time.timeScale = 0f;
            explanationText.text = "Paused. Press P to continue. Press R to restart.";
            explanationPanel.SetActive(true);
        }

        // Add additional logic here if needed when pausing/unpausing
    }
    void ToggleSpeed()
    {
        if (moveSpeed== 10)
        {
            moveSpeed = 3f;
            isCrouch = true;
        }
        else
        {
            moveSpeed = 10f;
            isCrouch = false;
        }

        // Add additional logic here if needed when pausing/unpausing
    }
    void RestartGame()
    {
        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3 (rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            if (isSnowing)
            {
                limitedVel = limitedVel / 3;
            }
          
            
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z); 
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.SetHealth(currentHealth);
        // Check if the player is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    private void Die()
    {
        // Reset the player's position to the spawn point
        transform.position = spawnPos;
        transform.rotation = spawnRot;

        // Reset health
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);

    }

    public void SetSpawn()
    {
        spawnPos = transform.position;
        spawnRot = transform.rotation;
    }

    public void AddBullets(int bulletCount)
    {
        gun.GetComponent<Weapon>().AddBullets(bulletCount);
    }

    public void AddTimer(int time)
    {
        currentTimer += time;
    }

    void UpdateTimer()
    {
        currentTimer -= Time.deltaTime;

        // Ensure the timer doesn't go below zero
        currentTimer = Mathf.Max(0f, currentTimer);

        // Update the UI Text for the timer
        UpdateTimerUI();

        // Check if the timer has reached zero
        if (currentTimer <= 0f)
        {
            // Handle timer expiration (e.g., trigger an event, reset the level, etc.)
            TimerExpired();
        }
    }
    void UpdateTimerUI()
    {
        // Update the UI Text with the current timer value
        timerText.text = "Time: " + Mathf.CeilToInt(currentTimer).ToString();
    }

    void TimerExpired()
    {
        // Add any logic for when the timer reaches zero
        Time.timeScale = 0f;
        explanationText.text = "You Lose. Press R to restart.";
        explanationPanel.SetActive(true);
    }

    void UpdateWeather()
    {
        // If it's snowing, decrease the snow timer
        if (isSnowing)
        {
            snowTimer -= Time.deltaTime;

            // If the snow duration is over, stop the snow effect
            if (snowTimer <= 0f)
            {
                StopSnowing();
            }
        }

        // Update the UI Text for the weather conditions
        UpdateWeatherUI();
    }
    void UpdateWeatherUI()
    {
        weatherText.text = isSnowing ? "Snow" : "Clear";
    }

    void StartSnowing()
    {
        isSnowing = true;
        snowDuration = Random.Range(10f, 40f);
        snowTimer = snowDuration;

        // Decrease player speed during snow


        // Start the snow particle system
        snowParticles.Play();
    }

    void StopSnowing()
    {
        isSnowing = false;


        // Stop the snow particle system
        snowParticles.Stop();
    }

    IEnumerator WeatherSystem()
    {
        while (true)
        {
            // Randomly start snowing with a 1/100 chance
            if (Random.Range(0, 60) == 0)
            {
                StartSnowing();
            }   

            yield return new WaitForSeconds(1f);
        }
    }

    private void MoveSnow()
    {
        Vector3 offset = new Vector3(0, 8, 0);
        snowParticles.transform.position = transform.position + offset;

    }

    public void WinGame()
    {
        Time.timeScale = 0f;
        GameManager.instance.IncreaseDifficulty();
        explanationText.text = "Congratulations! You Win! Press R to restart at a higher difficulty.";
        explanationPanel.SetActive(true);
    }

}


