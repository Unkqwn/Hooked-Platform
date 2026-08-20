using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnStartButton()
    {
        GameManager.Instance.StartGame();
    }

    public void OnOptionsButton()
    {
        GameManager.Instance.OpenOptionsMenu();
    }

    public void OnCreditsButton()
    {
        GameManager.Instance.OpenCreditsMenu();
    }

    public void OnQuitButton()
    {
        GameManager.Instance.QuitGame();
    }
}
