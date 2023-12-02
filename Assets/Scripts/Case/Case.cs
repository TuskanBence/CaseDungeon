using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
public class Case : MonoBehaviour
{
    public enum Rarity
    {
        common,
        uncommon,
        rare,
        legendary
    }
    public enum Size
    {
        S = 1,
        M = 2,
        L = 3,
        XL = 4
    }
    public int caseValue { get; set; }
    public float caseWeight { get; set; }
    public Size caseSize { get; set; }
    public Rarity caseRarity { get; set; }
    public int onGridPositionX { get; set; }
    public int onGridPositionY { get; set; }
    internal void Initialize(CaseData randomData)
    {
        this.caseValue = randomData.caseValue;
        this.caseSize = randomData.caseSize;
        this.caseRarity = randomData.caseRarity;
        this.caseWeight = randomData.caseWeight;
    }
}
