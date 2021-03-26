using System.Collections;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public Animator animPlayer;
    public GameObject blood;
    public int damage = 20;
    public bool freaze;

    private void OnTriggerEnter(Collider other)
    {
        if (!animPlayer.GetCurrentAnimatorStateInfo(0).IsName("Attack1")) return;

        if (other.tag == "Enemy")
        {
            Vector3 vector = new Vector3(other.transform.position.x, other.transform.position.y + 1, other.transform.position.z);
            Instantiate(blood, vector, other.transform.rotation);
            other.GetComponent<EnemyController>().hp -= damage;
            if (freaze) StartCoroutine(Freaze(other));
            PlayerController.money += Random.Range(0, 3);
        }
    }
    IEnumerator Freaze(Collider frag)
    {
        frag.GetComponent<EnemyController>().speed = 1f;
        yield return new WaitForSeconds(2);
        frag.GetComponent<EnemyController>().speed = 3.5f;
    }
}
