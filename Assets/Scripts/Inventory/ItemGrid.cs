using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.UIElements;

/// <summary>
/// Represents the grid in the game.
/// </summary>
public class ItemGrid : MonoBehaviour
{
    /// <summary>
    /// The width of each tile in the grid.
    /// </summary>
    public const int tileSizeWidht = 32;

    /// <summary>
    /// The height of each tile in the grid.
    /// </summary>
    public const int tileSizeHeight = 32;

    /// <summary>
    /// The 2 dimensional array representing the inventory item slots.
    /// </summary>
    public Case[,] inventoryitemSlotCase;

    /// <summary>
    /// The RectTransform component of the item grid.
    /// </summary>
    RectTransform rectTransform;

    /// <summary>
    /// The width of the item grid.
    /// </summary>
    [SerializeField] public int gridSizeWidth;

    /// <summary>
    /// The height of the item grid.
    /// </summary>
    [SerializeField] public int gridSizeHeight;

    /// <summary>
    /// The starting position on the grid.
    /// </summary>
    Vector2 positionOnTheGrid = new Vector2();

    /// <summary>
    /// The tile grid position represented as a Vector2Int.
    /// </summary>
    Vector2Int tileGridPosition = new Vector2Int();

    /// <summary>
    /// Reference to the canvas the gird is in
    /// </summary>
    public Canvas canvas;

    /// <summary>
    /// Called when the object is enabled.
    /// </summary>
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        //Initializes the grid with gridSizeWidth and gridSizeHeight
        Init(gridSizeWidth, gridSizeHeight);
        this.gameObject.SetActive(false);
        //Puts all cases from player's inventory onto the grid
        foreach (Case current in Inventory.Instance.cases)
        {
            InventoryController.instance.InsertCase(current);
        }
    }

    /// <summary>
    /// Initializes the item grid with the specified width and height.
    /// </summary>
    /// <param name="width">The width of the grid.</param>
    /// <param name="height">The height of the grid.</param>
    public void Init(int width, int height)
    {
        inventoryitemSlotCase = new Case[width, height];
        Vector2 size = new Vector2(width * tileSizeWidht, height * tileSizeHeight);
        rectTransform.sizeDelta = size;
    }
 
    /// <summary>
    /// Gets the tile grid position based on the mouse position.
    /// </summary>
    /// <param name="mousePosition">The mouse position.</param>
    /// <returns>The tile grid position as a Vector2Int.</returns>
    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {


        positionOnTheGrid.x = (mousePosition.x - rectTransform.position.x) / canvas.scaleFactor;
        positionOnTheGrid.y = (rectTransform.position.y - mousePosition.y) / canvas.scaleFactor;

        tileGridPosition.x = (int)(positionOnTheGrid.x / tileSizeWidht);
        tileGridPosition.y = (int)(positionOnTheGrid.y / tileSizeHeight);
        return tileGridPosition;
    }

    /// <summary>
    /// Places an item on the item grid at the specified position.
    /// </summary>
    /// <param name="item">The item to place.</param>
    /// <param name="posX">The x-position on the grid.</param>
    /// <param name="posY">The y-position on the grid.</param>
    /// <param name="overlapItem">The item overlapping with the target position.</param>
    /// <returns>True if the item can be placed, false otherwise.</returns>
    public bool SuccessfulCasePlacement(Case item, int posX, int posY)
    {
        if (BoundryCheck(posX, posY, (int)item.caseSize, (int)item.caseSize) == false)
        {
            return false;
        }

        if (OverlapCheck(posX, posY, (int)item.caseSize, (int)item.caseSize) == false)
        {
            return false;
        }
        PlaceItem(item, posX, posY);
        return true;
    }

    /// <summary>
    /// Places an item on the item grid at the specified position.
    /// </summary>
    /// <param name="item">The item to place.</param>
    /// <param name="posX">The x-position on the grid.</param>
    /// <param name="posY">The y-position on the grid.</param>
    public void PlaceItem(Case item, int posX, int posY)
    {
        RectTransform rectTransform = item.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform);
        for (int x = 0; x < (int)item.caseSize; x++)
        {
            for (int y = 0; y < (int)item.caseSize; y++)
            {
                inventoryitemSlotCase[posX + x, posY + y] = item;
            }
        }
        item.onGridPositionX = posX;
        item.onGridPositionY = posY;

        Vector2 position = CalculatePositionOnGrid(item, posX, posY);

        rectTransform.localPosition = position;
    }

    /// <summary>
    /// Calculates the position of an item on the grid.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="posX">The x-position on the grid.</param>
    /// <param name="posY">The y-position on the grid.</param>
    /// <returns>The calculated position.</returns>
    public Vector2 CalculatePositionOnGrid(Case item, int posX, int posY)
    {
        Vector2 position = new Vector2();
        position.x = posX * tileSizeWidht + tileSizeWidht * (int)item.caseSize / 2;
        position.y = -(posY * tileSizeHeight + tileSizeHeight * (int)item.caseSize / 2);
        return position;
    }
    /// <summary>
    /// Checks if space is empty
    /// </summary>
    /// <param name="posX">The x-position on the grid.</param>
    /// <param name="posY">The y-position on the grid.</param>
    /// <param name="width">The width of the empty space needed.</param>
    /// <param name="height">The height of the empty space needed.</param>
    /// <returns>Returns true if item can be placed on location otherwise, false.</returns>
    private bool CheckAvaiableSpaceCase(int posX, int posY, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (inventoryitemSlotCase[posX + x, posY + y] != null) { return false; }
            }
        }
        return true;
    }
    /// <summary>
    /// Check if position is overlapping with another item
    /// </summary>
    /// <param name="posX">The x-position on the grid.</param>
    /// <param name="posY">The y-position on the grid.</param>
    /// <param name="width">The width of the empty space needed.</param>
    /// <param name="height">The height of the empty space needed.</param>
    /// <param name="overlapItem">The item overlapping with the target position.</param>
    /// <returns>Returns false if there is more than one item overlapping otherwise, true.</returns>
    private bool OverlapCheck(int posX, int posY, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (inventoryitemSlotCase[posX + x, posY + y] != null)
                {
                  
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    /// Picks up an item at the specified grid position.
    /// </summary>
    /// <param name="x">The x-position on the grid.</param>
    /// <param name="y">The y-position on the grid.</param>
    /// <returns>The picked up item.</returns>
    public Case PickUpCase(int x, int y)
    {
        Case toReturn = inventoryitemSlotCase[x, y];
        if (toReturn == null) { return null; }
        CleanGridReference(toReturn);
        return toReturn;
    }

    /// <summary>
    /// Cleans the grid reference of an item.
    /// </summary>
    /// <param name="item">The item to clean.</param>
    public void CleanGridReference(Case item)
    {
        for (int ix = 0; ix < (int)item.caseSize; ix++)
        {
            for (int iy = 0; iy < (int)item.caseSize; iy++)
            {
                inventoryitemSlotCase[item.onGridPositionX + ix, item.onGridPositionY + iy] = null;
            }
        }
    }
    /// <summary>
    /// Checks if position is inside the grid
    /// </summary>
    /// <param name="posX">The x-position on the grid.</param>
    /// <param name="posY">The y-position on the grid.</param>
    /// <returns>True if position is inside the grid otherwise, false.</returns>
    bool PositionCheck(int posX, int posY)
    {
        if (posX < 0 || posY < 0)
        {
            return false;
        }
        if (posX >= gridSizeWidth || posY >= gridSizeHeight)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Checks if the specified position is within the grid boundaries.
    /// </summary>
    /// <param name="posX">The x-position on the grid.</param>
    /// <param name="posY">The y-position on the grid.</param>
    /// <returns>True if the position is within the boundaries, false otherwise.</returns>
    public bool BoundryCheck(int posX, int posY, int width, int height)
    {
        if (!PositionCheck(posX, posY))
        {
            return false;
        }
        posX += width - 1;
        posY += height - 1;

        if (!PositionCheck(posX, posY))
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Gets the item at the specified grid position.
    /// </summary>
    /// <param name="x">The x-position on the grid.</param>
    /// <param name="y">The y-position on the grid.</param>
    /// <returns>The item at the specified position.</returns>
    public Case GetCase(int x, int y)
    {
        return inventoryitemSlotCase[x, y];
    }

    /// <summary>
    /// Finds a space for an object of the specified size in the grid.
    /// </summary>
    /// <param name="itemToInsert">The item to insert.</param>
    /// <returns>The position on the grid where the object can fit, or null if no space is available.</returns>
    public Vector2Int? FindSpaceForObject(Case itemToInsert)
    {
        int width = gridSizeWidth - (int)itemToInsert.caseSize + 1;
        int height = gridSizeHeight - (int)itemToInsert.caseSize + 1;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (CheckAvaiableSpaceCase(x, y, (int)itemToInsert.caseSize, (int)itemToInsert.caseSize))
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return null;
    }
}
