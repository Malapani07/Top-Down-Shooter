
using UnityEngine;

public class FireBall : MonoBehaviour
{
    GameObject target;
    int Power = 20;
    private Rigidbody2D rb;
    [SerializeField]private ParticleSystem Blast;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        rb.AddForce(transform.up * Power);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target.GetComponent<PlayerInfo>().Attack();
            Destroy(this.gameObject);
        }
        else
        {
            Instantiate(Blast, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
