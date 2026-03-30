using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public enum Difficulty { Easy, Normal, Medium, Difficult, NearlyImpossible }
    [SerializeField] private float difficultyIncreaseInterval = 30f; // Time in seconds to increase difficulty
    [SerializeField] private Difficulty currentDifficulty = Difficulty.Easy;
    
    [SerializeField] private float difficultyIncrementTimeIncreasePerState = 3f;

    [Header("Speed")]
    [SerializeField] private float startSpeed = 5f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float increasePerState = .5f;

    [Header("Obstracle Row Frequency")]
    [SerializeField] private float startTimeGapBtwEachSpawn = 4f; // Time gap between each spawn
    [SerializeField] private float minTimeGapBtwEachSpawn = 2f; // Time gap between each spawn
    [SerializeField] private float decreasePerState = 0.5f;

    [Header("Score Per Second")]
    [SerializeField] private float startScorePerSecond = 5f;
    [SerializeField] private float maxScorePerSecond = 10f;
    [SerializeField] private float increaseScorePerState = 1f;

    [Header("Camera FOV")]
    [SerializeField] private float startCameraFOV = 49f;
    [SerializeField] private float maxCameraFOV = 55f;
    [SerializeField] private float increaseCameraFOVPerState = 0.5f;
    [SerializeField] private float FOVTransitionSpeed = 1f;

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime; // Add this line to track time

        ChangeValueRespectToDifficulty();

        if (timer >= difficultyIncreaseInterval)
        {
            timer = 0f;
            IncreaseDifficulty();
        }

        Difficulty priveousDifficulty = currentDifficulty;

        if(currentDifficulty != priveousDifficulty)
        {
            difficultyIncreaseInterval += difficultyIncrementTimeIncreasePerState;
        }

        
    }


    void IncreaseDifficulty()
    {
        if ((int)currentDifficulty < System.Enum.GetValues(typeof(Difficulty)).Length - 1)
        {
            currentDifficulty++;
            
        }
    }

    private void ChangeValueRespectToDifficulty()
    {
        MoveSpeedManager();
        TimeGapBtwEachSpawnManager();
        ScorePerSecondManager();
        ObstraclePerRowManager();

        if(CameraController.Instance.GetCameraFOV() <= maxCameraFOV) 
            CameraController.Instance.SetCameraFOV(Mathf.Lerp(CameraController.Instance.GetCameraFOV(), startCameraFOV + ((int)currentDifficulty * 0.5f), Time.deltaTime * FOVTransitionSpeed));
    }

    private void ObstraclePerRowManager()
    {
        if ((int)currentDifficulty >= 3)
        {
            ObstacleSpawner.Instance.SetObstraclePerRow(5);
        }
        else
        {
            ObstacleSpawner.Instance.SetObstraclePerRow(4);
        }
    }

    private void ScorePerSecondManager()
    {
        if (Player_UI.Instance.GetScorePerSecond() < maxScorePerSecond && Player_UI.Instance != null ) Player_UI.Instance.SetScorePerSecond(startScorePerSecond + ((int)currentDifficulty * increaseScorePerState));
    }

    private void TimeGapBtwEachSpawnManager()
    {
        if (ObstacleSpawner.Instance.GetMaxTimeGapBtwEachSpawn() >= minTimeGapBtwEachSpawn) ObstacleSpawner.Instance.SetMaxTimeGapBtwEachSpawn(startTimeGapBtwEachSpawn - ((int)currentDifficulty * decreasePerState));
    }

    private void MoveSpeedManager()
    {
        if (SettingVariableContainer.Instance.GetMoveSpeed() < maxSpeed) SettingVariableContainer.Instance.SetMoveSpeed(startSpeed + ((int)currentDifficulty * increasePerState));
    }

}
















//private void ValueCangerOverDifficultyLevel()
//{
//    switch (currentDifficulty)
//    {
//        case Difficulty.Easy:
//            // Set values for Easy difficulty

//            break;
//        case Difficulty.Normal:
//            // Set values for Normal difficulty
//            break;
//        case Difficulty.Medium:
//            // Set values for Medium difficulty
//            break;
//        case Difficulty.Difficult:
//            // Set values for Difficult difficulty
//            break;
//        case Difficulty.NearlyImpossible:
//            // Set values for Nearly Impossible difficulty
//            break;
//    }
//}

//will apply this after an update
