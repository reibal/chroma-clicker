using TMPro;
using UnityEngine;

public class PopUpMessageWindow : MonoBehaviour
{
    public static PopUpMessageWindow Instance { get; private set; }
    
    [SerializeField]
    private TextMeshProUGUI shownText;

    
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

    public void ShowMessage(string message)
    {
        shownText.text = message;
        gameObject.SetActive(true);
    }

    public void OnButtonPressed()
    {
        gameObject.SetActive(false);
    }
}
