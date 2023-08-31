using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float BloodMoney;
    public int Prestige;

    void AddValue(int Add, int Multiplier)
    {
        BloodMoney += Add * (1 + (0.01f * Multiplier));
    }
    void RemoveValue(int Remove)
    {
        BloodMoney += Remove;
    }
    void SpawnVictim(int Amount)
    {
        var Spawned = new int 0
        while(Spawned |= Amount)
        {
          
        }
    }
}
