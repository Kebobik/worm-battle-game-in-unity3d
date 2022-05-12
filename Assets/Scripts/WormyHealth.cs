using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WormyHealth : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public Text txtHealth;

	// Use this for initialization
	void Start ()
    {
        health = maxHealth;
        txtHealth.text = health.ToString();
    }

    public void ChangeHealth(int change)
    {
        health += change;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        txtHealth.text = health.ToString();
     }
    void Update()
    {
        if (health <= 0)
        {
            WormyManager.isGameOver = true;
            gameObject.SetActive(false);
        }
    }
    
     private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Border")
        {
            WormyManager.isGameOver = true;
            gameObject.SetActive(false);
        }
    }
}
