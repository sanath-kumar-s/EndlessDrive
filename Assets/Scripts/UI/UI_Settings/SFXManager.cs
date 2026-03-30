using UnityEngine;
using UnityEngine.UI;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { get; private set; }

    [SerializeField] private Scrollbar sfxScrollbar;

    private float sfxVolume;

    private const string SFXVolumeKey = "SFXVolume";



    public void SFXVolumeSetter(float sfxVolume)
    {
        this.sfxVolume = sfxVolume;
        AudioVolumeManager.Instance.SetSFXVolume(sfxVolume);

    }



    public void SFXVolumeChanged()
    {
        AudioVolumeManager.Instance.OnSFXVolumeChanged();


    }


}
