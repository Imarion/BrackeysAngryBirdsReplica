using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{

    public float health = 4f; // Actually velocity at which collision is managed.
    public GameObject deathEffect;

    public static int EnemiesAlive = 0;

    // Start is called before the first frame update
    void Start()
    {
        EnemiesAlive++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > health)
        {
            Die();
        }

        //Debug.Log(collision.relativeVelocity.magnitude);
    }

    private void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        EnemiesAlive--;
        if (EnemiesAlive <= 0)
        {
            Debug.Log("Level won");
        }

        Destroy(gameObject);
    }
}
