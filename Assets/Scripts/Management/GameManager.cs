using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class GameManager : Singleton<GameManager>
{

    private PlayerController _playerController;
    public PlayerController PlayerController => _playerController;

    public List<RecipieItem> InventoryItems = new List<RecipieItem>();
    private RecipieController _recipieController;
    public RecipieController RecipieController => _recipieController;


    private void Start()
    {
        _recipieController = FindObjectOfType<RecipieController>();
        _playerController = FindObjectOfType<PlayerController>();
        _playerController.StartCharacter();
        
    }
    public void SwitchScheme(bool isMenuOpen)
    {
        if (isMenuOpen)
        {
            _playerController.EnableMenuControls();
        }
        else
        {
            _playerController.EnableGameplayControls();
        }
    }

    public void AddItemToInventory(RecipieItem item)
    {
        if(!InventoryItems.Contains(item))
        {
            InventoryItems.Add(item);
            GameMenu.Instance.SetRecipieItemInventory(item);

        }
    }

    public void DisplayState()
    {
        if(HasPlayerWon())
        {
            StartCoroutine(PlayerHasWonRoutine());
        }
        else
        {
            print("Te faltan ciertas cosas");
        }

    }

    private IEnumerator PlayerHasWonRoutine()
    {
        SwitchScheme(true);
        yield return new WaitForSeconds(1.2f);
        WinScreen.Open();
        //EndMenu
    }


    private bool HasPlayerWon()
    {
        var itemsLimit = InventoryItems.Count;
        var itemsCount = 0;
        var itemsleft = _recipieController.SelectedRecipieItems.Where(i => !InventoryItems.Contains(i)).ToList();
        return itemsleft.Count == 0 ? true : false ;
    }




}
