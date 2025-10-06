using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour
{
    [Header("Nombre de la escena del menú")]
    public string sceneName = "CharacterScreen";

    public void OnTouchBack(InputAction.CallbackContext ctx)
    {
        Debug.Log ("OnTouchBack");
        SceneManager.LoadScene(sceneName);
    }
}
