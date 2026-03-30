using UnityEngine;

public class SettingVariableContainer : MonoBehaviour
{
    public static SettingVariableContainer Instance;

    public float moveSpeed = 5f;
    [Tooltip("Dont Forget To Change Value in GrassSpawner Script!")]


    private void Awake()
    {
        Instance = this;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
