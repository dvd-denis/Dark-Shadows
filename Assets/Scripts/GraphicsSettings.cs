using UnityEngine;
using UnityEngine.UI;
public class GraphicsSettings : MonoBehaviour
{
    public Slider slider;
    public int Qualtyvalue;
    public Text Qualtytext;
    bool show = false;
    private void Start()
    {
        slider.value = PlayerPrefs.GetInt("Qualtyvalue");
        Qualtytext.text = "Качество графики: низкое";
    }

    private void Update()
    {
        Qualtyvalue = (int)slider.value;
        if (Qualtyvalue == 1) Qualtytext.text = "Качество графики: низкое";
        else if (Qualtyvalue == 2) Qualtytext.text = "Качество графики: среднее";
        else if (Qualtyvalue == 3) Qualtytext.text = "Качество графики: высокое";
        else Qualtytext.text = "Качество графики: очень высокое";
    }

    public void Apply()
    {
        PlayerPrefs.SetInt("Qualtyvalue", Qualtyvalue);
        ShowAndHide();
        switch (Qualtyvalue)
        {
            case 1: QualitySettings.SetQualityLevel(0); break;  
            case 2: QualitySettings.SetQualityLevel(1); break;
            case 3: QualitySettings.SetQualityLevel(2); break;
            case 4: QualitySettings.SetQualityLevel(3); break;
        }    
    }
    public void ShowAndHide()
    {
        if (show) { gameObject.SetActive(false); show = false; }
        else {gameObject.SetActive(true); show = true; }
    }
}
