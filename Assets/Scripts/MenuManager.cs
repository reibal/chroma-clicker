using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public static MenuManager Instance { get; private set; }

	[Header("Menu Screens")]
    [SerializeField] private GameObject homeMenuScreen;
    [SerializeField] private GameObject resourcesMenuScreen;
    [SerializeField] private GameObject battleMenuScreen;
    [SerializeField] private GameObject settingsMenuScreen;

	[Header("Menu Navigation (Buttons)")]
	[SerializeField] private GameObject homeMenuButton;
    [SerializeField] private GameObject resourcesMenuButton;
    [SerializeField] private GameObject battleMenuButton;
    [SerializeField] private GameObject settingsMenuButton;

	private readonly Color SELECTED_MENU_COLOR = new Color(1f, 0.8f, 0.1f, 1f);

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
        OpenHomeMenu();
    }

    public void OpenHomeMenu()
    {
		OpenMenu(homeMenuScreen, homeMenuButton);
	}

    public void OpenResourcesMenu()
    {
		OpenMenu(resourcesMenuScreen, resourcesMenuButton);
    }

	public void OpenBattleMenu()
	{
		OpenMenu(battleMenuScreen, battleMenuButton);
	}

	public void OpenSettingsMenu()
	{
		OpenMenu(settingsMenuScreen, settingsMenuButton);
	}

    private void CloseAllMenus()
    {
        homeMenuScreen.SetActive(false);
        resourcesMenuScreen.SetActive(false);
		battleMenuScreen.SetActive(false);
		settingsMenuScreen.SetActive(false);
    }

	private void OpenMenu(GameObject menu, GameObject button)
	{
		CloseAllMenus();
		ResetAllButtons();
		menu.SetActive(true);
		SetButtonAsSelected(button);
	}

	private void SetButtonAsSelected(GameObject button)
	{
		button.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().color = SELECTED_MENU_COLOR;
	}

	private void ResetButton(GameObject button)
	{
		button.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().color = Color.white;
	}

	private void ResetAllButtons()
	{
		ResetButton(homeMenuButton);
		ResetButton(resourcesMenuButton);
		ResetButton(battleMenuButton);
		ResetButton(settingsMenuButton);
	}

}
