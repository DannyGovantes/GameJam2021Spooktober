using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TransitionFader : ScreenFader
{

    [SerializeField]
    private float _lifetime = 1f;

    [SerializeField]
    private float _delay = 0.3f;
    public float Delay {get{return _delay;}}

    protected void Awake()
    {
        _lifetime = Mathf.Clamp(_lifetime, FadeOnDuration + FadeOffDuration + _delay, 10f);
    }

    private IEnumerator PlayRoutine()
    {


        FadeOn();

        yield return new WaitForSeconds(FadeOnDuration);

    }
    public void Play()
    {
        StartCoroutine(PlayRoutine());
    }

    public static void PlayTransition(TransitionFader transitionPrefab)
    {
        if(transitionPrefab != null){
            TransitionFader instance = Instantiate(transitionPrefab,Vector3.zero,Quaternion.identity);

            instance.Play();

        }
    }

}