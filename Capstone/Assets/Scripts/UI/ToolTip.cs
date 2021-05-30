using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;


public class ToolTip : MonoBehaviour
{
    public static ToolTip theToolTip = null;

    [SerializeField]
    private Camera gameCamera;

    [SerializeField]
    private CanvasGroup toolTipGroup;

    [SerializeField]
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;
    public TextMeshProUGUI itemEffectText;

    // Start is called before the first frame update
    void Start()
    {
        setup();
    }

    // Update is called once per frame
    void Update()
    {
        if(toolTipGroup.alpha == 1)
        {
            followMouse();
        }
    }

    public void setup()
    {
        theToolTip = this;
        gameCamera = Camera.main;
        toolTipGroup = GetComponent<CanvasGroup>();
    }

    public void followMouse()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent as RectTransform, Input.mousePosition, transform.parent.GetComponent<Canvas>().worldCamera, out localPoint);
        transform.localPosition = localPoint;
    }


    public void setItemInfo(GameItem theGameItem)
    {
        itemNameText.text = theGameItem.itemName;
        itemDescriptionText.text = theGameItem.itemDescription;
        string itemEffect;
        switch (theGameItem.gameItemType)
        {
            case itemType.CONSUME:
                itemEffect = consumeEffectHandler((ConsumableItem)theGameItem);
                break;
            case itemType.DEFAULT:
                itemEffect = defaultEffectHandler();
                break;
             default:
                itemEffect = equipmentEffectHandler((EquipItem)theGameItem);
                break;
        }

        itemEffectText.text = itemEffect;
        showTool();
    }

    private string consumeEffectHandler(ConsumableItem theGameItem)
    {
        string returnString = "";
        if(theGameItem.HPValue > 0)
        {
            returnString += "Restore " + theGameItem.HPValue + " HP";
            returnString += "\n";
        }

        if (theGameItem.MPValue > 0)
        {
            returnString += "Restore " + theGameItem.MPValue + " MP";
            returnString += "\n";
        }

        return returnString;
    }

    private string defaultEffectHandler()
    {
        return "";
    }

    private string equipmentEffectHandler(EquipItem theGameItem)
    {
        string returnString = "";

        if (theGameItem.addAttack > 0)
        {
            returnString += "+ " + theGameItem.addAttack + " Attack Points";
            returnString += "\n";
        }

        if (theGameItem.addHP > 0)
        {
            returnString += "+ " + theGameItem.addHP + " HitPoints";
            returnString += "\n";
        }

        return returnString;
    }

    public void resetToolTip()
    {
        hideTool();
        itemNameText.text = "";
        itemDescriptionText.text = "";
        itemEffectText.text = "";
    }


    public void showTool()
    {
        toolTipGroup.alpha = 1;
    }

    public void hideTool()
    {
        toolTipGroup.alpha = 0;
    }
}
