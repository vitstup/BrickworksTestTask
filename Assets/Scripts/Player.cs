using UnityEngine;

public class Player : MonoBehaviour
{
    public Wallet wallet { get; private set; }

    public AbilitiesTree abilitiesTree { get; private set; }

    private void Awake()
    {
        wallet = new Wallet();
        abilitiesTree = new AbilitiesTree();
    }

}