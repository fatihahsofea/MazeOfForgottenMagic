using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boulder"))
        {
            Debug.Log("Enemy hit by boulder!");
            TakeDamage(100); // or whatever value you like
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died");
        Destroy(gameObject);
    }
}