using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] int balance;
    [SerializeField] int startingBalance;
    public int GetBalance()
    {
        return balance;
    }

    private void Awake()
    {
        balance = startingBalance;
    }
    
    public void ChangeBalance(int change)
    {
        balance += change;
    }
}
