using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;



public class InventoryController : MonoBehaviour
{
    [SerializeField] public ItemGrid selectedItemGrid;
    public TextMeshProUGUI info;
    public TextMeshProUGUI money;
    public Button saveButton;
    public static InventoryController instance;
    Case selectedCase;

    Case overlapCase;

    Case CaseToHighlight;
    RectTransform rectTransform;

    bool InventoryActive;


    [SerializeField] Transform canvasTranform;

    InventoryHighligth inventoryHighligth;



    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        inventoryHighligth = GetComponent<InventoryHighligth>();
        InventoryActive = false;

    }
    private void Update()
    {
        UpdateMoney(Player.playerInstance.currentMoney);
        if (Player.playerInstance.isInShopArea)
        {
            info.gameObject.SetActive(true);
        }
        else
        {
            info.gameObject.SetActive(false);
        }
        if (Player.playerInstance.inUpgradeRoom)
        {
            saveButton.gameObject.SetActive(true);
        }
        else
        { saveButton.gameObject.SetActive(false);}
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowInventory(InventoryActive);

        }
        if (!InventoryActive)
        {
            return;
        }
        ItemIconDragCase();
        if (OutOfBounds())
        {
            inventoryHighligth.Show(false);
        }
        HandleHighlightCase();
        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonPress();
        }
    }

    public void UpdateMoney(int amount)
    {
        money.text = "Money: " + amount;
    }
    private void ShowInventory(bool inventoryActive)
    {
        InventoryActive = !InventoryActive;
        selectedItemGrid.gameObject.SetActive(InventoryActive);
    }

    public bool OutOfBounds()
    {
        Vector2Int positionOnGrid = GetTileGridPosition();
        return positionOnGrid.x > selectedItemGrid.gridSizeWidth - 1 || positionOnGrid.y > selectedItemGrid.gridSizeHeight - 1 || positionOnGrid.y < 0 || positionOnGrid.x < 0;
    }
    public void InsertCaseS(Case insert)
    {

        Case itemToInsert = insert;
        ChangeSize(itemToInsert.gameObject.GetComponent<RectTransform>(), 32f * (int)itemToInsert.caseSize, 32f * (int)itemToInsert.caseSize);
        selectedCase = null;
        InsertCase(insert);
    }
    private void ChangeSize(RectTransform rectTransform, float newWidth, float newHeight)
    {
        if (rectTransform != null)
        {
            // Modify the sizeDelta property to change width and height
            rectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }
        else
        {
            Debug.LogError("RectTransform is not assigned.");
        }
    }
    public void InsertCase(Case itemToInsert)
    {
        Vector2Int? posOnGrid = selectedItemGrid.FindSpaceForObject(itemToInsert);
        if (posOnGrid == null) { return; }
        selectedItemGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
    }

    private void HandleHighlightCase()
    {
        Vector2Int positionOnGrid = GetTileGridPosition();
        if (selectedCase == null)
        {
            if (!OutOfBounds())
            {
                CaseToHighlight = selectedItemGrid.GetCase(positionOnGrid.x, positionOnGrid.y);
            }
            if (CaseToHighlight != null)
            {
                // inventoryHighligth.SetParent(selectedItemGrid);
                inventoryHighligth.Show(true);
                inventoryHighligth.SetSize(CaseToHighlight);
                inventoryHighligth.SetPosition(selectedItemGrid, CaseToHighlight);
            }
            else
            {
                inventoryHighligth.Show(false);
            }
        }
        else
        {
            // inventoryHighligth.SetParent(selectedItemGrid);
            inventoryHighligth.Show(selectedItemGrid.BoundryCheck(positionOnGrid.x, positionOnGrid.y, (int)selectedCase.caseSize, (int)selectedCase.caseSize));
            inventoryHighligth.SetSize(selectedCase);
            inventoryHighligth.SetPosition(selectedItemGrid, selectedCase, positionOnGrid.x, positionOnGrid.y);
        }
    }
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
    private void PlaceCase(Vector2Int tileGridPosition)
    {
        if (OutOfBounds())
        {

            Vector2 dropPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedCase.gameObject.transform.SetParent(null);
            selectedCase.gameObject.transform.position = dropPosition;
            RoomController.instance.getCurrentRoom().cases.Add(selectedCase);
            Inventory.Instance.Remove(selectedCase);
            selectedCase = null;

            return;
        }
        bool complete = selectedItemGrid.PlaceItem(selectedCase, tileGridPosition.x, tileGridPosition.y, ref overlapCase);

        if (complete)
        {
            selectedCase = null;
            if (overlapCase != null)
            {
                selectedCase = overlapCase;
                overlapCase = null;
                rectTransform = selectedCase.GetComponent<RectTransform>();
            }
        }
    }
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
    private void ItemIconDragCase()
    {
        if (selectedCase != null)
        {
            rectTransform.position = Input.mousePosition;
        }

    }
}
