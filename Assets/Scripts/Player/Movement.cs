using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Movement class handles the player character's movement.
/// </summary>
public class Movement : MonoBehaviour
{
    /// <summary>
    /// Reference to the Rigidbody2D component attached to the player.
    /// </summary>
    public Rigidbody2D body;

    /// <summary>
    /// The walking speed of the player.
    /// </summary>
    public float walkSpeed;

    /// <summary>
    /// The direction in which the player is moving.
    /// </summary>
    Vector2 direction;

    /// <summary>
    /// Called every frame.
    /// </summary>
    void Update()
    {
        // Update the movement direction based on user input
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
    }

    /// <summary>
    /// Called at a fixed interval.
    /// </summary>
    private void FixedUpdate()
    {
        // Move the player based on the direction and speed
        body.MovePosition(body.position + direction * walkSpeed * Time.fixedDeltaTime);
    }
}
