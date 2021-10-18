using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Recipie Characters",menuName= "Recipie/Create Character",order=3)]
public class RecipieCharacter : ScriptableObject
{
    [SerializeField]
    protected string _name;
    public string Name => _name;

    [SerializeField]
    [TextArea]
    protected string _description;
    public string Description => _description;

    [SerializeField]
    protected Sprite _image;
    public Sprite Image => _image;
}
