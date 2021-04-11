using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public static float hp = 10;
    public static int money;
    public static int healcount = 3;
    public static int silver;
    public static int level = 1;
    public static int healmaxCount = 3;

    public float attackwait = 1.3f;

    public GameObject[] weapons = new GameObject[2];
    public static int weaponActive = 5;

    private Vector3 moveVector;
    private float speedMove = 100;

    private bool equm = false;
    private bool attack;

    private GameObject Eq;
    private FloatingJoystick fl;
    private CharacterController ch;

    private Animator anim;

    private Text moneyText;
    private Text healText;
    private Text finishText;
    private Text silverText;
    private Image UIHp;

    private void Start()
    {
        SaveController.LoadStats();
        ch = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        try
        {
            fl = GameObject.Find("Floating Joystick").GetComponent<FloatingJoystick>();
            UIHp = GameObject.Find("ImageHp").GetComponent<Image>();
            Eq = GameObject.Find("Eq");
            Eq.gameObject.SetActive(false);
            moneyText = GameObject.Find("MoneyText").GetComponent<Text>();
            healText = GameObject.Find("HealText").GetComponent<Text>();
            finishText = GameObject.Find("FinishText").GetComponent<Text>();
            silverText = GameObject.Find("silverText").GetComponent<Text>();

        }  catch (Exception) { }
    }

    private void FixedUpdate()
    {
        moneyText.text = money.ToString() + "$";
        healText.text = healcount.ToString() + " / " + healmaxCount.ToString();
        finishText.text = "Level: " + level .ToString();
        silverText.text = "Silver: " + silver .ToString();
        UIHp.fillAmount = hp / 10;

        weapons[weaponActive].SetActive(true);

        for (int i = 0; i < weapons.Length; i++)
            if (i != weaponActive)
                weapons[i].SetActive(false);

        if (hp <= 0)
        {
            //Vector3 vector = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
            Destroy(gameObject);
            hp = 10;
            SaveController.SaveSilver();
            SaveController.Save();
            ShowMenu show = GetComponent<ShowMenu>();
            show.SetDead();
        }
        else if (hp>10)
            hp = 20;
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnAttack();
        }
        if (!attack)
            CharacterMove();
        else anim.SetBool("Move", false);
    }
    public void CharacterMove()
    {
        if (moveVector.x != 0 || moveVector.z != 0) anim.SetBool("Move", true);
        else anim.SetBool("Move", false);
        moveVector = Vector3.zero;
        if (fl.Horizontal!=0&&fl.Vertical!=0)
        {
            moveVector.x = fl.Horizontal * speedMove * Time.fixedDeltaTime;
            moveVector.z = fl.Vertical * speedMove * Time.fixedDeltaTime;
        }
        else
        {
            moveVector.x = Input.GetAxis("Horizontal") * speedMove * Time.fixedDeltaTime;
            moveVector.z = Input.GetAxis("Vertical") * speedMove * Time.fixedDeltaTime;
        }

        if (Vector3.Angle(Vector3.forward,moveVector)>1f || Vector3.Angle(Vector3.forward, moveVector) == 0f)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedMove, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }
        
        //moveVector.y = gravityForce;

        //ch.Move(moveVector * Time.deltaTime);
    }

    public void OnAttack()
    {
        if (!attack)
        {
            if (weapons[weaponActive].GetComponent<Item>().type == Item.Type.Sword)
                StartCoroutine(AttackSword());
            if (weapons[weaponActive].GetComponent<Item>().type == Item.Type.Staff)
                StartCoroutine(AttackStaff());
        }
    }

    public void onRoll()
    {
        anim.SetTrigger("roll");
    }

    public IEnumerator AttackSword()
    {
        attack = true;
        anim.SetTrigger("attack");
        yield return new WaitForSecondsRealtime(attackwait);
        attack = false;
    }
    public IEnumerator AttackStaff()
    {
        attack = true;
        anim.SetTrigger("attack");
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        float temp = 9999;
        GameObject enemy = null;
        if (enemys.Length != 0)
        {
            foreach (GameObject go in enemys)
            {
                float temp2 = Vector3.Distance(transform.position, go.transform.position);
                if (temp2 < temp)
                {
                    temp = temp2;
                    enemy = go;
                }
            }

            Vector3 newDir = Vector3.RotateTowards(transform.forward, (enemy.transform.position - transform.position), 5, 0.0F);

            transform.rotation = Quaternion.LookRotation(newDir);


        }
        yield return new WaitForSecondsRealtime(0.2f);
        GameObject BULLET = Instantiate(weapons[weaponActive].GetComponent<Item>().bullet, weapons[weaponActive].transform.position, transform.rotation);
        BULLET.GetComponent<Rigidbody>().velocity = transform.forward * 10;

        yield return new WaitForSecondsRealtime(attackwait);
        attack = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Chest")
        {
            Eq.gameObject.SetActive(true);
            if (equm)
            {
                Item currentItem = other.GetComponent<ChestController>().currentobject.GetComponent<Item>();
                ChestController controller = other.GetComponent<ChestController>();
                if (controller.type == ChestController.Type.Shop & controller.sold == false)
                {
                    if (!(money >= controller.cost)) return;
                    money -= controller.cost;
                    controller.text.text = null;
                    controller.sold = true;
                }
                if (currentItem.type == Item.Type.Sword)
                {
                    other.GetComponent<ChestController>().currentobject = Instantiate(weapons[weaponActive], other.transform.TransformPoint(0, 2.4f, 0), other.transform.rotation, other.transform);
                    other.GetComponent<ChestController>().currentobject.SetActive(true);
                    weaponSelect(currentItem.id);
                }
                else if (currentItem.type == Item.Type.Staff)
                {
                    other.GetComponent<ChestController>().currentobject = Instantiate(weapons[weaponActive], other.transform.TransformPoint(0, 2.4f, 0), other.transform.rotation, other.transform);
                    other.GetComponent<ChestController>().currentobject.SetActive(true);
                    weaponSelect(currentItem.id);
                }
                else if (currentItem.type == Item.Type.Heal)
                {
                    if (healcount == healmaxCount) return;
                    healcount += 1;
                }
                else if (currentItem.type == Item.Type.Silver)
                {
                    silver += 1;
                    SaveController.SaveSilver();
                }
                    
                Destroy(other.GetComponentInChildren<Item>().gameObject);
                equm = false;
                Eq.gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Chest")
        {
            Eq.gameObject.SetActive(false);
        }
    }
    public void weaponSelect(int id)
    {
        for (int i = 0; i < weapons.Length; i++)
            if (weapons[i].GetComponent<Item>().id == id)
                weaponActive = i;
    }
    public void Damage(float damage)
    {
        hp -= damage;
    }
    public void OnEq()
    {
        equm = true;
    }
    public void Heal()
    {
        if (healcount == 0) return;
        if (hp == 10) return;
        healcount -= 1;
        hp += 3;
    }
}
