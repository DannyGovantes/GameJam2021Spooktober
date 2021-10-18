using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;


public class MenuManager : MonoBehaviour
{
    #region  Prefabs
    [SerializeField]
    private MainMenu mainMenuPrefab;
    [SerializeField]
    private SettingsMenu settingsMenuPrefab;
    [SerializeField]
    private CreditsMenu creditsMenuPrefab;
    [SerializeField]
    private PauseMenu pauseMenuPrefab;
    [SerializeField]
    private GameMenu gameMenuPrefab;
    [SerializeField]
    private WinScreen winScreenPrefab;



    #endregion

    private static MenuManager _instance;
    public static MenuManager Instance { get { return _instance; } }

    [SerializeField]
    private Transform _menuParent;

    private Stack<Menu> _menuStack = new Stack<Menu>();

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            InitializeMenus();
            Object.DontDestroyOnLoad(gameObject);
        }



    }
    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    private void InitializeMenus()
    {
        if (_menuParent == null)
        {
            GameObject menuParent = new GameObject("Menus");
            _menuParent = menuParent.transform;

        }
        Object.DontDestroyOnLoad(_menuParent.gameObject);

        BindingFlags myFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
        System.Type myType = this.GetType();

        FieldInfo[] fields = myType.GetFields(myFlags);


        foreach (FieldInfo field in fields)
        {
            Menu menu = field.GetValue(this) as Menu;
            if (menu != null)
            {
                Menu menuInstance = Instantiate(menu, _menuParent);
                if (menu != mainMenuPrefab)
                {
                    menuInstance.gameObject.SetActive(false);
                }
                else
                {
                    OpenMenu(menuInstance);
                }



            }
        }
    }

    public void OpenMenu(Menu menuInstance)
    {
        if (menuInstance == null)
        {
            Debug.LogWarning("MENUMANAGER Open menu error: invalid menu");
        }
        if (_menuStack.Count > 0)
        {
            if (!menuInstance.m_isStackVisible)
            {
                foreach (Menu menu in _menuStack)
                {
                    menu.gameObject.SetActive(false);
                }
            }
        }
        menuInstance.gameObject.SetActive(true);
        _menuStack.Push(menuInstance);
    }

    public void CloseMenu()
    {
        if (_menuStack.Count == 0)
        {
            Debug.LogWarning("MENUMANAGER Close menu, no menus in stack");
            return;
        }
        Menu topMenu = _menuStack.Pop();
        topMenu.gameObject.SetActive(false);

        if (_menuStack.Count > 0)
        {
            Menu nextMenu = _menuStack.Peek();
            nextMenu.gameObject.SetActive(true);
        }
    }
}

