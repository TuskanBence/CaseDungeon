using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>
/// Manages the inventory and item placement.
/// </summary>
public class InventoryController : MonoBehaviour
{
    /// <summary>
    /// The grid where the selected item is placed.
    /// </summary>
    [SerializeField] public ItemGrid selectedItemGrid;

    /// <summary>
    /// The UI element for displaying information.
    /// </summary>
    public TextMeshProUGUI info;

    /// <summary>
    /// The UI element for displaying the player's money.
    /// </summary>
    public TextMeshProUGUI money;

    /// <summary>
    /// The button for saving in the upgrade room.
    /// </summary>
    public Button saveButton;

    /// <summary>
    /// Static reference to the InventoryController instance.
    /// </summary>
    public static InventoryController instance;

    /// <summary>
    /// The selected case in the inventory.
    /// </summary>
    Case selectedCase;

    /// <summary>
    /// The case overlapping with the selected case.
    /// </summary>
    Case overlapCase;

    /// <summary>
    /// The case to highlight under the cursor.
    /// </summary>
    Case CaseToHighlight;

    /// <summary>
    /// The RectTransform component used for resizing and repositioning the selected case.
    /// </summary>
    RectTransform rectTransform;

    /// <summary>
    /// Indicates whether the inventory is currently active.
    /// </summary>
    bool InventoryActive;

    /// <summary>
    /// The transform of the canvas.
    /// </summary>
    [SerializeField] Transform canvasTranform;

    /// <summary>
    /// The highlighter used to indicate the selected or cursor-over case.
    /// </summary>
    InventoryHighligth inventoryHighligth;

    /// <summary>
    /// Called when the script instance is being loaded.
    /// Called before Start
    /// </summary>
    private void Awake()
    {
        // Ensure the instance is not destroyed when loading a new scene.
        DontDestroyOnLoad(this);

        // Set the instance if it's the first instance; otherwise, destroy it.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Get the InventoryHighligth component.
        inventoryHighligth = GetComponent<InventoryHighligth>();

        // The inventory is not active initially.
        InventoryActive = false;
    }

    /// <summary>
    /// Updates the inventory and money display.
    /// </summary>
    private void Update()
    {
        // Update the money display.
        UpdateMoney(Player.playerInstance.currentMoney);
        // Show or hide information based on the player's location.
        if (Player.playerInstance.isInShopArea)
        {
            info.gameObject.SetActive(true);
        }
        else
        {
            info.gameObject.SetActive(false);
        }

        // Show or hide the save button based on the player's location.
        if (Player.playerInstance.inUpgradeRoom)
        {
            saveButton.gameObject.SetActive(true);
        }
        else
        {
            saveButton.gameObject.SetActive(false);
        }

        // Toggle the inventory on Esc key press.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowInventory(InventoryActive);
        }

        // If the inventory is not active, return.
        if (!InventoryActive)
        {
            return;
        }

        // Update the item icon drag case functionality.
        ItemIconDragCase();

        // Check if the cursor is out of bounds and hide the highlighter accordingly.
        if (OutOfBounds())
        {
            inventoryHighligth.Show(false);
        }

        // Handle the highlighting of cases based on the cursor position.
        HandleHighlightCase();

        // Handle left mouse button press.
        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonPress();
        }
    }

    /// <summary>
    /// Updates the money display.
    /// </summary>
    /// <param name="amount">The current amount of money.</param>
    public void UpdateMoney(int amount)
    {
        money.text = "Money: " + amount;
    }

    /// <summary>
    /// Toggles the visibility of the inventory.
    /// </summary>
    /// <param name="inventoryActive">The new state of the inventory.</param>
    private void ShowInventory(bool inventoryActive)
    {
        InventoryActive = !InventoryActive;
        selectedItemGrid.gameObject.SetActive(InventoryActive);
    }

    /// <summary>
    /// Checks if the cursor is out of bounds on the inventory grid.
    /// </summary>
    /// <returns>true if the cursor is out of bounds; otherwise, false.</returns>
    public bool OutOfBounds()
    {
        Vector2Int positionOnGrid = GetTileGridPosition();
        return positionOnGrid.x > selectedItemGrid.gridSizeWidth - 1 || positionOnGrid.y > selectedItemGrid.gridSizeHeight - 1 || positionOnGrid.y < 0 || positionOnGrid.x < 0;
    }

    /// <summary>
    /// Changes the size of the specified RectTransform.
    /// </summary>
    /// <param name="rectTransform">The RectTransform to resize.</param>
    /// <param name="newWidth">The new width.</param>
    /// <param name="newHeight">The new height.</param>
    private void ChangeSize(RectTransform rectTransform, float newWidth, float newHeight)
    {
        if (rectTransform != null)
        {
            Canvas c = GetComponent<Canvas>();
            // Modify the sizeDelta property to change width and height.
            rectTransform.sizeDelta = new Vector2(newWidth*c.scaleFactor, newHeight* c.scaleFactor);
        }
        else
        {
            Debug.LogError("RectTransform is not assigned.");
        }
    }

    /// <summary>
    /// Inserts a case into the inventory grid.
    /// </summary>
    /// <param name="itemToInsert">The case to insert.</param>
    public void InsertCase(Case itemToInsert)
    {
        ChangeSize(itemToInsert.gameObject.GetComponent<RectTransform>(), 32f * (int)itemToInsert.caseSize, 32f * (int)itemToInsert.caseSize);
        //selectedCase = null;
        Vector2Int? posOnGrid = selectedItemGrid.FindSpaceForObject(itemToInsert);
        if (posOnGrid == null) { return; }
        selectedItemGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
    }

    /// <summary>
    /// Handles highlighting of the selected case or the case under the cursor.
    /// </summary>
    private void HandleHighlightCase()
    {
        // Get the position of the cursor on the grid.
        Vector2Int positionOnGrid = GetTileGridPosition();

        // If no case is selected, try highlighting the case under the cursor.
        if (selectedCase == null)
        {
            if (!OutOfBounds())
            {
                CaseToHighlight = selectedItemGrid.GetCase(positionOnGrid.x, positionOnGrid.y);
            }

            // Show or hide the highlighter based on whether a case is under the cursor.
            if (CaseToHighlight != null)
            {
                inventoryHighligth.Show(true);
                inventoryHighligth.SetSize(CaseToHighlight);
                inventoryHighligth.SetPosition(selectedItemGrid, CaseToHighlight);
            }
            else
            {
                inventoryHighligth.Show(false);
            }
        }
        // If a case is selected, show its highlighter and handle its positioning.
        else
        {
            inventoryHighligth.Show(selectedItemGrid.BoundryCheck(positionOnGrid.x, positionOnGrid.y, (int)selectedCase.caseSize, (int)selectedCase.caseSize));
            inventoryHighligth.SetSize(selectedCase);
            inventoryHighligth.SetPosition(selectedItemGrid, selectedCase, positionOnGrid.x, positionOnGrid.y);
        }
    }

    /// <summary>
    /// Handles left mouse button press actions.
    /// If already holding a case place it on the grid otherwise, pick up the case under the cursor
    /// </summary>
    private void LeftMouseButtonPress()
    {
        Vector2Int tileGridPosition = GetTileGridPosition();
        if (selectedCase == null)
        {
            PickUpCase(tileGridPosition);
        }
        else
        {
            PlaceCase(tileGridPosition);
        }
    }

    /// <summary>
    /// Gets the tile grid position based on the cursor position.
    /// </summary>
    /// <returns>The tile grid position.</returns>
    private Vector2Int GetTileGridPosition()
    {
        Vector2 position = Input.mousePosition;
       
       

        if (selectedCase != null)
        {
            position.x -= ((int)selectedCase.caseSize - 1) * ItemGrid.tileSizeWidht / 2;
            position.y += ((int)selectedCase.caseSize - 1) * ItemGrid.tileSizeHeight / 2;
        }
        
        return selectedItemGrid.GetTileGridPosition(position);
    }

    /// <summary>
    /// Places the selected case on the inventory grid.
    /// If clicked outside the grid remove case from grig
    /// </summary>
    /// <param name="tileGridPosition">The position on the tile grid.</param>
    private void PlaceCase(Vector2Int tileGridPosition)
    {
        if (OutOfBounds())
        {
            Vector2 dropPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedCase.gameObject.transform.SetParent(null);
            selectedCase.gameObject.transform.position = dropPosition;
            RoomController.instance.getCurrentRoom().cases.Add(selectedCase);
            Inventory.Instance.Remove(selectedCase);
            selectedItemGrid.CleanGridReference(selectedCase);
            selectedCase = null;

            return;
        }
        bool complete = selectedItemGrid.SuccessfulCasePlacement(selectedCase, tileGridPosition.x, tileGridPosition.y);

        if (complete)
        { 
            selectedCase = null;
        }
    }

    /// <summary>
    /// Picks up the case from the inventory grid.
    /// </summary>
    /// <param name="tileGridPosition">The position on the tile grid.</param>
    private void PickUpCase(Vector2Int tileGridPosition)
    {
        if (OutOfBounds())
        {
            return;
        }
        selectedCase = selectedItemGrid.PickUpCase(tileGridPosition.x, tileGridPosition.y);
        if (selectedCase != null)
        {
            rectTransform = selectedCase.GetComponent<RectTransform>();
        }
    }

    /// <summary>
    /// Drags the selected case icon based on the cursor position.
    /// </summary>
    private void ItemIconDragCase()
    {
        if (selectedCase != null)
        {
            rectTransform.position = Input.mousePosition;
        }
    }
}
