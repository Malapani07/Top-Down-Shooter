using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public GameObject PausePanel;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PausePanel.activeSelf)
            {
                PausePanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                PausePanel.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    public void resume()
    {
        FindObjectOfType<AudioManager>().Play("click");
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        FindObjectOfType<AudioManager>().Play("click");
        Application.Quit();
    }
    public void Menu()
    {
        FindObjectOfType<AudioManager>().Play("click");
        SceneManager.LoadScene("Menu");
    }

}
