using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// The TutorialText class manages the display of tutorial text.
/// </summary>
public class TutorialText : MonoBehaviour
{
    /// <summary>
    /// Reference to the text component for displaying tutorial text.
    /// </summary>
    public TextMeshProUGUI text;

    /// <summary>
    /// Indicates whether the tutorial text is currently displayed.
    /// </summary>
    bool on = true;

    /// <summary>
    /// Toggles the visibility of the tutorial text.
    /// </summary>
    public void ShowTutorialText()
    {
        text.gameObject.SetActive(on);
        on = !on;
    }
}
