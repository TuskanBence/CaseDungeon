using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SellAll : MonoBehaviour
{
    public TextMeshProUGUI valueText;
    public int sum;
    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(ButtonPressed);
        sum = 0;
        valueText.text = "Money: " + sum;
    }
    public void ButtonPressed()
    {
        for (int i = 0; i < InventoryController.instance.selectedItemGrid.gridSizeWidth; i++)
        {
            for (int j = 0; j < InventoryController.instance.selectedItemGrid.gridSizeHeight; j++)
            {
                Case currentCase = InventoryController.instance.selectedItemGrid.GetCase(i, j);

                if (currentCase != null)
                {
                    // Remove from the list
                    Inventory.Instance.cases.Remove(currentCase);
                    sum += currentCase.caseValue;
                    InventoryController.instance.selectedItemGrid.CleanGridReference(currentCase);
                    // Destroy the game object
                    Destroy(currentCase);
                }
            }
        }
        valueText.text = "Money: " + sum;
    }
}
