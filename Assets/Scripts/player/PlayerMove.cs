using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private static PlayerMove _instance;
    public static PlayerMove Instance { get { return _instance; } }
    private float x, y;
    private Animator animator_body;
    private Animator animator_head;
    private Rigidbody rigidbody;
    public float speed;
    public GameObject body;
    public GameObject head;
   
	void Start ()
	{
	    _instance = this;
	    animator_body = body.GetComponent<Animator>();
	    animator_head = head.GetComponent<Animator>();
	    rigidbody = GetComponent<Rigidbody>();
	}


    void FixedUpdate()
    {
        Vector3 currentPos = transform.position;
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        if (x == 0)
            animator_body.SetFloat("lf", 0);
        animator_body.SetFloat("lf", x);
        if (y == 0)
            animator_body.SetBool("ud", false);
        else
        {
            animator_body.SetBool("ud", true);
        }

        
        if ((!Input.GetKey(KeyCode.LeftArrow)) && (!Input.GetKey(KeyCode.RightArrow)) &&
            (!Input.GetKey(KeyCode.UpArrow)) && (!Input.GetKey(KeyCode.DownArrow)))
        {
            animator_head.SetFloat("leftRight", x);
            animator_head.SetFloat("backFront", -y);
        }
        if ((Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.RightArrow)) ||
            (Input.GetKey(KeyCode.UpArrow)) || (Input.GetKey(KeyCode.DownArrow)))
        {
            animator_head.SetFloat("leftRight", 0);
            animator_head.SetFloat("backFront", 0);
        }

    if (x == 0 && y == 0 && (!Input.GetKey(KeyCode.LeftArrow)) && (!Input.GetKey(KeyCode.RightArrow)) && (!Input.GetKey(KeyCode.UpArrow)) && (!Input.GetKey(KeyCode.DownArrow)))
        {

            animator_body.SetTrigger("stop");
            animator_head.SetTrigger("stop");
        }       
        rigidbody.MovePosition(Vector2.Lerp(currentPos, currentPos + new Vector3(x, y, 0), speed * Time.deltaTime));
    }
}
