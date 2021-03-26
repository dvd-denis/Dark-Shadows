using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	public enum Type
	{
		Defaultbullet = 0,
	}

	public float damage = 0.5f;

	public Type type;
    private void Start()
    {
		damage = Random.Range(0.3f, 0.6f);
    }

    private void OnTriggerEnter(Collider other)
    {
		if (other.CompareTag("Player"))
        {
			if (type == Type.Defaultbullet)
            {
				Destroy(gameObject);
				other.GetComponent<PlayerController>().Damage(damage);
            }
        }
		Destroy(gameObject, 5f);
	}
}
