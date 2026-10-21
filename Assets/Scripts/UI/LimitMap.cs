using UnityEngine;

public class LimitMap : MonoBehaviour
{
   
    public GameObject player;

     void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, 40, player.transform.position.z);
    }
}
