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
    public int recipieLimitItems =>  _spawnPoints.Count;

    [SerializeField]
    private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();
    public List<SpawnPoint> SpawnPoints => _spawnPoints;



    private void Awake()
    {
        GetData();
    }

    private void Start()
    {
        _spawnPoints = GetSpawnPoints();
        _selectedRecipieItems = GetSelectedRecipieItems();
        _selectedCharacter = GetSelectedRecipieCharacter();
        
        if(!LevelRecipieMenu.Instance) return;
        SetUICard();
        SpawnItems();

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

    private string CreateDescription()
    {
        var listDest = "";
        foreach(var item in _selectedRecipieItems)
        {
            listDest += $"-{item.Name}\n";
        }
        var description = $"{_selectedCharacter.Description}\n{listDest}";
        return description;

    }

    private List<SpawnPoint> GetSpawnPoints()
    {
        var spawnPointsFound = FindObjectsOfType<SpawnPoint>();
        var newList = new List<SpawnPoint>();
        foreach(var spawnPoint in spawnPointsFound)
        {
            newList.Add(spawnPoint);
        }
        return newList;

    }

    private void SetUICard()
    {
        LevelRecipieMenu.Instance.SetRecipieCard(_selectedCharacter.Name, CreateDescription(), _selectedCharacter.Image, _selectedRecipieItems);
    }

    private void SpawnItems()
    {
       
        var counter = 0;
        var items = _selectedRecipieItems;
        foreach(var spawnPoint in _spawnPoints)
        {
            var item = items[counter];
            var model = Instantiate(item.Model, spawnPoint.SpawnPosition.position, Quaternion.identity,spawnPoint.transform);
            spawnPoint.SetUI(item.Image, item.Name);
            model.GetComponent<PickUpitem>().SetRecipieItem(item);
            counter++;
        }

    }


}
