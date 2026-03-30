using UnityEngine;

public class BackButton_UI : MonoBehaviour
{
    [SerializeField] private GameObject QualityMenu;
    [SerializeField] private GameObject AudioMenu;

    private void Awake()
    {
        QualityMenu.SetActive(false);
        AudioMenu.SetActive(false);
    }

    public void GoBack()
    {
        QualityMenu.SetActive(false);
        AudioMenu.SetActive(false);
    }

    public void OpenQualityMenu()
    {
        QualityMenu.SetActive(true);
    }

    public void OpenAudioMenu()
    {
        AudioMenu.SetActive(true);
    }


}
