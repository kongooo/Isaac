using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireControl : MonoBehaviour
{

    private Animator animator;
    private bool none=false;

	void Start ()
	{
	    animator = GetComponent<Animator>();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "teer")
        {
            animator.SetBool("fire", true);
            if(none)
                Destroy(gameObject);
            none = true;
        }
        if (other.tag == "player")
        {
            other.gameObject.GetComponent<BasePlayer>().SufferDamage(1);
        }
    }

    
}
