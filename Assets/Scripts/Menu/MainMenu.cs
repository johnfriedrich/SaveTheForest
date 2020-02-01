using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuList;
    [SerializeField] private GameObject creditsScreen;
    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowCreditScreen()
    {
        creditsScreen.SetActive(true);
        menuList.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        menuList.SetActive(true);
        creditsScreen.SetActive(false);
    }
}
