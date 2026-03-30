using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QualityButton_UI : MonoBehaviour
{
    [SerializeField] private Button qualityButton;
    [SerializeField] private TextMeshProUGUI qualityText;

    private int currentQualityIndex = 0;

    private const string QualityPrefKey = "GameQualityLevel";

    private void Awake()
    {
        // Load saved quality index or use default
        currentQualityIndex = PlayerPrefs.GetInt(QualityPrefKey, 2);
        ApplyQuality(); // Apply loaded quality setting
    }

    private void Start()
    {
        if (qualityButton != null)
            qualityButton.onClick.AddListener(CycleQuality);
    }

    private void CycleQuality()
    {
        currentQualityIndex = (currentQualityIndex + 1) % QualitySettings.names.Length;
        ApplyQuality();
    }

    private void ApplyQuality()
    {
        if (QualitySettings.GetQualityLevel() != currentQualityIndex)
            QualitySettings.SetQualityLevel(currentQualityIndex, true);

        qualityText.text = QualitySettings.names[currentQualityIndex];

        PlayerPrefs.SetInt(QualityPrefKey, currentQualityIndex);
        PlayerPrefs.Save();
    }

}
