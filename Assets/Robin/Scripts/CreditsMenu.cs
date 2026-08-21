using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.creditsMenu = gameObject;
    }

    public void OnBackButton()
    {
        GameManager.Instance.mainMenu.SetActive(true);
        GameManager.Instance.creditsMenu.SetActive(false);
    }
}
