using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMPro.TMP_Text AnimalCount;
    public TMPro.TMP_Text MaxAnimals;

    public TMPro.TMP_Text FireCount;
    public TMPro.TMP_Text MaxFires;

    [SerializeField] private GameObject pauseMenu;
    private bool _isPaused;

    private void Start()
    {
        MaxAnimals.SetText("/" + LevelManager.Instance.KoalaGoal.ToString());
        MaxFires.SetText("/" + LevelManager.Instance.MinTrees.ToString());
    }

    private void Update()
    {
        AnimalCount.SetText(LevelManager.Instance.KoalasSaved.ToString());
        FireCount.SetText(LevelManager.Instance.TreeObjects.Count.ToString());

        if (Input.GetKeyDown(KeyCode.Escape))
            _isPaused = !_isPaused;

        TogglePauseMenu(_isPaused);
        Cursor.visible = _isPaused;
    }

    public void TogglePauseMenu(bool isPaused)
    {
        switch (isPaused)
        {
            case true:
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                pauseMenu.SetActive(true);
                break;
            case false:
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                pauseMenu.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void ContinueGame()
    {
        _isPaused = false;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
