using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{
    private bool is_paused = false;

    public GameObject pauseMenu;

    public void Awake()
    {
        pauseMenu.SetActive(false);
    }

    public void OnEscape(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (is_paused)
            {
                Time.timeScale = 1f;
                is_paused = false;

                pauseMenu.SetActive(false);
            }
            else
            {
                Time.timeScale = 0f;
                is_paused = true;

                pauseMenu.SetActive(true);
            }

        }
    }
}
