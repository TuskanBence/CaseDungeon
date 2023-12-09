using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IDataPersistence
{
    public GameObject casePrefab;
    public List<Case> cases = new List<Case>();
    public static Inventory Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than on einstance of inventory found");
            return;
        }
        Instance = this;
    }

    public void Add(Case c)
    {

        cases.Add(c);
    }
    public void Remove(Case c)
    {
        cases.Remove(c);
    }

    public void LoadData(GameData data)
    {
        if (data.fromShop)
        {
            return;
        }
        else
        {
            foreach (CaseData current in data.playerCases)
            {
                Case caseComponent = CreateCase(current, new Vector2(0, 0));
                cases.Add(caseComponent);
            }
        }

    }

    public Case CreateCase(CaseData current,Vector2 location)
    {
        GameObject caseObject = Instantiate(casePrefab, location, Quaternion.identity);
        Case caseComponent = caseObject.GetComponent<Case>();
        caseComponent.Initialize(current);
        caseComponent.transformX = location.x;
        caseComponent.transformY = location.y;
        return caseComponent;
    }

    public void SaveData(ref GameData data)
    {
        data.playerCases.Clear();
        foreach (Case currentCase in cases)
        {
            CaseData randomCase = new CaseData();
            randomCase.caseValue = currentCase.caseValue;
            randomCase.caseWeight = currentCase.caseWeight;
            randomCase.caseSize = currentCase.caseSize;
            randomCase.caseRarity = currentCase.caseRarity;
            data.playerCases.Add(randomCase);
        }
    }
}
