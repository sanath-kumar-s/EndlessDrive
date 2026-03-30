using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuesHandler : MonoBehaviour
{
    [SerializeField] private Transform gameOverMenu;
    [SerializeField] private Transform mainMenu;

    [SerializeField] private Transform playerMenu;
    [SerializeField] private Transform GameStartCountDownMenu;

    [SerializeField] private float gameOverMenuOpenDelay = 1f;

    private bool isDead;


    private void Awake()
    {


        playerMenu.gameObject.SetActive(false);
        gameOverMenu.gameObject.SetActive(false);
        GameStartCountDownMenu.gameObject.SetActive(false);
        isDead = false;
    }

    




    


    private void Update()
    {
        isDead = PlayerCollisionDetection.Instance.GetIfIsDead();


        if (isDead)
        {
            StartCoroutine(OpenGameOverMenu());
        }

        
    }

    private IEnumerator OpenGameOverMenu()
    {
        yield return new WaitForSeconds(gameOverMenuOpenDelay);
        gameOverMenu.gameObject.SetActive(true);

    }

   

}

