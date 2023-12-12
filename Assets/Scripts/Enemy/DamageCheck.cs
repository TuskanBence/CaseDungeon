using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles checking for collisions that result in damage to the  enemy.
/// </summary>
public class DamageCheck : MonoBehaviour
{
    /// <summary>
    /// Called when the enemy collides with another collider.
    /// </summary>
    /// <param name="collision">Gameobject that the enemy collided with.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Attempt to get the Player component from the collided object
        Player player = collision.gameObject.GetComponent<Player>();

        // If the collided object has a Player component, invoke the DamageCollision method in the parent EnemyAI
        if (player != null)
        {
            gameObject.GetComponentInParent<EnemyAI>().DamageCollision(player);
        }
    }
}
