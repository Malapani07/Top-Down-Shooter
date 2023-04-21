using UnityEngine;
using Pathfinding;

public class Wandering : MonoBehaviour,IHittable
{
    public float radius = 15;
    private GameObject Player;
    [SerializeField] public AIPath ai;
    [SerializeField] private ActionState state;
    [SerializeField] private ParticleSystem Death;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        state = new ActionState();
        state = ActionState.Patrol;
    }
    Vector3 PickRandomPoint()
    {
        var point = Random.insideUnitCircle * radius;
        point.y = 0;
        point += (Vector2)ai.position;
        return point;
    }
    void Update()
    {
        // Update the destination of the AI if
        // the AI is not already calculating a path and
        // the ai has reached the end of the path or it has no path at all
        switch (state)
        {
            case ActionState.Patrol:
                Patrol();
                break;

            case ActionState.MovingToTargert:
                MovingToTargert();
                break;
        }
       
    }

    void Patrol()
    {
       
        if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
        {
            ai.destination = PickRandomPoint();
            ai.SearchPath();
        }
        if (Vector3.Distance(this.transform.position, Player.transform.position) < 15f)
        {
            state = ActionState.MovingToTargert;
        }
    }

    void MovingToTargert()
    {
        ai.destination = Player.transform.position;
        if (Vector3.Distance(Player.transform.position, transform.position) > 15f)
        {
            state = ActionState.Patrol;
        }
    }

    public void ReceiveHit(RaycastHit2D hit)
    {
        Instantiate(Death, this.transform.position, Quaternion.identity);
        FindObjectOfType<AudioManager>().Play("Death");
        Player.GetComponent<PlayerMove>().kills++;
        Player.GetComponent<PlayerMove>().TextChangekills();
        Destroy(this.gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.gameObject.GetComponent<PlayerInfo>().Attack();
            this.transform.position -= new Vector3(1f, 1f, 1f);

        }
    }


}
