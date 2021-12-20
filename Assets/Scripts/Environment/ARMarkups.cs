using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARMarkups : MonoBehaviour
{


    [SerializeField]
    private List<GameObject> _arObjects;
    public List<GameObject> ArObjects;

    [SerializeField]
    private Transform _parent;

    [SerializeField]
    private Vector3 _sideDistance;

    [SerializeField]
    private int _index;

    [SerializeField]
    private float _time = 1.5f;

    [SerializeField]
    private LeanTweenType _easeType;

    [SerializeField]
    private Transform _initialPosition;
    
    [SerializeField]
    private Transform _finalPosition;


    private void Awake()
    {
        CreateList();
        SetUp();
    }

    private void CreateList()
    {
        foreach(var arObject in _arObjects)
        {

            ArObjects.Add(Instantiate(arObject, _parent));
        }
    }

    private void SetUp()
    {
        for (var i = 0; i < ArObjects.Count; i++)
        {
            if(i != 0) ArObjects[i].SetActive(false);
        }
    }

    public void Swipe(int direction)
    {
        _index += direction;
        _index = Mathf.Clamp(_index, 0, ArObjects.Count-1);
        ToggleObject(ArObjects[_index]);

    }

    private void ToggleObject(GameObject arObject)
    {
        foreach(var ob in ArObjects)
        {
            ob.SetActive(false);
        }
        arObject.SetActive(true);
    }


}
