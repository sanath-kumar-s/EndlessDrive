using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_GameOver : MonoBehaviour
{
    public static UI_GameOver Instance;

    public void Restart()
    {

        StartCoroutine(Restart_IEnumerator());

    }

    private IEnumerator Restart_IEnumerator()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
