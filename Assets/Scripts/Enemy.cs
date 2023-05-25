using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    [SerializeField] private int maxHealth = 20;
    [SerializeField] private AudioSource EnemyDeath;
    [SerializeField] private AudioSource EnemyHurt;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //hurt animation
        anim.SetTrigger("hurt");
        if(currentHealth <= 0)
        {
            Die();
        }
        else
        {
            EnemyHurt.Play();
        }
    }

    private void Die()
    {
        //die animation
        anim.SetBool("isDead", true);
        EnemyDeath.Play();
        //diable the enemy
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<EnemyMovement>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        this.enabled = false;
    }
}
