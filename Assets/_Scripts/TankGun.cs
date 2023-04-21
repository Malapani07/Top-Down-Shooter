
using UnityEngine;

public class TankGun : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D rb;
    [Range(0, 360)][SerializeField] float offset;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gunpoint;
    float time = 1;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var direction = target.position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.MoveRotation(angle + offset);
        if (Vector3.Distance(target.position, transform.position) < 20f)
        {
            if (time <= 0)
            {
                shoot();
                time = 1f;
            }
        }
        time-= Time.deltaTime;
    }
    void shoot()
    {
        Instantiate(bullet, gunpoint.position, transform.rotation);
        FindObjectOfType<AudioManager>().Play("Tank");
    }
}
