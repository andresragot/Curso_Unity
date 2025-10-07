using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    public GameObject panel;
    public Button[] buttons;
    public WeaponManager weaponManager;
    public LevelSystem levelSystem;

    private void Start()
    {
        panel.SetActive(false);
        levelSystem.OnLevelUp += showOptions;

        //for (int i = 0; i < buttons.Length; ++i)
        //{
        //    var button = buttons[i];
        //    button.onClick += ElergirArma(i);
        //}
    }

    void showOptions()
    {
        Time.timeScale = 0f;
        panel.SetActive(true);

        // TODO: Obtener un random de armas para poder añadir los botones.
    }

    public void ElergirArma (int index)
    {
        weaponManager.AddWeapon(index);
        panel.SetActive(false);
        Time.timeScale = 1f;
    }
}
