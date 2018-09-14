using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class items : MonoBehaviour {

    string itemName;
    [SerializeField] UISprite itemSprite;

    public void SetInfo(string spriteName)
    {
        itemSprite = GetComponent<UISprite>();
        itemSprite.spriteName = spriteName;
        itemName = spriteName;
    }
}
