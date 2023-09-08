using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static float BloodMoney;
    public int Prestige;



    [Header("Text")]
    public TextMeshProUGUI BloodmoneyText;
    public BusScript BusScript;















    public GameObject level;
    [Header("Victim spawning")]
    public float SpawnWaittime;
    public GameObject Victim;
    public int VictumCount;
    public bool Spawning;
    public bool BusSpawning = true;
    public void Start()
    {
        BusSpawning = true;
    }
    public void Update()
    {
        if(BusSpawning == true)
        {
        StartCoroutine(BusspawnCounter());
        }

        if (Spawning == false)
        {
            StartCoroutine(SpawnVictum(VictumCount));
        }
        BloodmoneyText.text = "BloodMoney = " + BloodMoney;

    }




    public static void AddValue(int Add, int Multiplier)
    {
        BloodMoney += Add * (1 + (0.01f * Multiplier));
    }
  public static void RemoveValue(int Remove)
    {
        BloodMoney -= Remove;
    }




    [Header("Upgrades")]
    public int Busfrequency = 500;
    public TextMeshProUGUI BusFreqButton;
    public int Bussize = 2000;
    public TextMeshProUGUI BussizeButtom;
    public int BusAmount = 1;
    public TextMeshProUGUI BusAmountButton;
    public int busSpeed = 100;
    public TextMeshProUGUI BusSpeedButton;
    public int Customerspawnfrequency = 150;
    public TextMeshProUGUI CustomerfreqButtom;
    public int CustomerspawnAmount = 100;
    public TextMeshProUGUI CustomerAmButton;
    public int CustomerReward = 200;
    public TextMeshProUGUI CustReward;
    public int PrestigeReward = 50000;
    public TextMeshProUGUI PrestigeRewardButton;

    public void BusFrequency(float Bouns)
    {
        if (BusWaitTime >= 0.5f && BloodMoney >= Busfrequency)
        {
            BusWaitTime -= Bouns;
            RemoveValue(Busfrequency);
            var rounded = Busfrequency * 1.5f;
            Busfrequency = Mathf.RoundToInt(rounded);
            BusFreqButton.text = "Bus Frequency\n Cost:" + Busfrequency;
        }
    }
    public void Buslimit(int Bouns)
    {
        if (ActiveBuses < 8 && BloodMoney >= BusAmount)
        {
                ActiveBuses += Bouns;
                RemoveValue(BusAmount);
            var rounded = BusAmount * 1.5f;
            BusAmount = Mathf.RoundToInt(rounded);
            BusAmountButton.text = "Bus Amount\n Cost:" + BusAmount;
        }
    }
    public void CustomerAmount(int Bouns)
    {
        if (VictumCount <= 60 && BloodMoney >= CustomerspawnAmount)
        {
            VictumCount += Bouns;
            RemoveValue(CustomerspawnAmount);
            var rounded = CustomerspawnAmount * 1.5f;
            CustomerspawnAmount = Mathf.RoundToInt(rounded);
            CustomerAmButton.text = "Victum Amount\n Cost:" + CustomerspawnAmount;
        }
    }
    public void VictumFrequency(float Bouns)
    {
        if (SpawnWaittime > 0.2f && BloodMoney >= Customerspawnfrequency)
        {
            SpawnWaittime -= Bouns;
            RemoveValue(Customerspawnfrequency);
            var rounded = Customerspawnfrequency * 1.5f;
            Customerspawnfrequency = Mathf.RoundToInt(rounded);
            CustomerfreqButtom.text = "Victum Frequency\n Cost:" + Customerspawnfrequency;
        }
    }
    public void Victimvalues(int Bouns)
    {
        if (BusScript.VictimValue < 10 && BloodMoney >= CustomerReward)
        {
            BusScript.VictimValue += Bouns;
            RemoveValue(CustomerReward);
            var rounded = CustomerReward * 1.5f;
            CustomerReward = Mathf.RoundToInt(rounded);
            CustReward.text = "Victum Value\n Cost:" + CustomerReward;
        }
    }

















    public  void SpawnVictim(int Amount)
    {
            Instantiate(Victim, SpawnLocation(level), Quaternion.identity, null);
    }


    //Auto Spawning
    IEnumerator SpawnVictum(int Amount)
    {
        Spawning = true;
        int Spawned = 0;
        
        while (Spawning == true)
        {
            if(Spawned < Amount)
            {
                Instantiate(Victim, SpawnLocation(level), Quaternion.identity, null);
                Spawned++;
            }
            else
            {
                break;
            }
        }
        yield return new WaitForSeconds(SpawnWaittime);
        Spawning = false;
    }

    Vector3 SpawnLocation(GameObject Map)
    {
        var location = Map.transform.position + new Vector3(Random.Range(-Map.transform.localScale.x / 4, Map.transform.localScale.x / 4), Random.Range(-Map.transform.localScale.y / 4, Map.transform.localScale.y / 4), 0);
        return location;
    }




    //Bus Spawning
    [Header("Bus Spawn System")]
    public GameObject[] Busspawns;
    public GameObject Busprefab;
    public int ActiveBuses;
    public float BusWaitTime;
    IEnumerator BusspawnCounter()
    {
        BusSpawning = false;
        Debug.Log("Spawning");
        if (ActiveBuses == 0)
            {

            }
            else
            {
                var spawned = 0;
                yield return new WaitForSeconds(BusWaitTime);
                while (ActiveBuses > spawned)
                {
                    Busspawn();
                Debug.Log("Spawned");
                spawned++;
                }
                if (ActiveBuses == spawned)
                {
                    spawned = 0;
                BusSpawning = true;
                }
            }
    }

    void Busspawn()
    {
        var randomnumber = Random.Range(0, Busspawns.Length);
        var spawnlocation = Busspawns[randomnumber];
        Instantiate(Busprefab, spawnlocation.transform.position,spawnlocation.transform.rotation,null);
    }
}
