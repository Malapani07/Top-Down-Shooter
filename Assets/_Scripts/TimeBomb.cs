
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TimeBomb : MonoBehaviour
{
    [SerializeField] private float triggerRange = 4f;
    [SerializeField] Text Message;
    bool isdefused = false;
    [SerializeField] GameObject Winningpanel;
    Transform display = null;
    private PlayerMove player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(this.transform.position, transform.up, triggerRange);
        if (hitInfo)
        {
            if (hitInfo.collider.CompareTag("Bomb"))
            {
                display = hitInfo.collider.gameObject.transform.GetChild(0).GetComponent<Transform>();
                if (display.localScale.y < 1) { Message.text = "Press E"; }
                else { Message.text = "Bomb Defused"; }

                if ((Input.GetKeyDown(KeyCode.E) && !isdefused) && player.checkTasksM3())
                {
                    if (display.localScale.y < 1)
                    {
                        display.localScale += new Vector3(0f, 0.2f, 0f);
                        if (display.localScale.y == 1f)
                        {
                            player.BombsDiffuse++;
                        }
                    }
                    if (display.localScale.y == 1f)
                    {
                        Message.text = "Bomb Defused";
                        winning();
                    }

                }
                return;
            }
        }
        Message.text = string.Empty;
    }
    public void winning()
    {
        Time.timeScale = 0f;
        Winningpanel.SetActive(true);
        FindObjectOfType<AudioManager>().Play("Winning");
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            PlayerPrefs.SetInt("level_unlocked", 4);
        }
    }
}
