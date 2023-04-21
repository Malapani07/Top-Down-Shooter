using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public int level_unlocked;
   [SerializeField] private Button[] Buttons;

    // Start is called before the first frame update
    void Start()
    {

        level_unlocked = PlayerPrefs.GetInt("level_unlocked",1);

        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].interactable = false;
        }

        for (int i = 0; i < level_unlocked; i++)
        {
            Buttons[i].interactable = true;
        }


    }
    public void Play()
    {
        FindObjectOfType<AudioManager>().Play("Click");
    }
    
    public void loadMission(int index)
    {
        FindObjectOfType<AudioManager>().Play("Click");
        SceneManager.LoadScene(index);
    }

    public void Quit()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Application.Quit();
    }
}
