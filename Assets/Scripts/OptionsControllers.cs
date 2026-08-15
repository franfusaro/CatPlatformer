using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsControllers : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] public static float defaultVolume = 0.2f;

    [SerializeField] Slider difficultySlider;
    [SerializeField] public static float defaultDifficulty = 1f;


    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = PlayerPrefsController.GetMasterVolume();
        //difficultySlider.value = PlayerPrefsController.GetDifficulty();
    }

    // Update is called once per frame
    void Update()
    {
        var musicPlayer = FindObjectOfType<MusicPlayer>();
        if (musicPlayer)
        {
            musicPlayer.SetMasterVolume(volumeSlider.value);
        }
        else
        {
            Debug.LogWarning("No music player found...");
        }
    }
    public void SaveAndExit()
    {
        PlayerPrefsController.SetMasterVolume(volumeSlider.value);
        //PlayerPrefsController.SetDifficulty(difficultySlider.value);
        FindObjectOfType<LevelLoader>().LoadMainMenu();
    }

    public void SetDefaults()
    {
        PlayerPrefsController.SetMasterVolume(defaultVolume);
        //PlayerPrefsController.SetDifficulty(defaultDifficulty);
        volumeSlider.value = PlayerPrefsController.GetMasterVolume();
        //difficultySlider.value = PlayerPrefsController.GetDifficulty();
    }
}
