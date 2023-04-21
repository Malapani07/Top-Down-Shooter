
using UnityEngine;
using UnityEngine.UI;
public class PlayerInfo : MonoBehaviour, IHittable
{
    [SerializeField]
    public Image HealthBar;
    [SerializeField]
    private ParticleSystem blood;

    [SerializeField] private GameObject MissionPanel;
    [SerializeField] GameObject losePanel;

    private void Start()
    {
        healthUpdate();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            if (MissionPanel.activeSelf) 
            {
                MissionPanel.SetActive(false);
            }
            else
            {
                MissionPanel.SetActive(true);
            }           
        }
    }

    public void healthUpdate()
    {
        HealthBar.fillAmount -= 0.05f;
        if (HealthBar.fillAmount == 0f)
        {
            Time.timeScale = 0f;
            FindObjectOfType<AudioManager>().Play("Losing");
            losePanel.SetActive(true);
        }
    }

    public void ReceiveHit(RaycastHit2D raycastHit2D)
    {
        Attack();
    }

    public void Attack()
    {
        FindObjectOfType<AudioManager>().Play("Pain");
        Instantiate(blood, this.transform.position, Quaternion.identity);
        blood.Play();
        healthUpdate();
    }

}
