using UnityEngine;

public class Item : MonoBehaviour
{
	public int id;
	public enum Type
	{
		Sword = 0,
		Heal = 1,
		Silver = 2,
		Staff = 3
	}
	public Type type;

	public GameObject bullet;
}
