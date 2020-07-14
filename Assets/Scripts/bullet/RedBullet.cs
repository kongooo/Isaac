using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullet : MonoBehaviour
{
    private float y;
    private float x;
    public void RedMove()
    {
        x = Random.Range(-5, 5);
         y = Random.Range(4, 7);
        float x1 = Random.Range(-10, 10);
        float y1 = Random.Range(-10, 10);
        x += x1/10;
        y += y1 / 10;
        GetComponent<Rigidbody>().velocity = new Vector3(x, y,-2f*(7-y));
        Invoke("AddSpeed",0.3f);
    }

    void AddSpeed()
    {
        GetComponent<Rigidbody>().velocity += new Vector3(-x+(x)*0.5f, -y-1.5f, 0);
    }

    void OnTriggerEnter(Collider col )
    {
        if (col.tag == "room" || col.tag == "magic")
        {
            GetComponent<Animator>().SetTrigger("break");  
            GetComponent<Rigidbody>().velocity=Vector3.zero;
            Invoke("de",0.5f);
        }
            
    }

    void de()
    {
        Destroy(gameObject);
    }

    
}
