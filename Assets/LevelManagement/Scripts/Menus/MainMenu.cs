using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


  public class MainMenu : Menu<MainMenu>
    {
    [SerializeField]
    private RawImage _rawImage;
    [SerializeField]
    private float _x = 0.01f;
    
    [SerializeField]
    private float _y = 0.01f;


    [SerializeField]
    private float _playDelay = 0.5f;

    [SerializeField]
    private TransitionFader startTransitionPrefab;

    public void OnPlayPressed()
    {
        // StartCoroutine(OnPlayPressedRoutine());
        LevelLoader.LoadNextLevel();
        InstructionsMenu.Open();

    }
    // private IEnumerator OnPlayPressedRoutine()
    // {
    // TransitionFader.PlayTransition(startTransitionPrefab);
    // LevelLoader.LoadNextLevel();

    // yield return new WaitForSeconds(_playDelay);
    // }

    public void Update()
    {
        _rawImage.uvRect = new Rect(_rawImage.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _rawImage.uvRect.size);

    }

    public void OnSettingsPressed()
    {

        SettingsMenu.Open();
    }
    public void OnCreditsPressed()
    {



        CreditsMenu.Open();
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
