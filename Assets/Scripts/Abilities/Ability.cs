using UnityEngine;

public abstract class Ability
{
    public string abilityName {get; protected set;}

    public int pointsPrice {get; protected set;}

    public Ability[] linkedTo {get; protected set;}

    public bool isResearched {get; protected set;}

    public Ability(string abilityName, int pointsPrice, Ability[] linkedTo, bool isResearched)
    {
        this.abilityName = abilityName;
        this.pointsPrice = pointsPrice;
        this.linkedTo = linkedTo;
        this.isResearched = isResearched;
    }

    public void Research(Player player)
    {
        if (!isResearched)
        {
            isResearched = true;
            player.wallet.RemovePoints(pointsPrice);
        }
    }

    public virtual void Forget(Player player)
    {
        if (isResearched)
        {
            isResearched = false;
            player.wallet.AddPoints(pointsPrice);
        }
    }

    public bool CanResearch(Player player)
    {
        if (isResearched) return false;
        if (player.wallet.points < pointsPrice) return false;
        for (int i = 0; i < linkedTo.Length; i++)
        {
            if (linkedTo[i].isResearched) return true;
        }
        return false;
    }

    public virtual bool CanForget(Player player)
    {
        if (!isResearched) return false;
        var childrens = player.abilitiesTree.GetChilds(this);
        foreach (var child in childrens)
        {
            if (child.isResearched)
            {
                int parentsResearched = 0;
                for (int p = 0; p < child.linkedTo.Length; p++)
                {
                    if (child.linkedTo[p].isResearched) parentsResearched++;
                }
                if (parentsResearched <= 1) return false;
            }
        }
        return true;
    }

    public virtual Color GetAbilityColor()
    {
        return Color.blue;
    }
}