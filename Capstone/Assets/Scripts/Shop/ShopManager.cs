using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager theShopManager;

    public Dictionary<string, Shop> shopDictionary = new Dictionary<string, Shop>();
    public List<ScriptableShop> theScriptableShopList = new List<ScriptableShop>();

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public ShopUI theShopUI;

    public Shop activeShop;
    public float currentRestockTime;
    private float restockTime = 4200f;

    void Awake()
    {
        theShopManager = this;
        shopSetup();
    }

    void Start()
    {
        currentRestockTime = restockTime;
    }

     void Update()
    {
        updateTimer();
    }

    public void shopSetup()
    {
        for(int i = 0; i <  theScriptableShopList.Count; i++)
        {
            Shop addShop = new Shop(theScriptableShopList[i]);
            shopDictionary.Add(addShop.shopName, addShop);
        }
    }

    public void triggerShop(string theShopName)
    {
        
        Shop returnShop;
        if(shopDictionary.TryGetValue(theShopName, out returnShop)){
            returnShop = shopDictionary[theShopName];
        }

        Debug.Log("This is the result for try get value " + shopDictionary.TryGetValue(theShopName, out returnShop));

        Debug.Log("Dictionary count: " + shopDictionary.Count);
        Debug.Log("This is the result for return shop " + returnShop);
        Debug.Log("This is the result for return shop name " + returnShop.shopName);

        if (returnShop.shopName != null)
        {
            activeShop = returnShop;
            if (onItemChangedCallBack != null)
            {
                onItemChangedCallBack.Invoke();
            }
            turnOnShop();
        }
        else
        {
            activeShop = null;
            turnOffShop();
        }
    }

    public void addItem(GameItem theItem)
    {
        for(int i = 0; i < activeShop.theShopItem.Count; i++)
        {
            if(activeShop.theShopItem[i].theShopItem == theItem)
            {
                activeShop.theShopItem[i].shopItemAmount++;
                if (onItemChangedCallBack != null) onItemChangedCallBack.Invoke();
                return;
            }
        }

        ShopItem addShopItem = new ShopItem(theItem);
        activeShop.theShopItem.Add(addShopItem);
        if (onItemChangedCallBack != null) onItemChangedCallBack.Invoke();
    }

    public void removeItem(GameItem theItem)
    {
        for(int i = 0; i < activeShop.theShopItem.Count; i++)
        {
            if(activeShop.theShopItem[i].theShopItem == theItem)
            {
                if(activeShop.theShopItem[i].shopItemAmount > 1)
                {
                    activeShop.theShopItem[i].shopItemAmount--;
                }
                else
                {
                    activeShop.theShopItem.RemoveAt(i);
                }
            }
        }
        if (onItemChangedCallBack != null) onItemChangedCallBack.Invoke();
    }

    public bool canAdd()
    {
        if(activeShop.theShopItem.Count < 24)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void updateTimer()
    {
        currentRestockTime -= Time.deltaTime;
        if(currentRestockTime <= 0)
        {
            currentRestockTime = restockTime;
            shopReset();
        }
    }

    private void shopReset()
    {
        shopDictionary.Clear();
        shopSetup();
    }

    private void turnOnShop()
    {
        theShopUI.turnOn();
    }

    private void turnOffShop()
    {
        theShopUI.turnOff();
    }

}
