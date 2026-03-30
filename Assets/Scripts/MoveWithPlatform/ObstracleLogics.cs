using UnityEngine;

public class ObstracleLogics : MonoBehaviour
{
    [SerializeField] float platformLength_ = 50f;

    private bool isDead = false;

    public void Update()
    { 
        isDead = PlayerCollisionDetection.Instance.GetIfIsDead();
        if (isDead) return;

        Move();

    }

    private void Move()
    {
        float moveSpeed_ = SettingVariableContainer.Instance.moveSpeed;


        // Move the platform backward
        transform.Translate(Vector3.back * moveSpeed_ * Time.deltaTime);




        // If platform is behind the player, move it ahead
        if (transform.position.z < 0 - platformLength_)
        {
            Destroy(gameObject);

        }
    }
}
