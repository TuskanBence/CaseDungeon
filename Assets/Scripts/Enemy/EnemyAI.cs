using System;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 3f; // Adjust the speed as needed
    public Transform player; // Reference to the player's 
    private Rigidbody2D enemyRigidbody;
    private Room room;
    private bool playerInRange = false; // Flag to indicate if the player is in range
    public int damage = 5;
    public int currentHealth { get; set; }
    public int maxHealth { get; set; }

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        maxHealth = 10;
        currentHealth = maxHealth;
    }

   public void RangeCollision()
    {
       
        playerInRange = true;
    }

  public  void DamageCollision(Player player)
    {
        player.TakeDamage(damage);
        player.PushPlayer(this);
    }
    public void BulletCollison(Bullet bullet)
    {
        TakeDamage(bullet.damage);
    }

    private void TakeDamage(int damage)
    {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
    }

    private void Die()
    {
        room.removeEnemy(this);
        Destroy(gameObject);
    }

    void Update()
    {
        // If the player is in range, move towards the player
        if (playerInRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            // Move the enemy towards the player using the Rigidbody
            enemyRigidbody.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }
    }

    internal void setRoom(Room room)
    {
        this.room = room;
    }
}
