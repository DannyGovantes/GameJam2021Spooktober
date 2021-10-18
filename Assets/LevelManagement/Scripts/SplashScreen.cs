using UnityEngine;
using System.Collections;
using System.Collections.Generic;



    public class SplashScreen : MonoBehaviour
    {

      [SerializeField] private List<TransitionFader> _transitionFaders = new List<TransitionFader>();
        [SerializeField] private float delay = 1f;
        [SerializeField] private bool _isFinalScreen = false;

        private void Awake()
        {
           ClearScreen();
        }

        private void Start()
        {
            if (!_isFinalScreen)
            {
               StartShowScreens();
            }

        }
        
        private void ClearScreen()
        {
            foreach (var transitionFader in _transitionFaders)
            {
                transitionFader.SetAlpha(0);
            }
        }

        public void StartShowScreens()
        {
            StartCoroutine(ShowScreens());
        }

        private IEnumerator ShowScreens()
        {
            foreach (var transitionFader in _transitionFaders)
            {
                transitionFader.FadeOn();
                yield return new WaitForSeconds(transitionFader.FadeOnDuration);
                transitionFader.FadeOff();
                yield return new WaitForSeconds(transitionFader.FadeOffDuration+transitionFader.Delay);
                Destroy(transitionFader.gameObject);
            }
            LoadMainMenu();
        }
        
        
        public void LoadMainMenu()
        {
            StartCoroutine(FadeAndLoadRoutine());
        }

        private IEnumerator FadeAndLoadRoutine()
        {
            yield return new WaitForSeconds(delay);
            LevelLoader.LoadMainMenuLevel();
            if (_isFinalScreen)
            {
                MainMenu.Open();
            }
        }
    }
