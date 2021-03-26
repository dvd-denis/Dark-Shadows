using UnityEngine;

public class Exit : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.HasKey("Money"))
        {
            PlayerController.money = PlayerPrefs.GetInt("Money");
        }
    }
    public void exit()
    {
        PlayerPrefs.SetInt("Money", PlayerController.money);
        Application.Quit();
    }
}
