using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public void onClick()
    {
        DataPresistenceManager.instance.SaveGame();
    }
}
