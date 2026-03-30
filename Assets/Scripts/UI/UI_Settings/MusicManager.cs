using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private Scrollbar musicScrollbar;

    private const string BackgroundMusicKey = "BackgroundMusicVolume";

    private void Awake()
    {


    }

    public void ChangeBackgroundMusicValue(float musicValue)
    {
        AudioVolumeManager.Instance.SetMusicVolume(musicValue);


    }

    public void OnValueChange()
    {
        AudioVolumeManager.Instance.OnMusicVolumeChanged();

    }
}
