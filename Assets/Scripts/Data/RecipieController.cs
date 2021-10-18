using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RecipieController : MonoBehaviour
{

    [Header("Data Assets")]

    [SerializeField]
    private List<RecipieItem> _recipieItems = new List<RecipieItem>();
    public List<RecipieItem> RecipieItems => _recipieItems;
    
    [SerializeField]
    private List<RecipieCharacter> _recipieCharacters = new List<RecipieCharacter>();
    public List<RecipieCharacter> RecipieCharacters => _recipieCharacters;

    [Header("Game Iteration Data")]

    [SerializeField]
    private List<RecipieItem> _selectedRecipieItems = new List<RecipieItem>();
    public List<RecipieItem> SelectedRecipieItems => _selectedRecipieItems;

    [SerializeField]
    private RecipieCharacter _selectedCharacter;
    public RecipieCharacter SelectedCharacter => _selectedCharacter;
    public int recipieLimitItems = 2;


    private void Awake()
    {
        GetData();
    }

    private void Start()
    {
        _selectedRecipieItems = GetSelectedRecipieItems();
        _selectedCharacter = GetSelectedRecipieCharacter();

    }

    public void GetData()
    {
        _recipieItems = GetAssets<RecipieItem>("Items");
        _recipieCharacters = GetAssets<RecipieCharacter>("Characters");
    }

    private List<T> GetAssets<T>(string folder){

        var assets = Resources.LoadAll(folder, typeof(T)).Cast<T>().ToArray();
        var newList = new List<T>();
        foreach(var asset in assets)
        {
            newList.Add(asset);
        }

        return newList;

    }

    private RecipieCharacter GetSelectedRecipieCharacter()
    {
        var randomNumber = Random.Range(0, _recipieCharacters.Count - 1);
        return _recipieCharacters[randomNumber];

    }

    private List<RecipieItem> GetSelectedRecipieItems()
    {
        var carryList = _recipieItems;
        var newList = new List<RecipieItem>();
        for (int i = 0; i < recipieLimitItems; i++)
        {
            var randomNumber = Random.Range(0, carryList.Count - 1);
            newList.Add(carryList[randomNumber]);
            carryList.Remove(carryList[randomNumber]);
        }
        return newList;

    }


}
