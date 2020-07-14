using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManageDoor : MonoBehaviour {

    private List<Transform> EnemyList=new List<Transform>();
    private int i;

    void Start()
    {
        GetComponent<SpriteRenderer>().color=Color.red;
    }

	void Update ()
	{
	    Transform[] gameObjects = gameObject.transform.parent.GetComponentsInChildren<Transform>();

	    foreach (Transform trans in gameObjects)
	    {
	        if (trans.tag == "Enemy")
	            i++;
	    }
	    if (i == 0)
	    {
	        GetComponent<BoxCollider>().isTrigger = true;
            GetComponent<SpriteRenderer>().color=Color.white;
        }
	        
	    i = 0;
	}

    
    
}
