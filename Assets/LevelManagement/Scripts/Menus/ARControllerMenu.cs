using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARControllerMenu : Menu<ARControllerMenu>
{
    private ARMarkups _arMarkups;


    public void Left()
    {
        _arMarkups = FindObjectOfType<ARMarkups>();
        _arMarkups.Swipe(-1);

    }

    public void Right()
    {
        _arMarkups = FindObjectOfType<ARMarkups>();
        _arMarkups.Swipe(1);
    }

    public override void OnBackPressed()
    {
        LevelLoader.LoadMainMenuLevel();
        MainMenu.Open();

    }

}
