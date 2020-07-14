using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMove : MonoBehaviour
{
    private Transform playerTrans;
    private Rigidbody rigidbody;
    public float flySpeed;
	
	void Start () {
		playerTrans=GameObject.FindGameObjectWithTag("player").transform;
	    rigidbody = GetComponent<Rigidbody>();
	}
	
	
	void Update ()
    {
        if (Vector2.Distance(Camera.main.transform.position, transform.position) <= 7)
            rigidbody.MovePosition(Vector3.Lerp(transform.position,playerTrans.position,flySpeed*Time.deltaTime));
	}
}
