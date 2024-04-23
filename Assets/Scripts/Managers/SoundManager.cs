using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public AudioClip[] clips;
    [SerializeField] private AudioSource audioSource;
    public AudioSource backSource;

    private void Start()
    {
        PlayBackground(AudioType.background);
    }
    private void PlayBackground(AudioType audioType)
    {
        backSource.volume = .75f;
        backSource.pitch = 1f;
        backSource.loop = true;
        backSource.clip = clips[(int)audioType];
        backSource.Play();
    }
    public void PlaySound(AudioType audioType, float volume, float pitch)
    {
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.loop = false;
        audioSource.PlayOneShot(clips[(int)audioType]);
    }
    public void StopSound()
    {
        audioSource.Stop();
    }
    public enum AudioType
    {
        background,
        click,
    }
}
