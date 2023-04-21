using Pathfinding;
using UnityEngine;


public class Enemy : MonoBehaviour, IHittable
{
    public Transform Player;
    public Transform[] Paths;
    [SerializeField] private ParticleSystem Death;
    [SerializeField] private ActionState state;
    [SerializeField] private GameObject bulletTrail;
    [SerializeField] private float WeaponRange = 10f;
    [SerializeField] private Transform gunPoint;
    float time = 1f;
    int counter = 0;
    private AIDestinationSetter AiD;
    private AIPath aipath;

    [SerializeField]private int BulletsTakeCapacity;

    void Start()
    {
        state = new ActionState();
        state = ActionState.Patrol;
        AiD = GetComponent<AIDestinationSetter>();
        aipath = GetComponent<AIPath>();
        AiD.target = Paths[counter];
        BulletsTakeCapacity = Random.Range(1, 4);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case ActionState.Patrol:
                Patrol();
                break;

            case ActionState.MovingToTargert:
                MovingToTargert();
                break;

            case ActionState.Shooting:

                if (time <= 0)
                {
                    shoot();
                    time = 1f;
                }
                if (Vector3.Distance(Player.position, transform.position) > 10f)
                {
                    state = ActionState.Patrol;
                    AiD.target = Paths[counter % Paths.Length];
                }
                break;
        }
        time -= Time.deltaTime;
    }

    void Patrol()
    {

        if (transform.position == AiD.target.position)
        {
            counter++;
            AiD.target = Paths[counter % Paths.Length];
        }
        if (Vector3.Distance(Player.position, transform.position) < 10f)
        {
            state = ActionState.MovingToTargert;
        }
    }

    void MovingToTargert()
    {
        AiD.target = Player;
        if (Vector3.Distance(Player.position, transform.position) <= 8f)
        {
            state = ActionState.Shooting;
        }
    }

    public void ReceiveHit(RaycastHit2D hit)
    {
        Instantiate(Death, this.transform.position, Quaternion.identity);
        FindObjectOfType<AudioManager>().Play("Death");
        BulletsTakeCapacity--;
        if (BulletsTakeCapacity == 0)
        {
            Deadth();
        }
    }

    private void Deadth()
    {
        Player.gameObject.GetComponent<PlayerMove>().kills++;
        Player.gameObject.GetComponent<PlayerMove>().TextChangekills();

        for (int i = 0; i < Paths.Length; i++)
        {
            Destroy(Paths[i].gameObject);
            Paths[i] = null;
        }
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.gameObject.GetComponent<PlayerInfo>().Attack();
        }
    }

    void shoot()
    {
        transform.up = AiD.target.position - transform.position;
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

