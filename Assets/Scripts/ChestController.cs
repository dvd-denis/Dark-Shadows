using UnityEngine;

public class ChestController : MonoBehaviour
{
    public GameObject[] objects;

    [HideInInspector]
    public GameObject currentobject;

    public TextMesh text;
    public int cost;
    public bool sold = false;

    public enum Type
    {
        Chest = 0,
        Shop = 1
    }

    public Type type;

    void Start()
    {
        currentobject = Instantiate(objects[Random.Range(0, objects.Length)], transform.TransformPoint(0, 2.4f, 0), transform.rotation, transform);
        if (type == Type.Chest)
            text.gameObject.SetActive(false);
        else
        {
            cost = Random.Range(2, 15);
            text.text = cost.ToString();
        }
    }
    void Update()
    {
        if (currentobject== null)
            GetComponent<BoxCollider>().enabled = false;
        else
            GetComponent<BoxCollider>().enabled = true;
    }
}
