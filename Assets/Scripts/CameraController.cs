using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class CameraController : MonoBehaviour

{
    public static CameraController instance;
    public Room currentRoom;
    public float cameraChangeSpeed;
    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        UpdatePosition();
    }
    //Moves the camera to the needed position
    void UpdatePosition()
    {
        if (currentRoom == null)
        {
            return;
        }

        Vector3 targetPosition = GetCameraTargetPosition();
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * cameraChangeSpeed);
    }
    //Returns the cameras needed position based on the room the player is in
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

    public bool isSwitchingScene()
    {
        return transform.position.Equals(GetCameraTargetPosition()) == false;
    }
}
