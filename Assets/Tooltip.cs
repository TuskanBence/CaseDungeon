using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Tooltip : MonoBehaviour
{

    /// <summary>
    /// Static reference to the tooltip instance.
    /// </summary>
    public static Tooltip tooltipinstance;
    public TextMeshProUGUI tipText;
    public RectTransform tipTransform;
    public bool stay;
    void Awake()
    {
        if (tooltipinstance == null)
        {
            tooltipinstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        HideTip();
    }

    public void ShowTip(string tip,Vector2 mousePos)
    {
            tipText.text = tip;
            tipTransform.sizeDelta = new Vector2(tipText.preferredWidth > 100 ? 100 : tipText.preferredWidth, tipText.preferredHeight);

            tipTransform.gameObject.SetActive(true);
            tipTransform.transform.position = new Vector2(mousePos.x + tipTransform.sizeDelta.x / 2, mousePos.y);
    }
    public void HideTip()
    {
        tipText.text = default;
        tipTransform.gameObject.SetActive(false);
    }
}
