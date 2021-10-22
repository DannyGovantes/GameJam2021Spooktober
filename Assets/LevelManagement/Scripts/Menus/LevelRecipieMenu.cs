using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelRecipieMenu : Menu<LevelRecipieMenu>
{

    public TMP_Text characterName;
    public TMP_Text description;
    public Image profileImage;
    public GameObject grid;
    public GameObject gridElementPrefab;
    private List<GameObject> _gridElementsList = new List<GameObject>();
    private WaitForSeconds _delayTime;
    [SerializeField]
    private float _delayTimeInSeconds = 5.0f;

    public void Start()
    {
        InitializeDelayTime();
    }

    public void InitializeDelayTime()
    {
        _delayTime = new WaitForSeconds(_delayTimeInSeconds);
        StartCoroutine(DisplayDelayTime());
    }

    public void SetRecipieCard(string characterName,string description, Sprite profileImage,List<RecipieItem> items)
    {
        ClearGridList();
        this.characterName.text = characterName;
        this.description.text = description;
        this.profileImage.sprite = profileImage;
        foreach(var item in items)
        {
            var newImage = Instantiate(gridElementPrefab,grid.transform);
            _gridElementsList.Add(newImage);
            newImage.GetComponent<Image>().sprite = item.Image;
        }

    }

    private void ClearGridList()
    {
        foreach(var gridItem in _gridElementsList)
        {
            Destroy(gridItem);
        }
        _gridElementsList.Clear();
    }

    private IEnumerator DisplayDelayTime()
    {
        yield return _delayTime;
        GameMenu.Open();
    }


}
