using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    public float fireRate;   //子弹发射频率
    public float bulletSpeed,zforce;   //子弹速度
    public float force;

    public Animator attack_animator;

    private float restTime=0;    //计时器
    public Transform left,right;
    public GameObject teer;
    private bool isleft=true;
    private GameObject Teer;

    private static PlayerAttack _instance;
    public static PlayerAttack Instance { get { return _instance; } }

    void Awake()
    {
        _instance = this;
    }

	void FixedUpdate ()
	{
	    restTime += Time.deltaTime;
	    if (Input.GetKey(KeyCode.LeftArrow))
	    {
           attack_animator.SetTrigger("attack_left");
	       firePause(Vector3.left);
        }
	    if (Input.GetKey(KeyCode.RightArrow))
	    {
	        attack_animator.SetTrigger("attack_right");
            firePause(Vector3.right);
	    }
	    if (Input.GetKey(KeyCode.UpArrow))
	    {
	        attack_animator.SetTrigger("attack_back");
            firePause(Vector3.up);
	    }
	    if (Input.GetKey(KeyCode.DownArrow))
	    {
	        attack_animator.SetTrigger("attack_front");
            firePause(Vector3.down);
	    }
    }

    private void fire(Transform bulletTrans, Vector3 direction)
    {
        if (restTime > 1 / fireRate)
        {           
            Teer = GameObject.Instantiate(teer, bulletTrans.position, Quaternion.identity);          
            Teer.GetComponent<Rigidbody>().AddForce(direction*force);
            Teer.GetComponent<Rigidbody>().AddForce(0,0,zforce);
            teer.GetComponent<Rigidbody>().velocity +=
                transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
            if(direction.x!=0)
            Invoke("drop",0.3f);
            restTime = 0;
        }
    }

    private void drop()
    {
        Teer.GetComponent<Rigidbody>().AddForce(0, -60f, 0);
    }
    //左右按次序发射子弹
    private void firePause(Vector3 dir)
    {
        if (isleft)
        {
            fire(left, dir);
            isleft = false;
        }
        else
        {
            fire(right, dir);
            isleft = true;
        }
        
    }

    

    
}
