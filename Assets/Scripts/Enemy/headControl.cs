using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headControl : MonoBehaviour
{

    public GameObject body;

	
	void Update () {
		if(body.GetComponent<EnemyBase>().Hp==0)
            Destroy(gameObject);
	}
}
