using System;
using UnityEngine;

public class CarAudioManager : MonoBehaviour
{
    public static CarAudioManager Instance { get; private set; }

    public event EventHandler OnCarAudioPlay;
    public event EventHandler OnCarAudioPause;

    [SerializeField] private AudioSource carAudioSource;

    private bool playAudio = false;

    public bool carStarted = false;

    private void Awake()
    {
        Instance = this;

        playAudio = false;
    }

    private void Update()
    {
        

        if (carStarted)
        {

            if (!PlayerCollisionDetection.Instance.GetIfIsDead())
            {
                playAudio = true;
            }
            else
            {
                playAudio = false;
            }
        }
        else
        {
            playAudio = false;
        }

        if (playAudio)
        {
            carAudioSource.enabled = true;
        }
        else
        {
            carAudioSource.enabled = false;
        }
    }
}
