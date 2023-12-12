using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The ExitGame class handles exiting the game.
/// </summary>
public class ExitGame : MonoBehaviour
{
    /// <summary>
    /// Called when the "ExitGame" button is clicked.
    /// </summary>
    public void ExitGameClick()
    {
        // Quit the application
        Application.Quit();
    }
}
