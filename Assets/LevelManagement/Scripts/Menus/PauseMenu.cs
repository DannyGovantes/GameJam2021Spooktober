using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PauseMenu : Menu<PauseMenu>
{

    public void OnResumePressed()
    {
        Time.timeScale = 1;
        GameManager.Instance.SwitchScheme(false);
        base.OnBackPressed();
    }

    public void OnRestartPressed()
    {
        Time.timeScale = 1;
        InstructionsMenu.Open();
        InstructionsMenu.Instance.InitializeRoutine();
        LevelLoader.ReloadLevel();
    }

    public void OnMainMenuPressed()
    {
        Time.timeScale = 1;
        LevelLoader.LoadMainMenuLevel();

        MainMenu.Open();
    }
  
      public override void OnBackPressed()
    {

#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else

        Application.Quit();
#endif
    }

}



