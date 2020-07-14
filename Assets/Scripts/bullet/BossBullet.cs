using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    void Update()
    {
        if(Vector2.Distance(transform.position,GameObject.Find("boss").transform.position)>10)
            Destroy(gameObject);
    }

    public void BossBuMove(int m,int n,bool isAttack)
    {
        float y;
        float x1 = Random.Range(-10, 10);
        float x = Random.Range(m, n);
        float y1 = Random.Range(-10, 10);
        if(isAttack)
        y = Random.Range(Mathf.Abs(n),Mathf.Abs(m));
        else
        {
            y = Random.Range(m, n);
        }
        x = x + x1 / 10;
        y = y + y1 / 10;
        GetComponent<Rigidbody>().AddForce(new Vector3(x*2.5f, y*2.5f, -(10 - Mathf.Abs(y)) * 1.2f));
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "room" || col.tag == "magic")
        {
            GetComponent<Animator>().SetTrigger("break");
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Invoke("de", 0.5f);
        }
    }

    void de()
    {
        Destroy(gameObject);
    }
}
