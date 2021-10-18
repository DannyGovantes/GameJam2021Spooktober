using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DontDestroyOnLoad : MonoBehaviour
{


    private void Awake()
    {
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }
}