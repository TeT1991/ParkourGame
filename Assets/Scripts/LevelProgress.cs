// Scripts/LevelProgress.cs
using UnityEngine;

public static class LevelProgress
{
    private const string UnlockedKey = "unlocked_level";

    /// <summary>
    /// »ндекс максимального открытого уровн€ (build index).
    /// ѕо умолчанию 1 Ч только первый уровень открыт.
    /// </summary>
    public static int MaxUnlockedLevel
    {
        get => PlayerPrefs.GetInt(UnlockedKey, 1);
        set
        {
            if (value > MaxUnlockedLevel)
            {
                PlayerPrefs.SetInt(UnlockedKey, value);
                PlayerPrefs.Save();
            }
        }
    }

    public static bool IsLevelUnlocked(int buildIndex)
    {
        return buildIndex <= MaxUnlockedLevel;
    }

    // ƒл€ сброса прогресса Ч вызовешь из кнопки если нужно
    public static void ResetProgress()
    {
        PlayerPrefs.DeleteKey(UnlockedKey);
        PlayerPrefs.Save();
    }
}