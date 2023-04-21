
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] private Transform Player;
    void LateUpdate()
    {
       this.transform.position = Player.transform.position.WithAxis(Axis.Z, -10);
    }
}
