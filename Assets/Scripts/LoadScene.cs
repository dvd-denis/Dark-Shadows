using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public int Scene = 0;
    public void Loadscene()
    {
        SceneManager.LoadScene(Scene);
        SaveController.SaveSilver();
        Time.timeScale = 1;
    }
}
