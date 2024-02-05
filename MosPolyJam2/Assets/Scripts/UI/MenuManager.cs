using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoScreen;
    public GameObject SounManager;
    public string nextSceneName;

    private void Start()
    {
        SounManager.SetActive(true);
        videoPlayer.enabled = false;
        videoPlayer.loopPointReached += EndReached;
    }

    public void StartVideo()
    {
        SounManager.SetActive(false);
        videoScreen.SetActive(true);
        videoPlayer.enabled = true;
        videoPlayer.Play();
    }

    private void EndReached(VideoPlayer vp)
    {
        SwitchToNextScene();
    }

    void SwitchToNextScene()
    {
        videoPlayer.enabled = false;
        LoadScene(nextSceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

}
