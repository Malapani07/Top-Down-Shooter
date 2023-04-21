
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]private GameObject Enemy;

    [SerializeField, Range(1, 20)] private int No_Of_Enemy;

    void Start()
    {
        Spawn();
    }

   void Spawn()
    {
        for (int i = 0; i < No_Of_Enemy; i++)
        {
            Instantiate(Enemy,this.transform.position, Quaternion.identity);
        }
    }
   
}
