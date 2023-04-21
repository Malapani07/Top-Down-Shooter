
using UnityEngine;

public class Sizeing : MonoBehaviour
{
    private BoxCollider2D bx;
    private SpriteRenderer sr;
    void Start()
    {
        bx= GetComponent<BoxCollider2D>();
        sr= GetComponent<SpriteRenderer>();
        bx.size=sr.size;
    }

}
