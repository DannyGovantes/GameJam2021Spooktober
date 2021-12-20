using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimation : MonoBehaviour
{

    public Vector3 scale = new Vector3(2f,2f,2f);
    public float time = 0.5f;

    public LeanTweenType easeType;
    public void OnEnable()
    {
        LeanTween.scale(gameObject, scale, time).setEase(easeType);

    }
    public void OnDisable()
    {
        LeanTween.scale(gameObject, Vector3.zero, time).setEase(easeType);

    }
}
