using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Rigidbody2D rb;
    public Animator anim;
    public Healthbar healthbar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20);
            
        }
    }

    public void TakeDamage(int damage)
    {
        if(currentHealth <= 0)
        {
            rb.bodyType = RigidbodyType2D.Static;
            anim.SetTrigger("death");
        }

        else
        {
            currentHealth -= damage;
            healthbar.SetHealth(currentHealth);
        }
    }
}
