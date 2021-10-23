using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIItem : MonoBehaviour
{
    [SerializeField]
    private Image _image;
    
    [SerializeField]
    private TMP_Text _name;

    private RecipieItem _recipieItem;
    public RecipieItem RecipieItem => _recipieItem;

    private Color32 _activateColor = new Color32(255, 255, 255, 255);

    public void SetUIItem(Sprite image,string name,RecipieItem item)
    {
        _image.sprite = image;
        _name.text = name;
        _recipieItem = item;
    }

    public void SetUIItemColor() => _image.color = _activateColor;



}
