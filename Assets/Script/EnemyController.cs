using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    // Enemy
    public NavMeshAgent Enemy;
    public Animator EnemyAnim;

    public int maxHealth = 30;
    public int currentHealth;
    public int damage = 10;

    // Player
    GameObject target;

    bool isdead = false;

    // Audio
    public AudioClip[] AudioClipsArr;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Find player
        target = GameObject.Find("Player");

        // Health 
        currentHealth = maxHealth;
        
        // Get Animator
        EnemyAnim = this.GetComponent<Animator>();

        // Get Audio
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Health death;
        if (currentHealth <= 0)
        {
            Die();
            isdead = true;
        }

    }

    void FixedUpdate()
    {
        Enemy.SetDestination(target.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            audioSource.PlayOneShot(AudioClipsArr[0]);
            Destroy(collision.gameObject);
            takeDamage(10);
        }
    }

    void takeDamage(int damage)
    {
        // Do damage 
        currentHealth -= damage;
    }

    private void Die()
    {
        if (!isdead)
        {
            // Die animation before destroying object
            isdead = true;

            EnemyAnim.SetTrigger("Death");
            Destroy(gameObject, 3);
            Enemy.Stop(true);
            PlayerController.score.AddScore();
        }
    }
}
