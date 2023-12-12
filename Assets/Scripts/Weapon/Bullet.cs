using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Bullet class represents a projectile fired by a weapon.
/// </summary>
public class Bullet : MonoBehaviour
{
    /// <summary>
    /// Gets or sets the damage done by the bullet.
    /// </summary>
    public int damage { get; set; }

    /// <summary>
    /// Called when the bullet is instantiated.
    /// </summary>
    private void Start()
    {
        // Set the damage of the bullet to the player's damage value
        damage = Player.playerInstance.damage;
    }

    /// <summary>
    /// Called when the bullet collides with another collider.
    /// </summary>
    /// <param name="collision">Gameobject that the bullet collided with.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy the bullet on collision
        Destroy(this.gameObject);
    }
}
