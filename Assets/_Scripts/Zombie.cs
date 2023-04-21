using Pathfinding;
using UnityEngine;

public enum ActionState
{
    Patrol,
    MovingToTargert,
    Shooting
}

public class Zombie : MonoBehaviour, IHittable
{
    public Transform Player;
    [SerializeField]private Transform[] Paths;
    [SerializeField] private ParticleSystem Death;
    [SerializeField] private ActionState state;
    private AIDestinationSetter AiD;
    int counter = 0;

    void Start()
    {
        state = new ActionState();
        state = ActionState.Patrol;
        AiD = GetComponent<AIDestinationSetter>();
        AiD.target = Paths[counter];
        
    }

   
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
        }
    }

    void Patrol()
    {
        if (transform.position == AiD.target.position)
        {
            counter++;
            AiD.target = Paths[counter % Paths.Length];
        }
        if (Vector3.Distance(Player.position, transform.position) < 15f)
        {
            state = ActionState.MovingToTargert;
        }
    }

    void MovingToTargert()
    {
        AiD.target = Player;
        if (Vector3.Distance(Player.position, transform.position) > 15f)
        {
            state = ActionState.Patrol;
            AiD.target = Paths[counter % Paths.Length];
        }
    }

    public void ReceiveHit(RaycastHit2D hit)
    {
        Deadth();
    }

    private void Deadth()
    {
        Instantiate(Death, this.transform.position, Quaternion.identity);
        FindObjectOfType<AudioManager>().Play("Death");
        Player.GetComponent<PlayerMove>().kills++;
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.gameObject.GetComponent<PlayerInfo>().Attack();
            this.transform.position -= new Vector3(1f, 1f, 0f);
        }
    }


}
