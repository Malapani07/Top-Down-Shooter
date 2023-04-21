
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 TargetPosition;
    private float _progress;

    [SerializeField] private float speed = 40f;

    void Start()
    {
        startPosition = transform.position.WithAxis(Axis.Z, -1);
    }

    // Update is called once per frame
    void Update()
    {
        _progress += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startPosition, TargetPosition, _progress);
    }

    public void setTargetPosition(Vector3 Target)
    {
        TargetPosition = Target.WithAxis(Axis.Z, -1);
    }
}
