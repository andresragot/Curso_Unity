using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    [Header("Prefabsd personajes (en el mismo orden que los botones)")]
    public GameObject[] characterPrefabs;

    [Header("Nombre de la escena del juego")]
    public string gameSceneName = "SampleScene";

    const string Key = "SelectedCharIndex";

    public void Select (int index)
    {
        if (index < 0 || index >= characterPrefabs.Length)
        {
            Debug.LogWarning("Índice de personaje fuera de rango.");
            return;
        }

        PlayerPrefs.SetInt(Key, index);
        PlayerPrefs.Save();

        SceneManager.LoadScene(gameSceneName);

        Debug.Log ("Pressed index: " + index);
    }
}
