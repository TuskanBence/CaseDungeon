using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

/// <summary>
/// The CameraController class manages the position of the camera based on the current room.
/// </summary>
public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Static reference to the CameraController instance.
    /// </summary>
    public static CameraController instance;

    /// <summary>
    /// The current room the player is in.
    /// </summary>
    public Room currentRoom;

    /// <summary>
    /// The speed at which the camera changes its position.
    /// </summary>
    public float cameraChangeSpeed;

    /// <summary>
    /// Called when the script instance is being loaded.
    /// Called before Start
    /// </summary>
    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Called every frame.
    /// </summary>
    void Update()
    {
        UpdatePosition();
    }

    /// <summary>
    /// Moves the camera to the needed position.
    /// </summary>
    void UpdatePosition()
    {
        if (currentRoom == null)
        {
            return;
        }

        Vector3 targetPosition = GetCameraTargetPosition();
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * cameraChangeSpeed);
    }

    /// <summary>
    /// Returns the camera's needed position based on the room the player is in.
    /// </summary>
    Vector3 GetCameraTargetPosition()
    {
        if (currentRoom == null)
        {
            return Vector3.zero;
        }
        Vector3 targetPosition = currentRoom.GetRoomCenter();
        targetPosition.z = transform.position.z;
        return targetPosition;
    }

    /// <summary>
    /// Checks if the camera is currently switching scenes.
    /// </summary>
    /// <returns>True if the camera is switching scenes, false otherwise.</returns>
    public bool isSwitchingScene()
    {
        return transform.position.Equals(GetCameraTargetPosition()) == false;
    }
}
