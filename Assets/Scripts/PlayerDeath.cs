using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private AudioSource deathSoundEffect;    

    private void Start()
    {
        //currentHealth = maxHealth;
        //healthbar.SetMaxHealth(maxHealth);
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20);


        }*/
    }

    
    /*public void TakeDamage(int damage)
    {
        if (currentHealth <= 0)
        {
            rb.bodyType = RigidbodyType2D.Static;
            anim.SetTrigger("death");
        }

        else
        {
            Die();
        }
    }*/


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            //TakeDamage(20);
            Die();

        }
    }


    /*private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");

    }*/
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
