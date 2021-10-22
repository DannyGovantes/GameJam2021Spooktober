using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsMenu : Menu<InstructionsMenu>
{

    [SerializeField]
    private float _timeInSeconds = 7.0f;

    private WaitForSeconds _delay;

    public void Start()
    {

        InitializeRoutine();
    }

    public void InitializeRoutine()
    {
        _delay = new WaitForSeconds(_timeInSeconds);
        StartCoroutine(OpenRecipieMenu());

    }

    private IEnumerator OpenRecipieMenu()
    {
        yield return _delay;
        LevelRecipieMenu.Open();
        LevelRecipieMenu.Instance.InitializeDelayTime();

    }
    
}
