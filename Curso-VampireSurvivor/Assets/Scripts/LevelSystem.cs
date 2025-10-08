using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class LevelSystem : MonoBehaviour
{
    public int level = 1;
    public int xp = 0;
    public int xpToNext = 10;

    public float xp_ui = 0;

    public Slider xp_slider;
    public TextMeshProUGUI level_text;

    public event Action OnLevelUp;

    private void Awake()
    {
        xp_slider = GameObject.FindAnyObjectByType<Slider>();

        GameObject xp_object = xp_slider.gameObject;
        level_text = xp_object.GetComponentInChildren<TextMeshProUGUI>();

        level_text.text = "Level " + level.ToString();

        xp_slider.value = 0f;
    }

    public void AddXP(int amount)
    {
        xp += amount;
        if (xp >= xpToNext)
        {
            xp -= xpToNext;
            level++;
            xpToNext += 5;
            OnLevelUp?.Invoke();

            level_text.text = "Level " + level.ToString();
        }

        xp_ui = (float) xp / xpToNext;

        xp_slider.value = xp_ui;
    }
}
