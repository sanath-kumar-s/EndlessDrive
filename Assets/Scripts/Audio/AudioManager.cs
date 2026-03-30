using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip countDown;
    [SerializeField] private AudioClip UIDrag;
    [SerializeField] private AudioClip GameOver;
    [SerializeField] private AudioClip NewHighScore;
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip jump;

    [Header("Settings")]
    [SerializeField] private float maxPitch = 1.2f;
    [SerializeField] private float minPitch = .8f;
    [SerializeField] private Transform player;

    private AudioSource _audioSource;
    private float audioVolume = 0f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCountDown.Instance.OnCountDownValueChanged += StartCountDown_OnCountDownValueChanaged;
        UI_MainMenu.Instance.OnPlayButtonPressed += UI_MainMenu_OnPlayButtonPressed;
        PlayerCollisionDetection.Instance.OnPlayerDead += PlayerCollisionDetection_OnPlayerDead;
        if(UI_HighScoreManager.Instance != null)UI_HighScoreManager.Instance.OnNewHighScore += UI_HighScoreManager_OnHighScoreUpdated;
        PlayerMovement.Instance.OnPlayerJump += PlayerMovement_OnJump;
    }

    private void Update()
    {
        audioVolume = PlayerPrefs.GetFloat("SFXVolume", .5f);

    }



    public void PlayAudio(AudioClip audioClip,Transform parent ,out AudioSource _audioSource ,bool randomPitch = false, bool doLoop = false)
    {




        GameObject audioObj = new GameObject("Audio_" + audioClip.name);
        audioObj.transform.parent = parent;
        audioObj.SetActive(true);

        AudioSource audioSource = audioObj.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.loop = doLoop;
        audioSource.playOnAwake = false;

        if (randomPitch)
        {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
        }
        else
        {
            audioSource.pitch = 1f;
        }

        audioSource.Play();

        if (!doLoop)
        {
            Destroy(audioObj, audioClip.length / audioSource.pitch);
        }

        _audioSource = audioSource;


    }
    private void PlayerCollisionDetection_OnPlayerDead(object sender, System.EventArgs e)
    {
        PlayAudio(GameOver, transform, out _audioSource,false, false);
        _audioSource.volume = audioVolume;
    }
    private void StartCountDown_OnCountDownValueChanaged(object sender, System.EventArgs e)
    {
        PlayAudio(countDown, transform, out _audioSource, false, false);
        _audioSource.volume = audioVolume;

    }
    private void UI_MainMenu_OnPlayButtonPressed(object sender, System.EventArgs e)
    {
        PlayAudio(UIDrag, transform, out _audioSource, false, false);
        _audioSource.volume = audioVolume;

    }
    private void UI_HighScoreManager_OnHighScoreUpdated(object sender, System.EventArgs e)
    {
        PlayAudio(NewHighScore, transform, out _audioSource, false, false);
        _audioSource.volume = audioVolume;

    }
    private void PlayerMovement_OnJump(object sender, System.EventArgs e)
    {
        PlayAudio(jump, transform, out _audioSource, false, false);
        _audioSource.volume = audioVolume;

    }

    public void OnButtonClick()
    {
        PlayAudio(buttonClick, transform, out _audioSource, true, false);
        _audioSource.volume = audioVolume;

    }

    public void OnTabSwiped()
    {
        PlayAudio(UIDrag, player, out _audioSource, true, false);

        _audioSource.spatialBlend = 1f; // Set to 3D sound

    }








}
