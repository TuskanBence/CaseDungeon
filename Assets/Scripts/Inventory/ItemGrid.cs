using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class ItemGrid : MonoBehaviour
{
    public const int  tileSizeWidht = 32;
   public  const int  tileSizeHeight = 32;

   public Case[,] inventoryitemSlotCase;
    RectTransform rectTransform;
    [SerializeField] public int gridSizeWidth;
    [SerializeField] public int gridSizeHeight;

    [SerializeField] GameObject inventoryItemPrefab;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(gridSizeWidth, gridSizeHeight);
        this.gameObject.SetActive(false);
    }
    public void Init(int width,int height)
    {
        inventoryitemSlotCase = new Case[width,height];
        Vector2 size = new Vector2(width*tileSizeWidht,height*tileSizeHeight);
        rectTransform.sizeDelta = size;
    }

    Vector2 positionOnTheGrid = new Vector2 (); 
    Vector2Int tileGridPosition  = new Vector2Int (); 
    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        positionOnTheGrid.x = mousePosition.x-rectTransform.position.x;
        positionOnTheGrid.y = rectTransform.position.y- mousePosition.y;

        tileGridPosition.x = (int)(positionOnTheGrid.x / tileSizeWidht);
        tileGridPosition.y = (int)(positionOnTheGrid.y / tileSizeHeight);

        return tileGridPosition;

    }
    public bool PlaceItem(Case item, int posX, int posY, ref Case overlapItem)
    {
        if (BoundryCheck(posX, posY,(int)item.caseSize, (int)item.caseSize) == false)
        {
            return false;
        }


        if (OverlapCheck(posX, posY, (int)item.caseSize, (int)item.caseSize, ref overlapItem) == false)
        {
            overlapItem = null;
            return false;
        }

        if (overlapItem != null) { CleanGridReference(overlapItem); }
        PlaceItem(item, posX, posY);
        return true;
    }
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
    public Vector2 CalculatePositionOnGrid(Case item, int posX, int posY)
    {
        Vector2 position = new Vector2();
        position.x = posX * tileSizeWidht+tileSizeWidht*(int)item.caseSize/2 ;
        position.y = -(posY * tileSizeHeight +tileSizeHeight*(int)item.caseSize/2);
        return position;

        /**/
    }
    private bool CheckAvaiableSpaceCase(int posX, int posY, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Debug.Log(inventoryitemSlotCase);
                if (inventoryitemSlotCase[posX + x, posY + y] != null) { return false; }
            }

        }
        return true;
    }
    private bool OverlapCheck(int posX, int posY, int width, int height, ref Case overlapItem)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
              
                if (inventoryitemSlotCase[posX + x, posY + y] != null)
                {
                    overlapItem = inventoryitemSlotCase[posX + x, posY + y];
                }
                else
                {
                    if (overlapItem != inventoryitemSlotCase[posX + x, posY + y])
                    {
                        return false;
                    }
                }
            }

        }
        return true;
    }
    public Case PickUpCase(int x, int y)
    {      
        Case toReturn = inventoryitemSlotCase[x, y];
        if (toReturn == null) { return null; }
        CleanGridReference(toReturn);
        return toReturn;
    }
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
    bool PositionCheck(int posX,int posY)
    {
        if (posX<0||posY<0)
        {
            return false;
        }
        if (posX >= gridSizeWidth || posY >=gridSizeHeight)
        {
            return false;
        }

        return true;
    }

    public bool BoundryCheck(int posX,int posY,int width, int height)
    {

        if (!PositionCheck(posX, posY))
        {
            return false;
        }
        posX += width-1;
        posY += height-1;

        if (!PositionCheck(posX, posY))
        {
            return false;
        }

        return true;
    }
        public Case GetCase(int x, int y)
        {
            return inventoryitemSlotCase[x, y];
        }
    public Vector2Int? FindSpaceForObject(Case itemToInsert)
    {
        int width = gridSizeWidth - (int)itemToInsert.caseSize + 1;
        int height = gridSizeHeight - (int)itemToInsert.caseSize + 1;
        for (int x = 0; x <width;  x++)
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
