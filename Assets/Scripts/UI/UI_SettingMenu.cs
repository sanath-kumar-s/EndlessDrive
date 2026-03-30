using System; 
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class UI_SettingMenu : MonoBehaviour
{
    private Animator _animator;
    private const string IS_OPEN = "IsOpen";

    [SerializeField] private Transform options;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.SetBool(IS_OPEN, true);
    }

    public void CloseMenu()
    {
        StartCoroutine(CloseMenuCoroutine()); 
    }

    public IEnumerator CloseMenuCoroutine()
    {
        _animator.SetBool(IS_OPEN, false);

        options.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f); // Wait for the animation to finish

        gameObject.SetActive(false);
    }

    private void Start()
    {
        UI_MainMenu.Instance.OnSettingsOpened += OnSettingsOpened;
    }

    private void OnSettingsOpened(object sender, EventArgs e)
    {

    }
}
