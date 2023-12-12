using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The CaseData class represents data for a case.
/// </summary>
[System.Serializable]
public class CaseData
{
    /// <summary>
    /// The value associated with the case.
    /// </summary>
    public int caseValue;

    /// <summary>
    /// The weight of the case.
    /// </summary>
    public int caseWeight;

    /// <summary>
    /// The size of the case.
    /// </summary>
    public Case.Size caseSize;

    /// <summary>
    /// The rarity of the case.
    /// </summary>
    public Case.Rarity caseRarity;
}
