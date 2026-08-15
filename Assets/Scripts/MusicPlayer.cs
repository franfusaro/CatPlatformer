using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    private void Awake()
    {
        SetUpSingleton();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsController.GetMasterVolume();
    }

    void Start()
    {
        PlayRandomMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
            PlayRandomMusic();
    }

    void PlayRandomMusic()
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
    }

    public void SetMasterVolume(float volume)
    {
        audioSource.volume = volume;
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
