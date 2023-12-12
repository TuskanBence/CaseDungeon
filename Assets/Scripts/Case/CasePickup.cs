using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The CasePickup class handles the logic when a player picks up a case.
/// </summary>
public class CasePickup : MonoBehaviour
{
    /// <summary>
    /// Called when another collider enters the trigger collider attached to this object.
    /// </summary>
    /// <param name="collision">The Collider2D that entered the trigger.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {


            // Get the Case component attached to this GameObject
            Case c = gameObject.GetComponent<Case>();

            // Insert the case into the grid
            InventoryController.instance.InsertCase(c);

            // Remove the case from the current room's list of cases
            RoomController.instance.getCurrentRoom().cases.Remove(c);

            // Add the case to the player's inventory
            Inventory.Instance.Add(c);
        }
    }
}
