using System;
using UnityEngine;

public class Base_MovableWithPlatform: MonoBehaviour
{
    [HideInInspector] public float moveSpeed_;
    [Tooltip("Dont Forget To Change Value in GrassSpawner Script!")]
    [HideInInspector] public float startSpeed_;
    [HideInInspector] public float maxSpeed_;

    public float platformLength_ = 50f; // Length of the platform
    public static Transform player;

    private bool isDead;


    // Reference to player/camera to detect "behind" state

    private void Start()
    {

        player = PlayerMovement.Instance._player;
        isDead = false;
    }



    private void Update()
    {
        isDead = PlayerCollisionDetection.Instance.GetIfIsDead();
        if (isDead)return;
        
        Move();

        SettingVariableContainer settingVariableContainer = SettingVariableContainer.Instance;

        moveSpeed_ = settingVariableContainer.moveSpeed;


    }

    public virtual void Move()
    {
        // Move the platform backward
        transform.Translate(Vector3.back * moveSpeed_ * Time.deltaTime);

        


        // If platform is behind the player, move it ahead
        if (transform.position.z < 0 - platformLength_)
        {
            transform.position += new Vector3(0, 0, platformLength_ * 3); // Move ahead by total length of all platforms

        }

    }


}
