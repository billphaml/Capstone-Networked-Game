using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item Data", menuName = "GameItem/Item/Data")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public GameItem[] theGameItem;
    public Dictionary<GameItem, int> GetID = new Dictionary<GameItem, int>();
    public Dictionary<int, GameItem> getItem = new Dictionary<int, GameItem>();

    public void OnAfterDeserialize()
    {
        GetID = new Dictionary<GameItem, int>();
        getItem = new Dictionary<int, GameItem>();
        for (int i = 0; i < theGameItem.Length; i++)
        {
            GetID.Add(theGameItem[i], i);
            getItem.Add(i, theGameItem[i]);
        }

        
    }

    public void OnBeforeSerialize()
    {
       
    }
}
