using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public bool onShop = false;
    public GameObject shop;

    public Text textSilver;

    public int price_healmaxCount = 5;


    public Text stats_text_healmax;
    //public Text price_text_healmax;

    public void Start()
    {
        SaveController.Load();
        textSilver.text = "SILVER: " + PlayerController.silver;
    }

    public void Update()
    {
        stats_text_healmax.text = "Heal Max: " + PlayerController.healmaxCount;
    }

    public void onClickHealMaxCount()
    {
        if (PlayerController.silver >= price_healmaxCount)
        {
            PlayerController.silver -= price_healmaxCount;
            PlayerController.healmaxCount += 1;
            textSilver.text = "SILVER: " + PlayerController.silver;
        }
    }

    public void onClickON()
    {
        shop.SetActive(true);
        SaveController.LoadStats();
    }
    public void onClickOff()
    {
        shop.SetActive(false);
        SaveController.SaveStats();
    }
}
