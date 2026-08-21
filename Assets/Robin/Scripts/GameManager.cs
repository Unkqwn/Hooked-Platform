using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    MenuScreen,
    Playing,
    Victory,
    Defeated,
    Paused
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState CurrentGameState { get; private set; }

    [Header("Scene Names")]
    public string mainMenuSceneName;
    public string gameSceneName;
    public string victorySceneName;
    public string defeatSceneName;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject creditsMenu;
    public GameObject pauseMenu;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        CurrentGameState = GameState.MenuScreen;
    }

    public void VictoryMenu()
    {
        CurrentGameState = GameState.Victory;
        SceneManager.LoadScene(victorySceneName);
    }

    public void DefeatMenu()
    {
        CurrentGameState = GameState.Defeated;
        SceneManager.LoadScene(defeatSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
