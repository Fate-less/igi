using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Currency : MonoBehaviour
{
    public float money;
    public TextMeshProUGUI moneyTMP;

    // Update is called once per frame
    void Update()
    {
        moneyTMP.text = "Currency: " + money.ToString();
    }
}
