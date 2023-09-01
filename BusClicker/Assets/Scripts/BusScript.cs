using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BusScript : MonoBehaviour
{
    public int speed;
    public int decaytime;
    public static int Multiplier = 0;
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
            GameManager.AddValue(1,Multiplier);
            Destroy(collision.gameObject);
        }
    }
}
