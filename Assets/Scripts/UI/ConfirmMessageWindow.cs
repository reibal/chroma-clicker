using System;
using TMPro;
using UnityEngine;

public class ConfirmMessageWindow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI shownText;

    private Action callbackOnConfirm;

    public void ShowMessage(string message)
    {
        shownText.text = message;
        gameObject.SetActive(true);
    }

    public void SetCallbackOnConfirm(Action callbackOnConfirm)
    {
        this.callbackOnConfirm = callbackOnConfirm;
    }

    public void OnConfirmButtonPressed()
    {
        callbackOnConfirm();
        gameObject.SetActive(false);
    }

    public void OnCancelButtonPressed()
    {
        gameObject.SetActive(false);
    }
}
