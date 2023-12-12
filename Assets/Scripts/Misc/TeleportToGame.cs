using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The TeleportToGame class handles teleporting the player to the main game.
/// </summary>
public class TeleportToGame : MonoBehaviour
{
    /// <summary>
    /// Called when another collider stays inside the trigger collider attached to this object.
    /// </summary>
    /// <param name="collision">The Collider2D that stayed in the trigger.</param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Check if the colliding object is the player and the "F" key is pressed
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Teleporting to the game");

            // Set the player's inUpgradeRoom flag to false
            Player.playerInstance.inUpgradeRoom = false;

            // Load the "BasementMain" scene
            SceneManager.LoadScene("BasementMain");
        }
    }
}
