using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    public const string MASTER_VOLUME_KEY = "master_volume";
    public const string DIFFICULTY_KEY = "difficulty";

    public const float defaultVolume = 0.2f;
    public const float defaultDifficulty = 1f;

    private const float MIN_VOLUME = 0f;
    private const float MAX_VOLUME = 1f;
    private const float MIN_DIFFICULTY = 0f;
    private const float MAX_DIFFICULTY = 2f;

    public static void SetMasterVolume(float volume)
    {
        volume = Mathf.Clamp(volume, MIN_VOLUME, MAX_VOLUME);
        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
    }

    public static float GetMasterVolume()
    {
        if (!PlayerPrefs.HasKey(MASTER_VOLUME_KEY))
        {
            SetMasterVolume(defaultVolume);
        }
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }

    public static void SetDifficulty(float difficulty)
    {
        difficulty = Mathf.Clamp(difficulty, MIN_DIFFICULTY, MAX_DIFFICULTY);
        PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
    }

    public static float GetDifficulty()
    {
        if (!PlayerPrefs.HasKey(DIFFICULTY_KEY))
        {
            SetDifficulty(defaultDifficulty);
        }
        return PlayerPrefs.GetFloat(DIFFICULTY_KEY);
    }
}
