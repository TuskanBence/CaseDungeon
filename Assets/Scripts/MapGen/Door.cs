using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
  public enum DoorType
    {
        left,right, top, bottom
    }
    public DoorType doortype;
    public bool nowhere;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            Player p = collision.gameObject.GetComponent<Player>();
            switch (doortype)
            {
                case DoorType.bottom:
                  
                    break;
            }
        }
    }
}
