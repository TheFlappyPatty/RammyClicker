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















    public GameObject level;
    [Header("Victim spawning")]
    public int SpawnWaittime;
    public GameObject Victim;
    public int VictumCount;
    public bool Spawning;
    public void Start()
    {
        StartCoroutine(BusspawnCounter());
    }
    public void Update()
    {
        if(Spawning == false)
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
    public int Customerspawnfrequency = 250;
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
            Busfrequency = Busfrequency * 2;
            BusFreqButton.text = "Bus Frequency\n Cost:" + Busfrequency;
        }
    }
    public void Buslimit(int Bouns)
    {
        if (ActiveBuss <= 40 && BloodMoney > BusAmount)
        {
            ActiveBuss += Bouns;
            RemoveValue(BusAmount);
            BusAmount = BusAmount * 2;
            BusAmountButton.text = "Bus Amount\n Cost:" + BusAmount;
        }
    }
    public void CustomerAmount(int Bouns)
    {
        if (VictumCount <= 20 && BloodMoney >= CustomerspawnAmount)
        {
            VictumCount += Bouns;
            RemoveValue(CustomerspawnAmount);
            CustomerspawnAmount = CustomerspawnAmount * 2;
            CustomerAmButton.text = "Victum amount\n Cost:" + CustomerspawnAmount;
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
    public int ActiveBuss;
    public float BusWaitTime;
    IEnumerator BusspawnCounter()
    {
        while (true)
        {
            if(ActiveBuss == 0)
            {

            }
            else
            {
                var spawned = 0;
                yield return new WaitForSeconds(BusWaitTime);
                while (ActiveBuss > spawned)
                {
                    Busspawn();
                    spawned++;
                }
                if (ActiveBuss == spawned)
                {
                    spawned = 0;
                }
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
