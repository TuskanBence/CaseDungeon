using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a highlighter for inventory items.
/// </summary>
public class InventoryHighligth : MonoBehaviour
{
    /// <summary>
    /// The RectTransform of the highlighter.
    /// </summary>
    [SerializeField] RectTransform highligther;

    /// <summary>
    /// Shows or hides the highlighter.
    /// </summary>
    /// <param name="show">True to show the highlighter, false to hide it.</param>
    public void Show(bool show)
    {
        highligther.gameObject.SetActive(show);
    }

    /// <summary>
    /// Sets the size of the highlighter based on the target item's size.
    /// </summary>
    /// <param name="targetItem">The target item.</param>
    public void SetSize(Case targetItem)
    {
        Vector2 size = new Vector2();
        size.x = (int)targetItem.caseSize * ItemGrid.tileSizeWidht;
        size.y = (int)targetItem.caseSize * ItemGrid.tileSizeHeight;
        highligther.sizeDelta = size;
    }

    /// <summary>
    /// Sets the position of the highlighter based on the target grid and item.
    /// </summary>
    /// <param name="targetGrid">The target grid.</param>
    /// <param name="targetItem">The target item.</param>
    public void SetPosition(ItemGrid targetGrid, Case targetItem)
    {
        Vector2 pos = targetGrid.CalculatePositionOnGrid(targetItem, targetItem.onGridPositionX, targetItem.onGridPositionY);
        highligther.localPosition = pos;
    }

    /// <summary>
    /// Sets the parent of the highlighter to the specified grid.
    /// </summary>
    /// <param name="targetGrid">The target grid.</param>
    public void SetParent(ItemGrid targetGrid)
    {
        if (targetGrid == null) { return; }
        highligther.SetParent(targetGrid.GetComponent<RectTransform>());
    }

    /// <summary>
    /// Sets the position of the highlighter based on the target grid, item, and specified position.
    /// </summary>
    /// <param name="targetGrid">The target grid.</param>
    /// <param name="targetItem">The target item.</param>
    /// <param name="posX">The x-position.</param>
    /// <param name="posY">The y-position.</param>
    public void SetPosition(ItemGrid targetGrid, Case targetItem, int posX, int posY)
    {
        Vector2 pos = targetGrid.CalculatePositionOnGrid(targetItem, posX, posY);
        highligther.localPosition = pos;
    }
}
