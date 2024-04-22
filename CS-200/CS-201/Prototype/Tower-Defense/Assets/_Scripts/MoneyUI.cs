using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] Money money;
    [SerializeField] TMPro.TextMeshProUGUI moneyText;
    private void Update()
    {
        moneyText.text = "Money : $" + money.GetBalance().ToString();
    }
}
