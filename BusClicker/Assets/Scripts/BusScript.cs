using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BusScript : MonoBehaviour
{
    public static int speed = 2;
    public static int decaytime = 4;
    public static int Multiplier = 0;
    public static int VictimValue = 1;
    void Update()
    {
        gameObject.transform.Translate(Vector3.down * Time.deltaTime * speed);
    }
    public void Awake()
    {
        StartCoroutine(Decay());
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
            GameManager.AddValue(VictimValue,Multiplier);
            Destroy(collision.gameObject);
        }
    }
}
