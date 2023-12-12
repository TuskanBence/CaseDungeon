using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The TeleportToUpgrade class handles teleporting the player to the upgrade room.
/// </summary>
public class TeleportToUpgrade : MonoBehaviour
{
    /// <summary>
    /// Reference to the Room component.
    /// </summary>
    public Room room;

    /// <summary>
    /// Called when another collider stays inside the trigger collider attached to this object.
    /// </summary>
    /// <param name="collision">The Collider2D that stayed in the trigger.</param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Check if the colliding object is the player, the "F" key is pressed, and the room is cleared
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.F) && room.cleared)
        {
            // Resets the players position
            collision.gameObject.transform.position = new Vector2(0, 0);

            // Set the player's inUpgradeRoom variable to true
            Player.playerInstance.inUpgradeRoom = true;

            // Load the "UpgradeRoom" scene
            SceneManager.LoadScene("UpgradeRoom");
        }
    }
}
