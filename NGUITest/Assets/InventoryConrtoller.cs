using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryConrtoller : MonoBehaviour
{
    [SerializeField] List<string> itemNameList = new List<string>();
    [SerializeField] List<items> itemList = new List<items>();

    [SerializeField] GameObject ItemPrefab;
    [SerializeField] UIScrollView UIScrollView;
    [SerializeField] UIGrid UIGrid;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            InitItems();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            AddItem();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ClearAll();
        }
    }

    void InitItems()
    {
        itemNameList.Add("Berry_02");
        itemNameList.Add("Book_00");
        itemNameList.Add("Coin_03");
        itemNameList.Add("Cloak_02");
        itemNameList.Add("Crystal Ball_02");
        itemNameList.Add("Crystal Ball_03");
        itemNameList.Add("Essence_03");
        itemNameList.Add("Flower Bunch_03");
        itemNameList.Add("Gems_01");
        itemNameList.Add("Necklace_03");
        itemNameList.Add("metal sword");
        itemNameList.Add("Shirt_01");
    }

    void AddItem()
    {
        if(itemNameList.Count > 0)
        {
            int random = Random.Range(0, itemNameList.Count - 1);
            GameObject item = NGUITools.AddChild(UIGrid.gameObject, ItemPrefab);
            items itemScr = item.GetComponent<items>();
            itemScr.SetInfo(itemNameList[random]);
            itemList.Add(itemScr);
            UIGrid.Reposition();
            UIScrollView.ResetPosition();
        }
        else
        {
            InitItems();
            AddItem();
        }
        
    }

    void ClearAll()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if(itemList[i] != null)
            {
                Destroy(itemList[i].gameObject);
            }
        }

        itemList.Clear();
        itemNameList.Clear();
        UIGrid.Reposition();
        UIScrollView.ResetPosition();
    }

}
