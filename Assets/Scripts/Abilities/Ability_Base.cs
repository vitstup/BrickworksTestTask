using UnityEngine;

public class Ability_Base : Ability
{
    public Ability_Base(string abilityName) : base(abilityName, 0, null, true)
    {

    }

    public override bool CanForget(Player player)
    {
        return false;
    }

    public override void Forget(Player player)
    {
        Debug.Log("Trying to forget base ability, result = false");
    }

    public override Color GetAbilityColor()
    {
        return Color.green;
    }
}