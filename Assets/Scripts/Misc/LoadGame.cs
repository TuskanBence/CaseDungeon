using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Class responsible for starting a new game or loading from a save file.
/// </summary>
public class LoadGame : MonoBehaviour
{
    /// <summary>
    /// Reference to the TextMeshProUGUI component for displaying UI text.
    /// </summary>
    public TextMeshProUGUI text;

    /// <summary>
    /// Update is called once per frame.
    /// Checks if the save file exists and updates the UI text accordingly.
    /// </summary>
    private void Update()
    {
        // Construct the full path to the save file
        string fullPath = Path.Combine(Application.persistentDataPath, "saveGame.game");

        // Check if the file exists
        if (File.Exists(fullPath))
        {
            // Save file exists, display "Load Game" text
            text.text = "Load Game";
        }
        else
        {
            // Save file does not exist, display "New Game" text
            text.text = "New Game";
        }
    }

    /// <summary>
    /// Called when the "Load Game" button is clicked.
    /// Loads the specified scene ("BasementMain") to start or continue the game.
    /// </summary>
    public void LoadGameOnClick()
    {
        SceneManager.LoadScene("BasementMain");
    }
}
