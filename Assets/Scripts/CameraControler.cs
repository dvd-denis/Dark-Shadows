using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public GameObject player;
    public float plusY = 8;
    public float minusZ = 2.5f;

    private void Update()
    {
        if (player != null)
        {
            Vector3 vector3 = new Vector3(player.transform.position.x, player.transform.position.y + plusY, player.transform.position.z - minusZ);
            this.transform.position = vector3;
        }
    }
}
