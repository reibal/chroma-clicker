using Unity.VisualScripting;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public static MenuManager Instance { get; private set; }

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject upgradesMenu;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        OpenMainMenu();
    }

    public void OpenMainMenu()
    {
        CloseAllMenus();
        mainMenu.SetActive(true);
    }

    public void OpenUpgradesMenu()
    {
        CloseAllMenus();
        upgradesMenu.SetActive(true);
    }

    private void CloseAllMenus()
    {
        mainMenu.SetActive(false);
        upgradesMenu.SetActive(false);
    }

}
