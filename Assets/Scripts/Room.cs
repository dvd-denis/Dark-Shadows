using UnityEngine;
using UnityEngine.SceneManagement;

public class Room : MonoBehaviour
{   
    public GameObject doorUp;
    private bool doorUptemp = true;

    public GameObject doorDown;
    private bool doorDowntemp = true;

    public GameObject doorLeft;
    private bool doorLefttemp = true;

    public GameObject doorRight;
    private bool doorRighttemp = true;
    public enum Type
    {
        DefaultRoom = 0,
        EnemyRoom = 1,
        FinishRoom = 2,
        ShopRoom = 3
    }

    public Type type;

    private int Stage;
    private int StageCount;
    private bool onWin;

    public static int countEnemyinRoom;
    //public static int countEnemyRoom;

    public void Start()
    {
        if (type==Type.EnemyRoom)
            StageCount = Random.Range(1, 3);
        if (type!=Type.ShopRoom)
        {
            doorDowntemp = doorDown.activeSelf;
            doorLefttemp = doorLeft.activeSelf;
            doorRighttemp = doorRight.activeSelf;
            doorUptemp = doorUp.activeSelf;

        }
    }
    public void Update()
    {
/*        // Room Final
        if (type == Type.FinishRoom)
        {
            if (Room.countEnemyRoom == 0)
            {
                doorDown.SetActive(doorDowntemp);
                doorLeft.SetActive(doorLefttemp);
                doorRight.SetActive(doorRighttemp);
                doorUp.SetActive(doorUptemp);
            }
            else
            {
                doorDown.SetActive(true);
                doorLeft.SetActive(true);
                doorRight.SetActive(true);
                doorUp.SetActive(true);
            }
        }*/
        // Romm Enemy
        if (type == Type.EnemyRoom & !onWin)
        {
            if (Stage >= 1)
            {
                doorDown.SetActive(true);
                doorLeft.SetActive(true);
                doorRight.SetActive(true);
                doorUp.SetActive(true);
                if (Stage == StageCount & countEnemyinRoom == 0)
                {
                    doorDown.SetActive(doorDowntemp);
                    doorLeft.SetActive(doorLefttemp);
                    doorRight.SetActive(doorRighttemp);
                    doorUp.SetActive(doorUptemp);
                    onWin = true;
                }
                else if (countEnemyinRoom == 0 & Stage != StageCount & Stage != 0)
                {
                    Stage++;
                    Spawner[] mass = GetComponentsInChildren<Spawner>();
                    for (int i = 0; i < mass.Length; i++)
                        mass[i].Summon();
                }
            }
        } 
    }

    public void RotateRandomly()
    {
        int count = Random.Range(0, 4);

        for (int i = 0; i < count; i++)
        {
            transform.Rotate(0, 90, 0);

            GameObject tmp = doorLeft;
            doorLeft = doorDown;
            doorDown = doorRight;
            doorRight = doorUp;
            doorUp = tmp;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (type == Type.EnemyRoom & Stage==0)
            {
                Stage++;
                Spawner[] mass = GetComponentsInChildren<Spawner>();
                for (int i = 0; i < mass.Length; i++)
                    mass[i].Summon();
            }
            if (type == Type.FinishRoom)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                RoomPlacer.RoomCount+=Random.Range(1,4);
                PlayerController.money += 25;
                PlayerController.level += 1;
            }
        }
    }
}
