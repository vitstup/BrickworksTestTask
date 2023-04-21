using UnityEngine;
using UnityEngine.Events;

public class MainManager : MonoBehaviour
{
    public class TreeEvent : UnityEvent<AbilitiesTree> { }
    public static TreeEvent RenderTreeEvent = new TreeEvent();
    public static MainManager instance;

    [HideInInspector] public Player player;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        InitializePlayer();
    }

    private void Start()
    {
        RenderTreeEvent?.Invoke(player.abilitiesTree);
    }

    private void InitializePlayer() // can spawn player here in real game 
    {
        player = FindObjectOfType<Player>();
    }
}