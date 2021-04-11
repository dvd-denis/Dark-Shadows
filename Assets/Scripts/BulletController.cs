using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	public enum Type
	{
		Defaultbullet = 0,
		PlayerBullet = 1
	}

	public float damage = 0.5f;

	public Type type;
    private void Start()
    {
		if (type == Type.Defaultbullet)
			damage = Random.Range(0.3f, 0.6f);
    }

    private void OnTriggerEnter(Collider other)
    {
		if (type == Type.Defaultbullet)
        {
			if (other.CompareTag("Player"))
			{
				Destroy(gameObject);
				other.GetComponent<PlayerController>().Damage(damage);
			}
		}
		else if (type == Type.PlayerBullet)
        {
			if (other.CompareTag("Enemy"))
			{
				Destroy(gameObject);
				other.GetComponent<EnemyController>().hp -= damage;
			}
		}
		Destroy(gameObject, 5f);
	}
}
