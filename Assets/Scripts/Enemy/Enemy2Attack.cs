using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Attack : MonoBehaviour
{

    public GameObject buttet;
    private GameObject player;
    private Vector3 playerPos;

    private float j=1f;

	void Start () {
		player=GameObject.FindGameObjectWithTag("player");
	}
	
	
	void Update ()
    {
        if (Vector2.Distance(Camera.main.transform.position, transform.position) <= 7)
        {
            if (j >= 1f)
            {
                j = 0;
                playerPos = player.GetComponent<Transform>().position;
                GameObject bu = GameObject.Instantiate(buttet, transform.position, Quaternion.identity);
                bu.GetComponent<BulletEnemy2>().BulletMove(playerPos);
            }
            j += Time.deltaTime;
        }
           
	}
}
