using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHighligth : MonoBehaviour
{
    [SerializeField] RectTransform highligther;
    public void Show (bool b)
    {
        highligther.gameObject.SetActive(b);
    }
    public void SetSize(Case targetItem)
    {
        Vector2 size = new Vector2();
        size.x = (int)targetItem.caseSize * ItemGrid.tileSizeWidht;
        size.y = (int)targetItem.caseSize * ItemGrid.tileSizeHeight;
        highligther.sizeDelta = size;
    }
    public void SetPosition(ItemGrid targetGrid, Case targetItem)
    {
        Debug.Log(targetItem.onGridPositionX);
        Debug.Log(targetItem.onGridPositionY);
        Vector2 pos = targetGrid.CalculatePositionOnGrid(targetItem, targetItem.onGridPositionX, targetItem.onGridPositionY);

        highligther.localPosition = pos;
    }

    public void SetParent(ItemGrid targetGrid)
    {
        if(targetGrid == null) { return; }
        highligther.SetParent(targetGrid.GetComponent<RectTransform>());
    }

    public void SetPosition(ItemGrid targetGrid, Case targetItem, int posX, int posY)
    {

        Vector2 pos = targetGrid.CalculatePositionOnGrid(targetItem, posX, posY);

        highligther.localPosition = pos;
    }
}
