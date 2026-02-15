using System;
using TMPro;
using UnityEngine;

public class PrestigeAndBattleScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentPureChromaText;
    [SerializeField] private TextMeshProUGUI prestigeInfoText;
    [SerializeField] private TextMeshProUGUI prestigeButtonText;

    void OnEnable()
    {
        RecalculatePrestigeUpgrade();
        // TODO: This should also recalculate on chroma increase (or every certain time), but only while on this screen...
        //  (maybe a coroutine handled at this level?)
    }

    public void RecalculatePrestigeUpgrade()
    {
        double currentChroma = GameManager.Instance.CurrentChroma;
        int currentPureChroma = GameManager.Instance.PureChroma;
        float currentPureChromaBonus = Prestige.CalculateIncreaseFromPureChroma(currentPureChroma);
        int pureChromaGained = Prestige.CalculatePureChromaFromChroma(currentChroma);
        float gainedPureChromaBonus = Prestige.CalculateIncreaseFromPureChroma(pureChromaGained);
        // Current pure chroma text
        string currentPureChromaString = $"Pure Chroma: {currentPureChroma} <sprite index=0> (+{currentPureChromaBonus}% cps)";
        currentPureChromaText.text = currentPureChromaString;
        // Information text
        string prestigeInfoString1 = $"You can spend all your chroma and resources to <b>gain {pureChromaGained}<sprite index=0> (Pure Chroma)</b>.";
        string prestigeInfoString2 = $"Each <sprite index=0> will increase your cps by a 0.1%, making your next run smoother. You will gain an extra <b>+{gainedPureChromaBonus}%</b> if you prestige now.";
        string prestigeInfoString3 = $"After prestige, you will have <b>{currentPureChroma + pureChromaGained}<sprite index=0></b> (total bonus gain from Pure Chroma: <b>+{currentPureChromaBonus + gainedPureChromaBonus}%</b>)";
        string prestigeInfoString4 = "Are you ready to trascend?";
        prestigeInfoText.text = $"{prestigeInfoString1}\n{prestigeInfoString2}\n{prestigeInfoString3}\n{prestigeInfoString4}";
        // Button text
        string prestigeButtonString = $"Prestige now for {pureChromaGained}<sprite index=0>";
        prestigeButtonText.text = prestigeButtonString;
    }

    public void OnPrestigeNowButtonPressed()
    {
        UIManager.Instance.ShowConfirmMessage(
            "If you prestige now, you will start with no chroma and no resources, but you'll get a bonus. Continue?",
            GameManager.Instance.ActivatePrestige
        );
    }
}
