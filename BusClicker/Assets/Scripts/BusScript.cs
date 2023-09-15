using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BusScript : MonoBehaviour
{
    public static int speed = 2;
    public static int decaytime = 4;
    public static int Multiplier = 0;
    public static float VictimValue = 1;
    void Update()
    {
        gameObject.transform.Translate(Vector3.down * Time.deltaTime * speed);
    }
    public void Awake()
    {
        StartCoroutine(Decay());
    }
    public void ResetStats()
    {
        speed = 2;
        decaytime = 4;
        VictimValue = 1;
        gameObject.transform.localScale = new Vector3(0.4f, 0.8f, 0.5f);
    //    Destroy(gameObject);
    }
    IEnumerator Decay()
    {
        yield return new WaitForSeconds(decaytime);
        Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ("Victim"))
        {
            GameManager.AddValue(VictimValue,GameManager.CurrentPrestigeBonus);
            Destroy(collision.gameObject);
        }
    }
}
