using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCollisionDetection : MonoBehaviour
{

    public static PlayerCollisionDetection Instance;

    public event EventHandler OnPlayerDead;

    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask obstracleLayer;
    [SerializeField] private Vector3 halfExtents = new Vector3(0.5f, 0.1f, 0.5f); // Half extents of the box cast

    private Animator _animator;
    private const string DIE = "Dead";

    private bool isDead;

    private bool collisionDetected = false;

    private void Awake()
    {
        Instance = this;

        _animator = GetComponent<Animator>();
    }


    private void Update()
    {
        RaycastHit hit;

        // Center of box cast is the player's position
        Vector3 center = transform.position;

        // Half extents of the box (make Y small to only check slightly below feet)



        // Distance to cast
        float castDistance = 0.2f;



        // Perform the BoxCast
        if (Physics.BoxCast(center, halfExtents, Vector3.forward, out hit, Quaternion.identity, castDistance, obstracleLayer) && !collisionDetected)
        {
            //Player Died

            OnPlayerDead?.Invoke(this, EventArgs.Empty);


            if(!isDead)_animator.SetTrigger(DIE);

            isDead = true;

            collisionDetected = true;
        }


    }

    

    public bool GetIfIsDead()
    {
        return isDead;
    }
}

