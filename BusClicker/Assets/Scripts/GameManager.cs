using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    public static float BloodMoney;
    public int Prestige;
    public static float CurrentPrestigeBonus;
    public int PrestigeBonusReward;



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
        BusScript.transform.localScale = new Vector3(0.5f, 1, 0.5f);
        
    }
    public IEnumerator ClearArray(GameObject[] Select)
    {
        var Current = 0;
        while (Current <= Select.Length)
        {
            Select.SetValue(null,Current);
            Current++;
        }
        return null;
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
        PrestigeText.text = UpgradeCount + "/100";
    }

    public int UpgradeCount;
    public TextMeshProUGUI PrestigeText;
    public void PrestigeIsAGo()
    {
        if(UpgradeCount >= 100)
        {
        Prestige++;
        CurrentPrestigeBonus += 1 + PrestigeBonusReward;
        ResetStats();
        BusScript.ResetStats();
        }

    }

    public static void AddValue(float Add, float Multiplier)
    {
        BloodMoney += Add * (1 + (0.01f * Multiplier));
    }
  public static void RemoveValue(int Remove)
    {
        BloodMoney -= Remove;
    }
    public void ResetStats()
    {
        BloodMoney = 0;
        VictumCount = 0;
        SpawnWaittime = 10;
        ActiveBuses = 1;
        BusWaitTime = 5;
        UpgradeCount = 0;
        SpawnPerClick = 0;






        VictimPerClick = 60;
        ButtonVictimperclicktext.text = "Victim per click\n Cost:" + VictimPerClick;
        Busfrequency = 50;
        BusFreqButton.text = "Bus Frequency\n Cost:" + Busfrequency;
        Bussize = 2000;
        BussizeButtom.text = "Bus Size\n Cost:" + Bussize;
        BusAmount = 100;
        BusAmountButton.text = "Bus Amount\n Cost:" + BusAmount;
        busSpeed = 40;
        Customerspawnfrequency = 150;
        CustomerfreqButtom.text = "Victum Frequency\n Cost:" + Customerspawnfrequency;
        CustomerspawnAmount = 100;
        CustomerAmButton.text = "Victum Amount\n Cost:" + CustomerspawnAmount;
        CustomerReward = 200;
        CustReward.text = "Victum Value\n Cost:" + CustomerReward;
        PrestigeReward = 20000;
        PrestigeRewardButton.text = "Prestige Bonuse\n Cost:" + PrestigeReward;
    }




    [Header("Upgrades")]
    public int VictimPerClick = 60;
    public int SpawnPerClick = 0;
    public Button ButtonVictimperclick;
    public TextMeshProUGUI ButtonVictimperclicktext;
    public int Busfrequency = 50;
    public TextMeshProUGUI BusFreqButton;
    public int Bussize = 2000;
    public TextMeshProUGUI BussizeButtom;
    public int BusAmount = 100;
    public TextMeshProUGUI BusAmountButton;
    public int busSpeed = 40;
    public TextMeshProUGUI BusSpeedButton;
    public int Customerspawnfrequency = 150;
    public TextMeshProUGUI CustomerfreqButtom;
    public int CustomerspawnAmount = 100;
    public TextMeshProUGUI CustomerAmButton;
    public int CustomerReward = 200;
    public TextMeshProUGUI CustReward;
    public int PrestigeReward = 20000;
    public TextMeshProUGUI PrestigeRewardButton;

    public void BusFrequency(float Bouns)
    {
        if (BusWaitTime >= 0.5f && BloodMoney >= Busfrequency)
        {
            BusWaitTime -= Bouns;
            RemoveValue(Busfrequency);
            var rounded = Busfrequency * 1.1f;
            Busfrequency = Mathf.RoundToInt(rounded);
            BusFreqButton.text = "Bus Frequency\n Cost:" + Busfrequency;
            UpgradeCount++;
        }
    }
    public void Buslimit(int Bouns)
    {
        if (ActiveBuses < 12 && BloodMoney >= BusAmount)
        {
                ActiveBuses += Bouns;
                RemoveValue(BusAmount);
            var rounded = BusAmount * 1.2f;
            BusAmount = Mathf.RoundToInt(rounded);
            BusAmountButton.text = "Bus Amount\n Cost:" + BusAmount;
            UpgradeCount++;
        }
    }
    public void CustomerAmount(int Bouns)
    {
        if (VictumCount <= 100 && BloodMoney >= CustomerspawnAmount)
        {
            VictumCount += Bouns;
            RemoveValue(CustomerspawnAmount);
            var rounded = CustomerspawnAmount * 1.2f;
            CustomerspawnAmount = Mathf.RoundToInt(rounded);
            CustomerAmButton.text = "Victum Amount\n Cost:" + CustomerspawnAmount;
            UpgradeCount++;
        }
    }
    public void VictumFrequency(float Bouns)
    {
        if (SpawnWaittime > 1 && BloodMoney >= Customerspawnfrequency)
        {
            SpawnWaittime -= Bouns;
            RemoveValue(Customerspawnfrequency);
            var rounded = Customerspawnfrequency * 1.5f;
            Customerspawnfrequency = Mathf.RoundToInt(rounded);
            CustomerfreqButtom.text = "Victum Frequency\n Cost:" + Customerspawnfrequency;
            UpgradeCount++;
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
            UpgradeCount++;
        }
    }
    public void BusSize(float Bouns)
    {
        if (BusScript.transform.localScale.z < 1 && BloodMoney >= Bussize)
        {
            BusScript.transform.localScale += new Vector3(Bouns,Bouns,0);
            RemoveValue(Bussize);
            var rounded = Bussize * 1.75f;
            Bussize = Mathf.RoundToInt(rounded);
            BussizeButtom.text = "Bus Size\n Cost:" + Bussize;
            UpgradeCount++;
        }
    }
    public void PrestigeBonus(int Bouns)
    {
        if (BloodMoney >= PrestigeReward)
        {
            PrestigeBonusReward += Bouns;
            RemoveValue(PrestigeReward);
            var rounded = PrestigeReward * 1.8f;
            PrestigeReward = Mathf.RoundToInt(rounded);
            PrestigeRewardButton.text = "Prestige Bonus\n Cost:" + PrestigeReward;
            UpgradeCount++;
        }
    }














    public  void Spawncount(int Bouns)
    {

        if (BloodMoney >= VictimPerClick)
        {
            SpawnPerClick += Bouns;
            
            RemoveValue(VictimPerClick);
            var rounded = VictimPerClick * 2f;
            VictimPerClick = Mathf.RoundToInt(rounded);
            ButtonVictimperclicktext.text = "Victim per click\n Cost:" + VictimPerClick;
            UpgradeCount++;
        }
    }

   public void Spawn()
    {
        var Spawned = 0;
        while(SpawnPerClick >= Spawned)
        {
            Instantiate(Victim, SpawnLocation(level), Quaternion.identity, null);
            Spawned++;
        }
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

                while (ActiveBuses > spawned)
                {
                    Busspawn();
                yield return new WaitForSeconds(0.1f);
                spawned++;
                }
            if (spawned > ActiveBuses) spawned = 0;
                if (ActiveBuses == spawned)
                {
                    spawned = 0;

                yield return new WaitForSeconds(BusWaitTime);
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
