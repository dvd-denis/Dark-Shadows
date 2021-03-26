using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
	private GameObject player;

	public float hp = 100;
	public enum Type
	{
		enemy1 = 0,
		enemy2 = 1,
		enemy3 = 2
	}
	public GameObject bullet;

	public Type type;

	public float speed = 3.5f;

	private NavMeshAgent nav;
	private bool onWait = false;
	private float speedBullet;

	private float dist;
	public void Start()
	{
		nav = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player");

		//Random
		speed = Random.Range(3, 3.5f);
		speedBullet = Random.Range(6f, 12f);
    }
	public void Update()
	{
		nav.speed = speed;
		if (hp <= 0)
        {
			Destroy(gameObject);
			Room.countEnemyinRoom--;
		}
		if (player != null)
		{
			dist = Vector3.Distance(player.transform.position, transform.position);
			Vector3 direction = (player.transform.position - transform.position).normalized;
			Quaternion lookrotation = Quaternion.LookRotation(direction);
			transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, Time.deltaTime * 10f);
			if (type == Type.enemy1)
			{
				nav.stoppingDistance = 5f;
				if (dist <= 5)
					Attack(true);
				nav.SetDestination(player.transform.position);
			}
		}
	}
	void Attack(bool ranged_attack)
	{
		if (ranged_attack)
		{
			if (onWait == false)
			{
				StartCoroutine(Fire());
			}
		}
	}
	public IEnumerator Fire()
	{
		onWait = true;
		GameObject BULLET = Instantiate(bullet, transform.position, transform.rotation);
		BULLET.GetComponent<Rigidbody>().velocity = transform.forward * speedBullet;
		yield return new WaitForSeconds(2);
		onWait = false;
	}
}
