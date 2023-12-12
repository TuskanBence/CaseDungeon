using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Weapon class represents a weapon, capable of shooting projectiles at enemies.
/// </summary>
public class Weapon : MonoBehaviour
{
    /// <summary>
    /// The fire rate of the weapon
    /// </summary>
    [SerializeField] public float FireRate;

    /// <summary>
    /// The time when weapon can fire again.
    /// </summary>
    private float nextTimeToFire = 0f;

    /// <summary>
    /// The target the weapon is aiming at.
    /// </summary>
    [SerializeField] public Transform target;

    /// <summary>
    /// The bullet prefab to be shot by the weapon.
    /// </summary>
    [SerializeField] public GameObject bullet;

    /// <summary>
    /// The point where bullets are shoot from.
    /// </summary>
    [SerializeField] public Transform shootPoint;

    /// <summary>
    /// The force with which the bullets are shot.
    /// </summary>
    [SerializeField] public float force;

    /// <summary>
    /// The Player this weapon is atatched to.
    /// </summary>
    private Player p;
    /// <summary>
    /// Shows if enemy is in weapons range.
    /// </summary>
    private bool Detected = false;
    /// <summary>
    /// Direction the enemy is in.
    /// </summary>
    private Vector2 direction;

    /// <summary>
    /// The layer mask specifying the target layer for detecting enemies.
    /// </summary>
    public LayerMask targetLayer;

    /// <summary>
    /// Called every frame.
    /// </summary>
    void Update()
    {
        // If player instance is not set, set it and return
        if (p == null)
        {
            p = Player.playerInstance;
            return;
        }

        // Detects enemies in the Player's range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, p.getRange(), targetLayer);
     
        // Finds the closest enemy
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var collider in colliders)
        {
            float distance = Vector2.Distance(transform.position, collider.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = collider.transform;
            }
        }

        if (closestEnemy != null)
        {
            // Calculate direction of the closest enemy
            direction = (Vector2)closestEnemy.position - (Vector2)transform.position;

            // Raycast to check if there are obstacles between the weapon and the enemy
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, p.getRange(), targetLayer);

            if (hit)
            {
                // If the hit object is on the target layer (enemy), mark as detected
                if (hit.collider.gameObject.layer == 10)
                {
                    if (!Detected)
                    {
                        Detected = true;
                    }
                }
            }
            else
            {
                // If no hit, mark as not detected
                if (Detected)
                {
                    Detected = false;
                }
            }

            // If detected, shoot at the enemy
            if (Detected)
            {
                if (Time.time > nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1 / FireRate;
                    shoot();
                }
            }
        }
    }

    /// <summary>
    /// Shoots a bullet in the direction of the detected enemy.
    /// </summary>
    private void shoot()
    {
        GameObject bulletIns = Instantiate(bullet, shootPoint.position, Quaternion.identity);
        bulletIns.GetComponent<Rigidbody2D>().AddForce(direction * force);
    }
}
