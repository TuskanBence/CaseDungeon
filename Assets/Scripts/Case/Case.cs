using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

/// <summary>
/// The Case class represents an in-game case object.
/// </summary>
public class Case : MonoBehaviour
{
    /// <summary>
    /// Enum defining different rarities for the case.
    /// </summary>
    public enum Rarity
    {
        common = 1,
        uncommon = 2,
        rare = 3,
        legendary = 4
    }

    /// <summary>
    /// Enum defining different sizes for the case.
    /// </summary>
    public enum Size
    {
        S = 1,
        M = 2,
        L = 3,
        XL = 4
    }

    /// <summary>
    /// Gets or sets the value associated with the case.
    /// </summary>
    public int caseValue { get; set; }

    /// <summary>
    /// Gets or sets the weight of the case.
    /// </summary>
    public int caseWeight { get; set; }

    /// <summary>
    /// Gets or sets the size of the case.
    /// </summary>
    public Size caseSize { get; set; }

    /// <summary>
    /// Gets or sets the rarity of the case.
    /// </summary>
    public Rarity caseRarity { get; set; }

    /// <summary>
    /// Gets or sets the X-axis position on the grid.
    /// </summary>
    public int onGridPositionX { get; set; }

    /// <summary>
    /// Gets or sets the Y-axis position on the grid.
    /// </summary>
    public int onGridPositionY { get; set; }

    /// <summary>
    /// Initializes the case with data from parameter
    /// </summary>
    /// <param name="randomData">The CaseData variable to initialize the case with.</param>
    internal void Initialize(CaseData randomData)
    {
        // Set case attributes based on the provided randomData.
        this.caseValue = randomData.caseValue;
        this.caseSize = randomData.caseSize;
        this.caseRarity = randomData.caseRarity;
        this.caseWeight = randomData.caseWeight;
    }
}
