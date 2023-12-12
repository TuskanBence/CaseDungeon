using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a door in the game.
/// </summary>
public class Door : MonoBehaviour
{
    /// <summary>
    /// Enum representing different types of doors.
    /// </summary>
    public enum DoorType
    {
        left, right, top, bottom
    }

    /// <summary>
    /// Gets or sets the type of the door.
    /// </summary>
    public DoorType doortype;

    /// <summary>
    /// Gets or sets a value indicating if the door leads to nowhere.
    /// </summary>
    public bool nowhere;
}
