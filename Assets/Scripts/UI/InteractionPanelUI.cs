using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPanelUI : MonoBehaviour
{
    [SerializeField] private Canvas interactionCanvas;

    [SerializeField] private TextMeshProUGUI abilityNameText;
    [SerializeField] private TextMeshProUGUI researchStatusText;
    [SerializeField] private TextMeshProUGUI researchPriceText;

    [SerializeField] private Button ResearchBtn;
    [SerializeField] private Button ForgetBtn;

    private Player player;
    private Ability ability;

    private void Awake()
    {
        MainUI.ReUpdateInfoEvent.AddListener(ReUpdateInfo);
        AbilityPanel.OpenInteractionPanelEvent.AddListener(OpenPanel);
    }

    private void OpenPanel(Player player, Ability ability)
    {
        interactionCanvas.gameObject.SetActive(true);
        UpdateInfo(player, ability);
    }

    private void ReUpdateInfo()
    {
        if (interactionCanvas.gameObject.activeSelf) UpdateInfo(player, ability);
    }

    private void UpdateInfo(Player player, Ability ability)
    {
        this.player = player;
        this.ability = ability;

        abilityNameText.text = string.Format("Ability - {0}", ability.abilityName);

        researchStatusText.text = ability.isResearched ? "Researched" : "Not Researched";
        researchStatusText.color = ability.isResearched ? Color.green : Color.red;

        researchPriceText.text = string.Format("Research price - {0}", ability.pointsPrice);

        ResearchBtn.interactable = ability.CanResearch(player);
        ForgetBtn.interactable = ability.CanForget(player);
    }

    public void Research()
    {
        ability.Research(player);
    }

    public void Forget()
    {
        ability.Forget(player);
    }
}