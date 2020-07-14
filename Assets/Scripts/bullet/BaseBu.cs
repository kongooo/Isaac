using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBu : MonoBehaviour
{

    public int attack;
	
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "player")
        {
            col.gameObject.GetComponent<BasePlayer>().SufferDamage(attack);
        }
    }
}
