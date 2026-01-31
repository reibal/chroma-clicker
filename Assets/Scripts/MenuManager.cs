using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public static MenuManager Instance { get; private set; }

    [SerializeField] private GameObject homeMenu;
    [SerializeField] private GameObject upgradesMenu;
    [SerializeField] private GameObject battleMenu;
    [SerializeField] private GameObject settingsMenu;

	[SerializeField] private GameObject homeMenuButton;
    [SerializeField] private GameObject upgradesMenuButton;
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
		OpenMenu(homeMenu, homeMenuButton);
	}

    public void OpenUpgradesMenu()
    {
		OpenMenu(upgradesMenu, upgradesMenuButton);
    }

	public void OpenBattleMenu()
	{
		OpenMenu(battleMenu, battleMenuButton);
	}

	public void OpenSettingsMenu()
	{
		OpenMenu(settingsMenu, settingsMenuButton);
	}

    private void CloseAllMenus()
    {
        homeMenu.SetActive(false);
        upgradesMenu.SetActive(false);
		battleMenu.SetActive(false);
		settingsMenu.SetActive(false);
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
		button.GetComponent<UnityEngine.UI.Image>().color = SELECTED_MENU_COLOR;
	}

	private void ResetButton(GameObject button)
	{
		button.GetComponent<UnityEngine.UI.Image>().color = Color.white;
	}

	private void ResetAllButtons()
	{
		ResetButton(homeMenuButton);
		ResetButton(upgradesMenuButton);
		ResetButton(battleMenuButton);
		ResetButton(settingsMenuButton);
	}

}
