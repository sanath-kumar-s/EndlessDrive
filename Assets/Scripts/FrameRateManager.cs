using System.Collections;
using System.Threading;
using UnityEngine;

public class FrameRateManager : MonoBehaviour
{
    [SerializeField] private int defaultFPS = 60;


    private void Awake()
    {
        SetTargetFPS(defaultFPS);
    }

    public void SetTargetFPS(int fps)
    {
        Application.targetFrameRate = fps;
        
    }

    public int GetCurrentFPS()
    {
        return Application.targetFrameRate;
    }

    
}
