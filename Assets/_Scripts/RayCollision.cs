using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RayCollision : MonoBehaviour
{
    [SerializeField] private float triggerRange = 4f;
    [SerializeField] Text Message;
    bool isOpened=false;
    bool isdefused = false;
    bool isFlaged=false;
    Transform display = null;
    [SerializeField] Sprite myFlag;
    private PlayerMove player;
    [SerializeField] GameObject Winningpanel;
    PlayerInfo playerinfo;
    private void Start()
    {
        player = FindObjectOfType<PlayerMove>();
        playerinfo = FindObjectOfType<PlayerInfo>();
    }
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(this.transform.position, transform.up, triggerRange);
        if (hitInfo)
        {
            //Door
            if (hitInfo.collider.CompareTag("Door"))
            {
                Message.text = "Press O";
                var gateAnim = hitInfo.collider.GetComponent<Animator>();
                if (Input.GetKeyDown(KeyCode.O) && !isOpened)
                {
                    isOpened = true;
                    gateAnim.SetBool("isOpen", true);
                    FindObjectOfType<AudioManager>().Play("Door");

                }
                else if (Input.GetKeyDown(KeyCode.O) && isOpened)
                {
                    isOpened = false;
                    gateAnim.SetBool("isOpen", false);
                    FindObjectOfType<AudioManager>().Play("Door");
                }
                return;
            }

            //Bomb
            else if (hitInfo.collider.CompareTag("Bomb"))
            {
                display = hitInfo.collider.gameObject.transform.GetChild(0).GetComponent<Transform>();
                if (display.localScale.y < 1) { Message.text = "Press E"; }
                else { Message.text = "Bomb Defused"; }

                if (Input.GetKeyDown(KeyCode.E) && !isdefused)
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
                    }

                }
                return;
            }

            else if (hitInfo.collider.CompareTag("Flag"))
            {
                Message.text = "Press I";
                if ((Input.GetKeyDown(KeyCode.I) && !isFlaged && player.checkTasksM1()) || Input.GetKeyDown(KeyCode.I) && player.checkTasksM2() || Input.GetKeyDown(KeyCode.I) && player.checkTasksM5() || Input.GetKeyDown(KeyCode.I) && player.checkTasksM6() || Input.GetKeyDown(KeyCode.I) && player.checkTasksM4())
                {
                    var enemyflag = hitInfo.collider.GetComponent<SpriteRenderer>();
                    enemyflag.sprite = myFlag;
                    Time.timeScale = 0f;
                    Winningpanel.SetActive(true);
                    FindObjectOfType<AudioManager>().Play("Winning");
                    MakeLevel();
                    isFlaged = true;
                }
                return;
            }

            else if (hitInfo.collider.CompareTag("File"))
            {
                Message.text = "Press C";
                if (Input.GetKeyDown(KeyCode.C))
                {
                    Destroy(hitInfo.collider.gameObject);
                    player.files++;
                    player.TakeFile();
                }
                return;
            }

            else if (hitInfo.collider.CompareTag("Diamond"))
            {
                Message.text = "Press C";
                if (Input.GetKeyDown(KeyCode.C))
                {
                    Destroy(hitInfo.collider.gameObject);
                    player.Diamonds++;
                    player.TakeDiamond();
                }
                return;
            }

            else if (hitInfo.collider.CompareTag("HealthKit"))
            {
                Message.text = "Press H";
                if (Input.GetKeyDown(KeyCode.H))
                {
                    Destroy(hitInfo.collider.gameObject);
                    playerinfo.HealthBar.fillAmount+=1f;
                }
                return;
            }
        }
        Message.text = string.Empty;
    }

    public void MakeLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            PlayerPrefs.SetInt("level_unlocked", 2);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            PlayerPrefs.SetInt("level_unlocked", 3);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            PlayerPrefs.SetInt("level_unlocked", 4);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            PlayerPrefs.SetInt("level_unlocked", 5);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            PlayerPrefs.SetInt("level_unlocked", 6);
        }
    }
}
