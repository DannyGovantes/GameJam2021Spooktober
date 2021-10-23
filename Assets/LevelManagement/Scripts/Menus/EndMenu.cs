using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : Menu<EndMenu>
{
    public void OnRestartLevel()
    {
        Time.timeScale = 1;
        InstructionsMenu.Open();
        InstructionsMenu.Instance.InitializeRoutine();
        LevelLoader.ReloadLevel();
    }

    public void OnMainMenu()
    {
        LevelLoader.LoadMainMenuLevel();
        MainMenu.Open();
    }
}
