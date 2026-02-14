using TMPro;
using UnityEngine;

public class PopUpMessageWindow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI shownText;

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
