using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FPSButtonHandler : MonoBehaviour
{
    [SerializeField] private Button fpsButton;
    [SerializeField] private TextMeshProUGUI fpsText;
    [SerializeField] private FrameRateManager _frameRateManager;

    [SerializeField] private int[] availableFPS = new int[] { 30, 60, 90, 120 };
    private int currentFPSIndex = 0;

    private void Start()
    {
        if (fpsButton != null)
            fpsButton.onClick.AddListener(CycleFPS);

        ApplyFPS();
    }

    private void CycleFPS()
    {
        currentFPSIndex = (currentFPSIndex + 1) % availableFPS.Length;
        ApplyFPS();
    }

    private void ApplyFPS()
    {
        int selectedFPS = availableFPS[currentFPSIndex];
        _frameRateManager.SetTargetFPS(selectedFPS);
        fpsText.text = selectedFPS.ToString();
    }
}
