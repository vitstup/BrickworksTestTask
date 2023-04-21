using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MainUI : MonoBehaviour
{
    public static UnityEvent ReUpdateInfoEvent = new UnityEvent();

    [SerializeField] private TextMeshProUGUI pointsText;

    private void Start()
    {
        UpdateInfo();
    }

    public void EarnPoints()
    {
        MainManager.instance.player.wallet.AddPoints(1);
    }

    public void ForgetAll()
    {
        MainManager.instance.player.abilitiesTree.ForgetAll(MainManager.instance.player);
    }

    public void UpdateInfo()
    {
        pointsText.text = string.Format("Points - {0}", MainManager.instance.player.wallet.points);

        ReUpdateInfoEvent?.Invoke();
    }
}