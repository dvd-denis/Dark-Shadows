using UnityEngine;

public class SaveController : MonoBehaviour
{
    public static int recordLevel;
    public static int recordMoney;

    public static void Save()
    {
        Load();
        if (PlayerController.level > recordLevel)
            PlayerPrefs.SetInt("recordLevel", PlayerController.level);
        if (PlayerController.money > recordMoney)
            PlayerPrefs.SetInt("recordMoney", PlayerController.money);
        PlayerPrefs.Save();
        Load();
    }
    public static void SaveSilver()
    {
        PlayerPrefs.SetInt("silver", PlayerController.silver);
        PlayerPrefs.Save();
        Load();
    }

    public static void SaveStats()
    {
        PlayerPrefs.SetInt("healmaxCount", PlayerController.healmaxCount);
        PlayerPrefs.Save();
    }

    public static void LoadStats()
    {
        PlayerController.healmaxCount = PlayerPrefs.GetInt("healmaxCount");
        if (PlayerController.healmaxCount < 3)
            PlayerController.healmaxCount = 3;
    }

    public static void Load()
    {
        recordLevel = PlayerPrefs.GetInt("recordLevel");
        recordMoney = PlayerPrefs.GetInt("recordMoney");
        PlayerController.silver = PlayerPrefs.GetInt("silver");
    }
}
