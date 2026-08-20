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
    public static GameManager Instance { get; private set; }

    public GameState CurrentGameState { get; private set; }

    [Header("Scene Names")]
    [SerializeField] private string mainMenuSceneName;
    [SerializeField] private string gameSceneName;
    [SerializeField] private string victorySceneName;
    [SerializeField] private string defeatSceneName;

    [Header("Menus")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private bool optionsMenuEnabled, creditsMenuEnabled = false;

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

        if (mainMenu == null || pauseMenu == null || optionsMenu == null || creditsMenu == null)
        {
            Debug.LogError("One or more menu GameObjects are not assigned in the GameManager.");
        }

        CurrentGameState = GameState.MenuScreen;
    }

    public void MainMenu()
    {
        CurrentGameState = GameState.MenuScreen;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void StartGame()
    {
        CurrentGameState = GameState.Playing;
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenOptionsMenu()
    {
        optionsMenuEnabled = true;
        CurrentGameState = GameState.Paused;
        MenuLoad();
    }

    public void OpenCreditsMenu()
    {
        creditsMenuEnabled = true;
        CurrentGameState = GameState.Paused;
        MenuLoad();
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

    private void MenuLoad()
    {
        optionsMenu.SetActive(optionsMenuEnabled);
        creditsMenu.SetActive(creditsMenuEnabled);
        
        pauseMenu.SetActive(CurrentGameState == GameState.Paused);
    }
}
