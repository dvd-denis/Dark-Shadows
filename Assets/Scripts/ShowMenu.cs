using UnityEngine;

public class ShowMenu : MonoBehaviour
{
    public GameObject Game;
    public GameObject Menu;
    public void SetVisible()
    {
        Game.SetActive(false);
        Menu.SetActive(true);
        Time.timeScale = 0;
    }
    public void SetInvisible()
    {
        Game.SetActive(true);
        Menu.SetActive(false);
        Time.timeScale = 1;
    }
    public void SetDead()
    {
        Time.timeScale = 1;
        Game.SetActive(false);
        Menu.SetActive(true);
        GameObject.FindWithTag("return").SetActive(false);
    }
}
