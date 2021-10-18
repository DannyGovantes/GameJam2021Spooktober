using System.Collections;
using System.Collections.Generic;
using UnityEngine;


  public class MainMenu : Menu<MainMenu>
    {

        [SerializeField]
        private float _playDelay = 0.5f;

        [SerializeField]
        private TransitionFader startTransitionPrefab;

        public void OnPlayPressed()
        {
            // StartCoroutine(OnPlayPressedRoutine());
            LevelLoader.LoadNextLevel();
            GameMenu.Open();

        }
        // private IEnumerator OnPlayPressedRoutine()
        // {
        // TransitionFader.PlayTransition(startTransitionPrefab);
        // LevelLoader.LoadNextLevel();

        // yield return new WaitForSeconds(_playDelay);
        // }

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
