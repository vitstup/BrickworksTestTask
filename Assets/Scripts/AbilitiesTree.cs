using Abilities;
using System.Collections.Generic;

public class AbilitiesTree // Possible to create some specific trees with unique abilities list inherited from this class
{
    public List<Ability> abilities { get; private set; }

    public AbilitiesTree()
    {
        InitializeAbilities();
    }

    protected virtual void InitializeAbilities()
    {
        abilities = new List<Ability>();

        abilities.Add(new Ability_Base("Base"));

        abilities.Add(new Ability_1(1, new Ability[] { abilities[0] }, false));

        abilities.Add(new Ability_2(1, new Ability[] { abilities[0] }, false));
        abilities.Add(new Ability_3(2, new Ability[] { abilities[2] }, false));

        abilities.Add(new Ability_4(1, new Ability[] { abilities[0] }, false));
        abilities.Add(new Ability_5(2, new Ability[] { abilities[4] }, false));
        abilities.Add(new Ability_6(2, new Ability[] { abilities[4] }, false));
        abilities.Add(new Ability_7(3, new Ability[] { abilities[5], abilities[6] }, false));

        abilities.Add(new Ability_8(1, new Ability[] { abilities[0] }, false));
        abilities.Add(new Ability_9(1, new Ability[] { abilities[0] }, false));
        abilities.Add(new Ability_10(2, new Ability[] { abilities[8], abilities[9] }, false));
    }

    public Ability[] GetChilds(Ability ability)
    {
        List<Ability> childrens = new List<Ability>();
        for (int i = 0; i < abilities.Count; i++)
        {
            if (abilities[i].linkedTo == null) continue;
            for (int p = 0; p < abilities[i].linkedTo.Length; p++)
            {
                if (abilities[i].linkedTo[p] == ability) childrens.Add(abilities[i]);
            }
        }
        return childrens.ToArray();
    }

    public int GetAbilityId(Ability ability)
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            if (abilities[i] == ability) return i;
        }
        throw new System.Exception("Tree does'nt contain this ability");
    }

    public void ForgetAll(Player player)
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            abilities[i].Forget(player);
        }
    }
}