using UnityEngine;

public class RotateEnamy: MonoBehaviour
{
    private Transform Target;
    public float speedRotateX = 5;
    public float speedRotateY = 5;
    void Start()
    {
        Target = GetComponent<Transform>();
    }

    [System.Obsolete]
    void Update() 
    {
        if (!Input.GetMouseButton(0))
            return;

        float rotX = Input.GetAxis("Mouse X") * speedRotateX * Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * speedRotateY * Mathf.Deg2Rad;

        if (Mathf.Abs(rotX) > Mathf.Abs(rotY))
            Target.RotateAroundLocal(Target.up, -rotX);
        else
        {
            var prev = Target.rotation;
            Target.RotateAround(Camera.main.transform.right, rotY);
            if (Vector3.Dot(Target.up, Camera.main.transform.up) < 0.5f)
                Target.rotation = prev;
        }
    }
}
