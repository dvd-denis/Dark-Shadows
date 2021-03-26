using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLevelSelect : MonoBehaviour
{
    public int level;
    private void OnMouseDown()
    {
        SceneManager.LoadScene(level);
    }
}
