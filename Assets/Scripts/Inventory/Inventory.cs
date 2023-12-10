using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour,IDataPersistence
{
    public GameObject casePrefab;
    public List<Case> cases = new List<Case>();
    public static Inventory Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than on einstance of inventory found");
            Destroy(gameObject);
           
        }
        else
        {
            Instance = this;
        }
        
    }

    public void Add(Case c)
    {

        cases.Add(c);
    }
    public void Remove(Case c)
    {
        cases.Remove(c);
    }

   

    public Case CreateCase(CaseData current)
    {
        GameObject caseObject = Instantiate(casePrefab, new Vector2(0,0), Quaternion.identity);
        Case caseComponent = caseObject.GetComponent<Case>();
        caseComponent.Initialize(current);
        return caseComponent;
    }

    public void LoadData(GameData data)
    {
        foreach (CaseData currentCase in data.playerCases)
        {
            cases.Add(CreateCase(currentCase));
        }
    }

    public void SaveData(ref GameData data)
    {
        data.playerCases.Clear();
        foreach (Case currenCase in cases)
        {
            CaseData c = new CaseData();
            c.caseRarity=currenCase.caseRarity;
            c.caseValue=currenCase.caseValue;   
            c.caseWeight=currenCase.caseWeight; 
            c.caseSize=currenCase.caseSize;

            data.playerCases.Add(c);
        }
    }
}
