using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.optionsMenu = gameObject;
    }

    public void OnReturnButton()
    {
        GameManager.Instance.mainMenu.SetActive(true);
        GameManager.Instance.optionsMenu.SetActive(false);
    }
}
