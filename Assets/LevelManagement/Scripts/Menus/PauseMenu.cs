using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PauseMenu : Menu<PauseMenu>
{

    public void OnResumePressed()
    {
        Time.timeScale = 1;
        base.OnBackPressed();
    }

    public void OnRestartPressed()
    {
        Time.timeScale = 1;
        LevelLoader.ReloadLevel();
        base.OnBackPressed();
    }

    public void OnMainMenuPressed()
    {
        Time.timeScale = 1;
        LevelLoader.LoadMainMenuLevel();

        MainMenu.Open();

        base.OnBackPressed();

    }
    public void OnQuitPressed()
    {
        Application.Quit();

    }
}



