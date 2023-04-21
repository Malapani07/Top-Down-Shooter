using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D rb;
    public int kills;
    public int files;
    public int BombsDiffuse;
    public int Diamonds;
    [SerializeField] private Transform gunPoint;
    [SerializeField] private GameObject bulletTrail;
    [SerializeField] private float WeaponRange = 10f;
    [SerializeField] private Animator muzzleFlashAnimation;
    [SerializeField]public Text text;
   
    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        kills = 0;
    }

  
    void Update()
    {    
        lookAt();
        shoot();
        Move();    
    }

    public void TextChangekills()
    {
        text.text = "Kills=" + kills;
    }
    public void TakeFile()
    {
        text.text = "Files=" + files;
    }
    public void TakeDiamond()
    {
        text.text = "Diamonds=" + Diamonds;
    }
    void lookAt()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = (Vector3)(mouse - new Vector2(transform.position.x, transform.position.y));
    }
    private void Move()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = input.normalized * speed;
    }
    private void shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            muzzleFlashAnimation.SetTrigger("Shoot");
            FindObjectOfType<AudioManager>().Play("GunShoot");
            var hit = Physics2D.Raycast(gunPoint.position, transform.up, WeaponRange);
            var trail = Instantiate(bulletTrail, gunPoint.position, transform.rotation);
            var trailscript = trail.GetComponent<BulletTrail>();

            if (hit.collider != null)
            {
                trailscript.setTargetPosition(hit.point);
                var hittable = hit.collider.GetComponent<IHittable>();
                if (hittable != null)
                {
                    hittable.ReceiveHit(hit);
                }
            }
            else
            {
                var endPosition = gunPoint.position + transform.up * WeaponRange;
                trailscript.setTargetPosition(endPosition);
            }
        }
    }

    public bool checkTasksM1()
    {
        if (kills == 12 && BombsDiffuse == 4)
        {
            return true;
        }
        return false;
    }
    public bool checkTasksM2()
    {
        if (kills == 12 && files == 10)
        {
            return true;
        }
        return false;
    }
    public bool checkTasksM5()
    {
        if (kills == 15 && Diamonds == 20)
        {
            return true;
        }
        return false;
    }
    public bool checkTasksM3()
    {
        if (kills == 12)
        {
            return true;
        }
        return false;
    }

    public bool checkTasksM6()
    {
        if (kills == 20 && Diamonds == 15)
        {
            return true;
        }
        return false;
    }

    public bool checkTasksM4()
    {
        if (kills == 30)
        {
            return true;
        }
        return false;
    }
}
