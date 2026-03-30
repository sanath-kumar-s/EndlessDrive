using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float transitionTime = 1f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load_Scene(1);
        }
    }

    public void Load_Scene(string sceneName)
    {
        StartCoroutine(LoadSceneCourotine(sceneName));
    }
    
    public void Load_Scene(int sceneIndex)
    {
        StartCoroutine(LoadSceneCourotine(sceneIndex));
    }

    private IEnumerator LoadSceneCourotine(int LevelIndex)
    {
        _animator.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(LevelIndex);
    }
    
    private IEnumerator LoadSceneCourotine(string LevelString)
    {
        _animator.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(LevelString);
    }
}
