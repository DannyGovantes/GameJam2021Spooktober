using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Recipie Items", menuName = "Recipie/Create Recipie Item", order = 2)]
public class RecipieItem: ScriptableObject
{

    [SerializeField]
    protected string _name;
    public string Name => _name;

    [SerializeField]
    protected Sprite _image;
    public Sprite Image => _image;

    [SerializeField]
    protected GameObject _model;
    public GameObject Model => _model;



}