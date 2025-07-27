using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject backToMenuButton;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Text volumeText;

    public void StartScene()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void UpdateVolume()
    {
        float vol = volumeSlider.value;
        AudioListener.volume = vol;
        volumeText.text = $"Volume {Mathf.RoundToInt(vol * 100)}%";
    }
}
