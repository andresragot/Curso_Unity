using UnityEngine;
using System;

public class LevelSystem : MonoBehaviour
{
    public int level = 1;
    public int xp = 0;
    public int xpToNext = 10;

    public event Action OnLevelUp;

    public void AddXP(int amount)
    {
        xp += amount;
        if (xp >= xpToNext)
        {
            xp -= xpToNext;
            level++;
            xpToNext += 5;
            OnLevelUp?.Invoke();
        }
    }
}
