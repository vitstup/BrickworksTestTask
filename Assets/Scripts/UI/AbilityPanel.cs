using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AbilityPanel : MonoBehaviour
{
    public class OpenPanelEvent : UnityEvent<Player, Ability> { }
    public static OpenPanelEvent OpenInteractionPanelEvent = new OpenPanelEvent();

    private Image abilityImage;
    private TextMeshProUGUI abilityName;

    private Ability ability;

    private void Awake()
    {
        abilityImage = GetComponent<Image>();
        abilityName = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateInfo(Ability ability)
    {
        this.ability = ability;

        abilityImage.color = ability.GetAbilityColor();
        abilityName.text = ability.abilityName;
    }

    public void Interact()
    {
        OpenInteractionPanelEvent?.Invoke(MainManager.instance.player, ability);
    }
}