using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    [Header("Mismo orden/Longitud que en CharacterSelect")]
    public GameObject[] characterPrefabs;

    [Header("Dónde aparece el jugador")]
    public Transform spawPoint;

    const string Key = "SelectedCharIndex";


    private void Awake()
    {
        int index = PlayerPrefs.GetInt(Key, -1);

        if (
            index < 0 ||
            index >= characterPrefabs.Length ||
            characterPrefabs[index] == null
           )
        {
            Debug.LogWarning ("Selección invalida. Regresando a CharacterScene");
            SceneManager.LoadScene("CharacterScreen");
        }

        var gameCharacter = characterPrefabs[index];
        Instantiate (gameCharacter, spawPoint ? spawPoint.position : Vector3.zero, Quaternion.identity);
    }
}
