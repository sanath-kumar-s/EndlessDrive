using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeManager : MonoBehaviour
{
    public static AudioVolumeManager Instance;

    [SerializeField] private Scrollbar sfxScrollbar;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private Scrollbar musicScrollbar;



    private float MusicVolume;
    private float SFXVolume;

    private const string MusicVolumeKey = "BackgroundMusicVolume";
    private const string SFXVolumeKey = "SFXVolume";

    private void Awake()
    {
        Instance = this;

        SFXVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 1f);
        sfxScrollbar.value = SFXVolume;

        backgroundMusic.volume = PlayerPrefs.GetFloat(MusicVolumeKey, 0f);
        musicScrollbar.value = backgroundMusic.volume;
    }

    private void Update()
    {
        MusicVolumeValueHandler();
        
    }

    private void MusicVolumeValueHandler()
    {
        backgroundMusic.volume = MusicVolume;
        musicScrollbar.value = backgroundMusic.volume;



    }






#region MusicVolumeValue

    public void SetMusicVolume(float volume)
    {
        MusicVolume = volume;

    }

    public float GetMusicVolume()
    {
        return MusicVolume;
    }

    public void OnMusicVolumeChanged()
    {

        PlayerPrefs.SetFloat(MusicVolumeKey, backgroundMusic.volume);
        PlayerPrefs.Save();
    }

    #endregion
#region SFXVolumeValue
    public void SetSFXVolume(float volume)
    {
        SFXVolume = volume;
    }
    public float GetSFXVolume()
    {
        return SFXVolume;
    }

    public void OnSFXVolumeChanged()
    {
        PlayerPrefs.SetFloat(SFXVolumeKey, SFXVolume);
        PlayerPrefs.Save();
    }

    #endregion

}
