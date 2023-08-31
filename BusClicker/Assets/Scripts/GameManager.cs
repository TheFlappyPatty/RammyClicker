using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float BloodMoney;
    public int Prestige;
    public GameObject Victim;
    public GameObject level;
    public int BusCount;
    public bool Spawning;

    public void Update()
    {
        if(Spawning == false)
        {
            SpawnVictim(BusCount);
        }
    }




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
        Spawning = true;
        int Spawned = 0;
        while(true)
        {
            Instantiate(Victim, SpawnLocation(level), Quaternion.identity, null);
            if (Spawned == Amount)
            {
                break;

            }
    }
        Spawning = false;
    }
    Vector3 SpawnLocation(GameObject Map)
    {

     var location = new Vector3( Random.Range(-8f,8f), Random.Range(-3.7f,3.2f), 0);
      return location;
    }
}
