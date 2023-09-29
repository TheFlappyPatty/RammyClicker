using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static float BloodMoney;
    public static float CurrentPrestigeBonus;

    [Header("Text")]
    public TextMeshProUGUI BloodmoneyText;
    public BusScript BusScript;
    public TextMeshProUGUI PrestigeText;
    public GameObject level;

    [Header("Victim spawning")]
    public float SpawnWaittime;
    public GameObject Victim;

    [Header("Player progression")]
    public int PrestigeBonusReward;
    public int UpgradeCount;
    public int VictumCount;
    public int Prestige;

    [Header("Spawn Control Systems")]
    public bool Spawning;
    public bool BusSpawning = true;



    [Header("LootBoxUI")]
    public TextMeshProUGUI LootBoxText;
    public Image LootBoxImage;




    public void Start()
    {
        BusSpawning = true;
        BusScript.transform.localScale = new Vector3(0.5f, 1, 0.5f);
        
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
        BloodmoneyText.text = "BloodMoney = " + BloodMoney.ToString("###,###,###.##");
        PrestigeText.text = UpgradeCount + "/100";
        if(BusWaitTime < 0.01)
        {
            BusWaitTime = 0.1f;
        }
        if (SpawnWaittime < 0.4)SpawnWaittime = 0.4f;
        
        if (Input.GetKey(KeyCode.V) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.R)) cheats = true;
        if(cheats == true && Input.GetKey(KeyCode.M))
        {
            if(cheatSpawn == false)
            {
                StartCoroutine(ConstantSpawn());
            }
        }
        if (BusScript.VictimValue < 10 * CustomerRewardMulti)
        {
            CustReward.gameObject.GetComponentInParent<Button>().interactable = true;
        }
    }
    public bool cheats;

    public void PrestigeIsAGo()
    {
        if(UpgradeCount >= 100)
        {
        Prestige++;
        CurrentPrestigeBonus += 1 + PrestigeBonusReward;
        ResetStats();
        BusScript.ResetStats();
            Debug.Log(CurrentPrestigeBonus);
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
        SpawnWaittime = 3;
        ActiveBuses = 1;
        BusWaitTime = 5;
        UpgradeCount = 0;
        SpawnPerClick = 0;
        




        //Upgrade cost and Text
        VictimPerClick = 60;
        ButtonVictimperclicktext.text = "Victim per click\n Cost:" + VictimPerClick;
        Busfreq = 50;
        BusFreqButton.text = "Bus Frequency\n Cost:" + Busfreq;
        BusFreqButton.gameObject.GetComponentInParent<Button>().interactable = true;
        Bussize = 2000;
        BussizeButtom.text = "Bus Size\n Cost:" + Bussize;
        BussizeButtom.gameObject.GetComponentInParent<Button>().interactable = true;
        BusAmt = 100;
        BusAmountButton.text = "Bus Amount\n Cost:" + BusAmt;
        BusAmountButton.gameObject.GetComponentInParent<Button>().interactable = true;
        Custspawnfreq = 150;
        CustomerfreqButtom.text = "Victim Frequency\n Cost:" + Custspawnfreq;
        CustomerfreqButtom.gameObject.GetComponentInParent<Button>().interactable = false;
        CustspawnAmt = 100;
        CustomerAmButton.text = "Victim Amount\n Cost:" + CustspawnAmt;
        CustomerAmButton.gameObject.GetComponentInParent<Button>().interactable = true;
        CustomerReward = 200;
        CustReward.text = "Victim Value\n Cost:" + CustomerReward;
        CustReward.gameObject.GetComponentInParent<Button>().interactable = true;
        PrestigeReward = 20000;
        PrestigeRewardButton.text = "Prestige Bonuse\n Cost:" + PrestigeReward;
        LootBox = 200;
        LootBoxButton.text = "Loot Box\n Cost:" + LootBox;

        //UpgradeMultipliers
         BusFreqMulti = 0.08f;
        BussizeMulti = 0.01f;
        BusAmtMulti = 1;
        CustSpawnFreqMulti = 0.5F;
        CustspawnAmtmulti = 1;
        CustomerRewardMulti = 1;
        PrestigeRewardMulti = 1;
        VictimPerClickMulti = 1;
        
    }




    [Header("Upgrades")]
    public int VictimPerClick = 60;
    public int VictimPerClickMulti = 1;
    public int SpawnPerClick = 0;
    public Button ButtonVictimperclick;
    public TextMeshProUGUI ButtonVictimperclicktext;
    public int Busfreq = 50;
    private float BusFreqMulti = 0.08f;
    public TextMeshProUGUI BusFreqButton;
    public int Bussize = 2000;
    private float BussizeMulti = 0.01f;
    public TextMeshProUGUI BussizeButtom;
    public int BusAmt = 100;
    private int BusAmtMulti = 1;
    public TextMeshProUGUI BusAmountButton;
    public int Custspawnfreq = 150;
    private float CustSpawnFreqMulti = 0.5F;
    public TextMeshProUGUI CustomerfreqButtom;
    public Button CustfreqButton;
    public int CustspawnAmt = 100;
    private int CustspawnAmtmulti = 1;
    public TextMeshProUGUI CustomerAmButton;
    public int CustomerReward = 200;
    private int CustomerRewardMulti = 1;
    public TextMeshProUGUI CustReward;
    public int PrestigeReward = 20000;
    private int PrestigeRewardMulti = 1;
    public TextMeshProUGUI PrestigeRewardButton;
    public int LootBox = 200;
    public TextMeshProUGUI LootBoxButton;

    public void BusFrequency()
    {
        if (BusWaitTime >= 0.5f && BloodMoney >= Busfreq)
        {
            BusWaitTime -= BusFreqMulti;
            RemoveValue(Busfreq);
            var rounded = Busfreq * 1.25f;
            Busfreq = Mathf.RoundToInt(rounded);
            BusFreqButton.text = "Bus Frequency\n Cost:" + Busfreq;
            UpgradeCount++;
        }
         if(BusWaitTime <= 0.5f)
        {
            BusFreqButton.gameObject.GetComponentInParent<Button>().interactable = false;
        }
    }
    public void Buslimit()
    {
        if (ActiveBuses < 12 && BloodMoney >= BusAmt)
        {
                ActiveBuses += BusAmtMulti;
                RemoveValue(BusAmt);
            var rounded = BusAmt * 1.4f;
            BusAmt = Mathf.RoundToInt(rounded);
            BusAmountButton.text = "Bus Amount\n Cost:" + BusAmt;
            UpgradeCount++;
        }
        if (ActiveBuses >= 12)
        {
            BusAmountButton.gameObject.GetComponentInParent<Button>().interactable = false;
        }
    }
    public void CustomerAmount()
    {
        if (VictumCount <= 200 && BloodMoney >= CustspawnAmt)
        {
            if (SpawnWaittime > 0.4f)
            {
                CustfreqButton.interactable = true;
            }
            VictumCount += CustspawnAmtmulti;
            RemoveValue(CustspawnAmt);
            var rounded = CustspawnAmt * 1.1f;
            CustspawnAmt = Mathf.RoundToInt(rounded);
            CustomerAmButton.text = "Victum Amount\n Cost:" + CustspawnAmt;
            UpgradeCount++;
        }
        if (VictumCount >= 200)
        {
            CustomerAmButton.gameObject.GetComponentInParent<Button>().interactable = false;
        }
    }
    public void VictumFrequency()
    {
        if (SpawnWaittime > 0.4f && BloodMoney >= Custspawnfreq)
        {
            SpawnWaittime -= CustSpawnFreqMulti;
            RemoveValue(Custspawnfreq);
            var rounded = Custspawnfreq * 1.8f;
            Custspawnfreq = Mathf.RoundToInt(rounded);
            CustomerfreqButtom.text = "Victum Frequency\n Cost:" + Custspawnfreq;
            UpgradeCount++;
        }
        if (SpawnWaittime <= 0.4f)
        {
            CustomerfreqButtom.gameObject.GetComponentInParent<Button>().interactable = false;
        }
    }
    public void Victimvalues()
    {
        if (BusScript.VictimValue < 10 * CustomerRewardMulti && BloodMoney >= CustomerReward)
        {
            BusScript.VictimValue += CustomerRewardMulti;
            RemoveValue(CustomerReward);
            var rounded = CustomerReward * 1.5f;
            CustomerReward = Mathf.RoundToInt(rounded);
            CustReward.text = "Victum Value\n Cost:" + CustomerReward;
            UpgradeCount++;
        }
        if (BusScript.VictimValue > 10 * CustomerRewardMulti)
        {
            CustReward.gameObject.GetComponentInParent<Button>().interactable = false;
        }
    }
    public void BusSize()
    {
        if (BusScript.transform.localScale.z < 1 && BloodMoney >= Bussize)
        {
            BusScript.transform.localScale += new Vector3(BussizeMulti,BussizeMulti,0);
            RemoveValue(Bussize);
            var rounded = Bussize * 1.75f;
            Bussize = Mathf.RoundToInt(rounded);
            BussizeButtom.text = "Bus Size\n Cost:" + Bussize;
            UpgradeCount++;
        }
         if (BusScript.transform.localScale.z < 1)
        {
            BussizeButtom.gameObject.GetComponentInParent<Button>().interactable = false;
        }
    }
    public void PrestigeBonus()
    {
        if (BloodMoney >= PrestigeReward)
        {
            PrestigeBonusReward += PrestigeRewardMulti;
            RemoveValue(PrestigeReward);
            var rounded = PrestigeReward * 1.8f;
            PrestigeReward = Mathf.RoundToInt(rounded);
            PrestigeRewardButton.text = "Prestige Bonus\n Cost:" + PrestigeReward;
            UpgradeCount++;
        }
    }

    public void OpenLootBox()
    {
        if (BloodMoney >= LootBox)
        {
            StartCoroutine(LootBoxIsOpen());
            RemoveValue(LootBox);
            var rounded = LootBox * 4f;
            LootBox = Mathf.RoundToInt(rounded);
            LootBoxButton.text = "Loot Box\n Cost:" + LootBox;
            UpgradeCount++;
        }
    }














    public  void Spawncount()
    {

        if (BloodMoney >= VictimPerClick)
        {
            SpawnPerClick += VictimPerClickMulti;
            
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
    public IEnumerator ConstantSpawn()
    {
        if(cheatSpawn == false)
        {
            cheatSpawn = true;
            var Spawned = 0;
            while (SpawnPerClick >= Spawned)
            {
                Instantiate(Victim, SpawnLocation(level), Quaternion.identity, null);
                Spawned++;
            }
            yield return new WaitForSeconds(0.1f);
            cheatSpawn = false;
        }
    }
    private bool cheatSpawn;
    IEnumerator LootBoxIsOpen()
    {
        LootBoxImage.gameObject.SetActive(true);
        var Randompick = 0;
        var Count = 7;
        var Selected = 0;
        var Boost = 0;
        while(Randompick <= Count)
        {
            yield return new WaitForSeconds(0.2f);
            Boost = Random.Range(0,7);
            if (Boost == 0) { LootBoxText.text = "Victum Value"; }
            if (Boost == 1) { LootBoxText.text = "Victum Frequency"; }
            if (Boost == 2) { LootBoxText.text = "Victum Spawn rate"; }
            if (Boost == 3) { LootBoxText.text = "Bus Frequency"; }
            if (Boost == 4) { LootBoxText.text = "Bus Amount"; }
            if (Boost == 5) { LootBoxText.text = "Bus Size"; }
            if (Boost == 6) { LootBoxText.text = "Victim Per Click"; }
            if (Boost == 7) { LootBoxText.text = "Prestige Reward"; }
            Randompick++;
        }
        if(Randompick >= Count)
        {
            Selected = Boost;
        }
        if (Selected == 0) { CustomerRewardMulti += 1; LootBoxText.text = "Victum Value\n" + "+" + 1; }
        if (Selected == 1) { CustSpawnFreqMulti += 0.1f; LootBoxText.text = "Victum Frequency\n" + "+" + 0.1f; }
        if (Selected == 2) { CustspawnAmtmulti += 1; LootBoxText.text = "Victum Spawn rate\n" + "+" + 1; }
        if (Selected == 3) { BusFreqMulti += 1; LootBoxText.text = "Bus Frequency\n" + "+" + 1; }
        if (Selected == 4) { BusAmtMulti += 1; LootBoxText.text = "Bus Amount\n" + "+" + 1; }
        if (Selected == 5) { BussizeMulti += 0.1f; LootBoxText.text = "Bus Size\n" + "+" + 1; }
        if (Selected == 6) { VictimPerClickMulti += 1; LootBoxText.text = "Victim Per Click\n" + "+" + 1; }
        if (Selected == 7) { PrestigeRewardMulti += 1; LootBoxText.text = "Prestige Reward\n" + "+" + 1; }
        yield return new WaitForSeconds(3);
        LootBoxText.text = "";
        LootBoxImage.gameObject.SetActive(false);
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
    public IEnumerator ClearArray(GameObject[] Select)
    {
        var Current = 0;
        while (Current <= Select.Length)
        {
            Select.SetValue(null, Current);
            Current++;
        }
        return null;
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
            if (spawned > ActiveBuses)
            {
                spawned = 0;
                BusSpawning = true;

            }
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
