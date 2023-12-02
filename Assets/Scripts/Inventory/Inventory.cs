using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   public List<Case> cases = new List<Case>();
    public static Inventory Instance;
    private void Awake()
    {
        if (Instance!=null)
        {
            Debug.LogWarning("More than on einstance of inventory found");
            return;
        }
        Instance = this;
    }

    public void Add (Case c)
    {

        cases.Add(c);
    }
    public void Remove(Case c)
    {
        cases.Remove(c);
    }
}
