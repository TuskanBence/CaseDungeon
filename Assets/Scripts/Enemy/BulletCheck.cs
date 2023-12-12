using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles checking for collisions with bullets and triggers corresponding actions in the parent EnemyAI.
/// </summary>
public class BulletCheck : MonoBehaviour
{
    /// <summary>
    /// Called when the enemy collides with another collider.
    /// </summary>
    /// <param name="collision">Gameobject that the enemy collided with.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Attempt to get the Bullet component from the collided object
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();

        // If the collided object has a Bullet component, invoke the BulletCollison method in the parent EnemyAI
        if (bullet != null)
        {
            gameObject.GetComponentInParent<EnemyAI>().BulletCollison(bullet);
        }
    }
}
