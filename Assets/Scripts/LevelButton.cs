// Scripts/LevelButton.cs
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [Tooltip("Build index уровня, который открывает эта кнопка")]
    [SerializeField] private int levelBuildIndex;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        UpdateButtonState();
    }

    private void UpdateButtonState()
    {
        if (_button == null) return;

        bool isUnlocked = LevelProgress.IsLevelUnlocked(levelBuildIndex);
        _button.interactable = isUnlocked;
    }
}