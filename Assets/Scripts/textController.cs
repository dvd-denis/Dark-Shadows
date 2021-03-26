using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textController : MonoBehaviour
{
    public Text textRecordMoney;
    public Text textRecordLevels;
    void Start()
    {
        SaveController.Load();
        textRecordLevels.text = "RECORDS LEVELS: " + SaveController.recordLevel.ToString();
        textRecordMoney.text = "RECORDS MONEY: " + SaveController.recordMoney.ToString();
    }

}
