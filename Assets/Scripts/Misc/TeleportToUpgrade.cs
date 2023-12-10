using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToUpgrade : MonoBehaviour
{
    public Room room;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.F)&& room.cleared)
        {
            collision.gameObject.transform.position = new Vector2(0, 0);
            Player.playerInstance.inUpgradeRoom = true;
            SceneManager.LoadScene("UpgradeRoom");
           
        }
    }
}

