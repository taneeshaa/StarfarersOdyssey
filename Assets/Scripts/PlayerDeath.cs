using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private PlayerHealth PlayerHealth;

    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private AudioSource hurtSoundEffect;


    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        PlayerHealth = GetComponent<PlayerHealth>();
    }
    
    private void TakeDamage(int damage)
    {
        if (PlayerHealth.currentHealth <= 0)
        {
            Die();
            
        }

        else
        {
            PlayerHealth.currentHealth -= damage;
            PlayerHealth.healthbar.SetHealth(PlayerHealth.currentHealth);
            hurtSoundEffect.Play();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage(30);
            //Die();
        }

        else if (collision.gameObject.CompareTag("DeathTrap"))
        {
            Die();
        }
    }

    private void Die()
    {
        deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");

    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
