using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
   public TextMeshProUGUI text;
    private void Update()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, "saveGame.game");
        if (File.Exists(fullPath))
        {
            text.text = "Load Game";
        }
        else
        {
            text.text = "New Game";
        }
    }
    public void LoadGameOnClick()
    {
        SceneManager.LoadScene("BasementMain");
    }
}
