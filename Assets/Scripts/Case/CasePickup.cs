using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasePickup : MonoBehaviour   
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Case c = gameObject.GetComponent<Case>(); 
        InventoryController.instance.InsertCaseS(c);
       
       // Inventory.Instance.Add(c);
       // Destroy(gameObject);
    }
}
