using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip[] audioClips;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void play_powerup()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.resource = audioClips[1];
            audioSource.Play();
        }
    }

    public float player_death()
    {
        audioSource.resource = audioClips[2];
        audioSource.Play();

        return audioClips[2].length;
    }
}
