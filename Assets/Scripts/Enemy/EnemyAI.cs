using System;
using UnityEngine;

/// <summary>
/// Handles the behavior of an enemy AI, including movement, health, and interactions with the player.
/// </summary>
public class EnemyAI : MonoBehaviour
{
    /// <summary>
    /// Speed of the enemy.
    /// </summary>
    public float speed = 3f;

    /// <summary>
    /// Reference to the player's transform for tracking.
    /// </summary>
    public Transform player;

    /// <summary>
    /// Reference to the health bar.
    /// </summary
    public Transform healtBar;

    /// <summary>
    /// Reference to the Rigidbody2D component attached to the enemy.
    /// </summary>
    private Rigidbody2D enemyRigidbody;

    /// <summary>
    /// Reference to the room that the enemy is in.
    /// </summary>
    private Room room;


    /// <summary>
    /// Shows if the player is in range.
    /// </summary>
    private bool playerInRange = false;


     /// <summary>
     /// The damage dealt by the enemy.
     /// </summary>
    public int damage = 5;


    /// <summary>
    /// The current health of the player.
    /// </summary>
    public int currentHealth { get; set; }

    /// <summary>
    /// The maximum health of the enemy.
    /// </summary>
    public int maxHealth = 10;

    /// <summary>
    /// Called when the object is enabled.
    /// </summary>
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }
    /// <summary>
    /// Handles collision with player and reduces player's health.
    /// </summary>
    /// <param name="player">The player instance that collided with the enemy.</param>
    public void DamageCollision(Player player)
    {
        if (player == null)
        {
            return;
        }
        player.TakeDamage(damage);
        player.PushPlayer(this);
    }
    /// <summary>
    /// Handles collision with bullets and reduces enemy's health.
    /// </summary>
    /// <param name="bullet">The bullet instance that collided with the enemy.</param>
    public void BulletCollison(Bullet bullet)
    {
        TakeDamage(bullet.damage);
    }

    /// <summary>
    /// Enemy takes damage.
    /// </summary>
    /// <param name="damage">The amount of damage the enemy takes.</param>
    private void TakeDamage(int damage)
    {
            currentHealth -= damage;
        UpdateHealthBar();
        if (currentHealth <= 0)
            {
                Die();
            }
    }

    /// <summary>
    ///  Updates the healtBars current value to the current health of the enemy.
    /// </summary>
    private void UpdateHealthBar()
    {
        float healthPercentage = (float)currentHealth / maxHealth;
       healtBar.transform.localScale = new Vector3(5*healthPercentage, 1f, 1f);
    }

    /// <summary>
    /// Handles the enemy's death.
    /// </summary>
    private void Die()
    {
        if (room!=null)
        {
            room.removeEnemy(this);
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// Called every frame.
    /// </summary>
    void Update()
    {
        // If the player is in range, move towards the player
        if (playerInRange && player!=null)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            // Move the enemy towards the player using the Rigidbody
            enemyRigidbody.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }
    }
    /// <summary>
    ///  Set the room the enemy is in
    /// </summary>
    /// <param name="room">Room that the enemy is in</param>
    internal void setRoom(Room room)
    {
        this.room = room;
    }

    /// <summary>
    ///  Set the player
    /// </summary>
    /// <param name="p">Player thats set</param>
    internal void setPlayer(Transform p)
    {
        this.player = p.transform;
        playerInRange = true;
    }
}
