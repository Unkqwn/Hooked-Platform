using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.mainMenu = gameObject;

        GameManager.Instance.mainMenu.SetActive(true);
        GameManager.Instance.optionsMenu.SetActive(false);
        GameManager.Instance.creditsMenu.SetActive(false);
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene(GameManager.Instance.gameSceneName);
    }

    public void OnOptionsButton()
    {
        GameManager.Instance.optionsMenu.SetActive(true);
        GameManager.Instance.mainMenu.SetActive(false);
    }

    public void OnCreditsButton()
    {
        GameManager.Instance.creditsMenu.SetActive(true);
        GameManager.Instance.mainMenu.SetActive(false);
    }

    public void OnQuitButton()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
