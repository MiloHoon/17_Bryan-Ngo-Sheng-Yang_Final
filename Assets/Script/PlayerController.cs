using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Player movement
    public static bool PlayerDeath = false;
    float playerSpeed = 5.0f;

    // Score
    public static PlayerController score;
    float Scorecount = 0;

    // Gun
    public int maxAmmo = 15;
    private int currentAmmo;
    public float reloadTime = 2.0f;
    private bool isReloading = false;
    public float nextShot = 3.0f;

    // Health
    public int maxHealth = 100;
    int currentHealth;

    public HealthScript healthBar;

    // Key
    int getKey = 0;

    // Player
    bool isDead = false;
    
    public Rigidbody PlayerRB;
    public Animator PlayerAnim;

    // Audio
    public AudioClip[] AudioClipsArr;
    private AudioSource audioSource;

    // Game objects
    public GameObject BulletPrefab;
    public GameObject BulletSpawn;
    public GameObject Door;
    public GameObject Door2;

    // Text
    public Text Ammotext;
    public Text Scoretext;

    // Start is called before the first frame update
    void Start()
    {
        // Set Player Alive
        PlayerDeath = false;

        // Get Audio
        audioSource = GetComponent<AudioSource>();

        // Set ammo on start
        currentAmmo = maxAmmo;
        Ammotext.GetComponent<Text>().text = "Ammo: " + currentAmmo;

        // Check, set and display health when start
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        // Hide cursor
        Cursor.lockState = CursorLockMode.Locked;

        if (score == null)
        {
            score = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerDeath == false)
        {
            Movement();

            Shooting();
        }

        // Player death
        if (currentHealth <= 0)
        {
            PlayerDeath = true;
            Die();
            isDead = true;
        }
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed);
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            PlayerAnim.SetBool("isRunning", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * playerSpeed);
            //transform.rotation = Quaternion.Euler(0, -90, 0);
            PlayerAnim.SetBool("isRunning", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime * playerSpeed);
            //transform.rotation = Quaternion.Euler(0, 180, 0);
            PlayerAnim.SetBool("isRunning", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * playerSpeed);
            //transform.rotation = Quaternion.Euler(0, 90, 0);
            PlayerAnim.SetBool("isRunning", true);
        }
        else
        {
            PlayerAnim.SetBool("isRunning", false);
        }
    }

    void Shooting()
    {
        // Shooting
        if (Input.GetKeyDown(KeyCode.Space) && isReloading == false && currentAmmo >= 0)
        {
            Instantiate(BulletPrefab, BulletSpawn.transform.position, transform.rotation);
            audioSource.PlayOneShot(AudioClipsArr[1]);
            PlayerAnim.SetBool("isShooting", true);
            Ammotext.GetComponent<Text>().text = "Ammo: " + currentAmmo;
            currentAmmo--;
            // Addaudio //
        }
        else
        {
            PlayerAnim.SetBool("isShooting", false);
        }
        // Reload
        if (Input.GetKeyDown(KeyCode.R) && isReloading == false && isDead == false && currentAmmo < 15 )
        {
            audioSource.PlayOneShot(AudioClipsArr[2]);
            PlayerAnim.SetBool("isReloading", true);
            StartCoroutine(Reload());
            return;
        }
    }

    void TakeDamage(int damage)
    {
        // Do damage 
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Ammotext.GetComponent<Text>().text = "Ammo: Reloading..";

        yield return new WaitForSeconds(reloadTime);
        // Addaudio //
        currentAmmo = maxAmmo;
        Ammotext.GetComponent<Text>().text = "Ammo: " + currentAmmo;
        isReloading = false;
        PlayerAnim.SetBool("isReloading", false);
    }

    void Die()
    {
        // Death state
        if (!isDead)
        {
            PlayerAnim.SetTrigger("Death");
            Destroy(gameObject, 4);
            isDead = true;
            SceneManager.LoadScene("LoseScene");
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void AddScore()
    {
        // Add score
        Scorecount += 15;
        Scoretext.GetComponent<Text>().text = "Score:" + Scorecount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Collision With Key
        if (collision.gameObject.tag == "Key")
        {
            audioSource.PlayOneShot(AudioClipsArr[3]);
            Destroy(collision.gameObject);
            getKey = 1;
        }
        // Collision With Door
        if (collision.gameObject.tag == "Door" && getKey == 1)
        {
            audioSource.PlayOneShot(AudioClipsArr[4]);
            Destroy(Door);
        }
        // Collision With Door2
        if (collision.gameObject.tag == "Door2" && Scorecount == 150)
        {
            audioSource.PlayOneShot(AudioClipsArr[4]);
            Destroy(Door2);
        }

        // Collision With Enemy
        if (collision.gameObject.tag == "Enemy")
        {
            audioSource.PlayOneShot(AudioClipsArr[0]);
            TakeDamage(10);
        }

        // Win Scene
        if (collision.gameObject.tag == "Chest")
        {
            audioSource.PlayOneShot(AudioClipsArr[5]);
            SceneManager.LoadScene("WinScene");
            Cursor.lockState = CursorLockMode.None;
        }
    }
}