using UnityEngine;

[System.Serializable]
public class Wallet
{
    [field: SerializeField] public int points { get; private set; }

    public void AddPoints(int points)
    {
        this.points += points;
        Debug.Log(string.Format("Added {0} points, current points amount equals {1}", points, this.points));
    }

    public void RemovePoints(int points)
    {
        if (this.points < points) 
        {
            this.points = 0;
            Debug.Log("You're trying to remove more points, than you have.");
            return;
        }
        this.points -= points;
        Debug.Log(string.Format("Removed {0} points, current points amount equals {1}", points, this.points));
    }
}