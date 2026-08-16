using NUnit.Framework;
using UnityEngine;

public class PlayerPrefsControllerTests
{
    [TearDown]
    public void TearDown()
    {
        PlayerPrefs.DeleteKey(PlayerPrefsController.MASTER_VOLUME_KEY);
        PlayerPrefs.DeleteKey(PlayerPrefsController.DIFFICULTY_KEY);
    }

    [Test]
    public void SetMasterVolume_ClampsAboveMax()
    {
        PlayerPrefsController.SetMasterVolume(5f);

        Assert.AreEqual(1f, PlayerPrefsController.GetMasterVolume());
    }

    [Test]
    public void SetMasterVolume_ClampsBelowMin()
    {
        PlayerPrefsController.SetMasterVolume(-5f);

        Assert.AreEqual(0f, PlayerPrefsController.GetMasterVolume());
    }

    [Test]
    public void SetDifficulty_ClampsToRange()
    {
        PlayerPrefsController.SetDifficulty(-5f);
        Assert.AreEqual(0f, PlayerPrefsController.GetDifficulty());

        PlayerPrefsController.SetDifficulty(5f);
        Assert.AreEqual(2f, PlayerPrefsController.GetDifficulty());
    }

    [Test]
    public void GetDifficulty_SeedsDefaultWhenUnset()
    {
        Assert.AreEqual(PlayerPrefsController.defaultDifficulty, PlayerPrefsController.GetDifficulty());
    }
}
