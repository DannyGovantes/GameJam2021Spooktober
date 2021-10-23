using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GameMenu : Menu<GameMenu>
{

    [SerializeField]
    private List<RecipieItem> _selectedRecipieItems = new List<RecipieItem>();
    public List<RecipieItem> SelectedRecipieItems {
        get => _selectedRecipieItems;
        set => _selectedRecipieItems = value;
    }

    private List<GameObject> _layoutUIItems = new List<GameObject>();

    [SerializeField]
    private GameObject _itemImagePrefab;

    [SerializeField]
    private GameObject _layoutParent;

    public void InitializeGameMenu()
    {
        ClearUI();
        _selectedRecipieItems = GameManager.Instance.RecipieController.SelectedRecipieItems;
        foreach(var item in _selectedRecipieItems)
        {
            var uiItem = Instantiate(_itemImagePrefab, _layoutParent.transform);
            uiItem.GetComponent<UIItem>().SetUIItem(item.Image, item.Name,item);
            _layoutUIItems.Add(uiItem);
        }
    }

    private void ClearUI()
    {
        foreach(var item in _layoutUIItems)
        {
            Destroy(item);
        }
        _selectedRecipieItems.Clear();
        _layoutUIItems.Clear();
    }

    public void SetRecipieItemInventory(RecipieItem item)
    {
        if(_selectedRecipieItems.Contains(item))
        {
            var go = _layoutUIItems.Where(i => item == i.GetComponent<UIItem>().RecipieItem).ToList();
            go[0].GetComponent<UIItem>().SetUIItemColor();

        }
    }


}
