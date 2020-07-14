using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BossMove : MonoBehaviour
{
    private static BossMove _instance;
    public static BossMove Instance { get { return _instance; } }

    private Transform playerTrans;
    public GameObject bullet;
    public Slider BossSlider;
    public Image HeartImage, SliderImage,sliderBack;
    public int HPMax;

    private Rigidbody rigidbody;
    private Animator animator;
    private Vector3 dir;
   
    private Vector3 dire;
    private bool isfly=false;
    private bool ismove=false, isattack1=false, isattack2=false;

    private float seed,saw1=2;
    private float i = 2;
    [HideInInspector]public float countTime;
    private bool bossmove ;

	void Start ()
	{
	    countTime = 0.8f;
        SetColor(HeartImage,0);
        SetColor(SliderImage,0);
        SetColor(sliderBack,0);
	    bossmove = true;	    
	    _instance = this;
	    playerTrans = GameObject.FindGameObjectWithTag("player").transform;
        dir = playerTrans.position - transform.position;
        rigidbody = GetComponent<Rigidbody>();
	    animator = GetComponent<Animator>();
	}
		
	void Update ()
	{
	    if (Vector2.Distance(Camera.main.transform.position, transform.position) <= 7)
	    {	        
	        if (countTime <= 0)
	        {
	            if (bossmove)
	            {
	                SetColor(HeartImage, 1);
	                SetColor(SliderImage, 1);
	                SetColor(sliderBack, 1);
	                Debug.Log("move");
	                StartCoroutine("move");
	                bossmove = false;
	            }
	        }
        }
        BossSlider.value = (float)GetComponent<EnemyBase>().Hp / HPMax;
        countTime -= Time.deltaTime;
        if (i >= 2)
	    {
	        dir = playerTrans.position - transform.position;
	        i = 0;
	        seed = Random.Range(3, 10);
	    }
	    i += Time.deltaTime;
	}

    private void SetColor(Image image,int change)
    {
        Color color = image.color;
        color.a = change;
        image.color = color;
    }
    IEnumerator move( )
    {       
        animator.SetFloat("ready", dir.x);
        yield return new WaitForSeconds(0.5f);
        animator.SetFloat("ready", 0);
        animator.SetFloat("jump", dir.x);
        if (dir.x < 0)
        {
            rigidbody.AddForce(new Vector3(-0.00004f, 0.00004f, 0));
            yield return new WaitForSeconds(0.3f);
            rigidbody.AddForce(new Vector3(0, -0.00009f, 0));
            yield return new WaitForSeconds(0.2f);
        }
        if (dir.x > 0)
        {
            rigidbody.AddForce(new Vector3(0.00004f, 0.00004f, 0));
            yield return new WaitForSeconds(0.3f);
            rigidbody.AddForce(new Vector3(0, -0.00009f, 0));
            yield return new WaitForSeconds(0.2f);
        }
        animator.SetFloat("jump", 0);
        animator.SetFloat("land", dir.x);
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(Vector3.zero);
        yield return new WaitForSeconds(0.5f);
        animator.SetFloat("land", 0);
        if (saw1 >= 2)
        {
            saw1 = 0;
        }
        saw1 += 0.4f;
        if (saw1 >= 1.2f)
        {
            StopCoroutine("move");
            StartCoroutine("Attack");
            saw1 = 2;
        }
        else
        {
            yield return move();            
        }       
    }

    IEnumerator Attack()
    {
        animator.SetFloat("attack", dir.x);
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < 20; i++)
        {
            if (dir.x > 0)
            GameObject.Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<BossBullet>().BossBuMove(1,9,true);
            else if(dir.x <= 0)            
            GameObject.Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<BossBullet>().BossBuMove(-9,-1,true);           
        }
        yield return new WaitForSeconds(0.5f);
        animator.SetFloat("attack", 0);        
        StopCoroutine("Attack");
        StartCoroutine("attack");
    }

    IEnumerator attack()
    {
        animator.SetFloat("attack2", 1);
        yield return new WaitForSeconds(0.7f);       
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        rigidbody.velocity = new Vector3(0, 30, -30);
        animator.SetFloat("fly", 1);
        animator.SetFloat("attack2", 0);
        yield return new WaitForSeconds(0.5f);
        animator.SetFloat("fly", -1);
        dire = Vector3.Normalize(playerTrans.position - transform.position);
        rigidbody.velocity = dire * 30;
        yield return new WaitForSeconds(0.5f);
        isfly = true;        
        yield return new WaitForSeconds(1.5f);
        isfly = false;        
        rigidbody.isKinematic = false;
        animator.SetFloat("attack2", 0);
        StopCoroutine("attack");
        StartCoroutine("move");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isfly)
        {
            if (collision.gameObject||Mathf.Abs(transform.position.z-GameObject.FindGameObjectWithTag("player").transform.position.z)<3f)
            {
                if(collision.gameObject.tag=="up")
                    transform.Translate(new Vector3(0,-2,1.5f));
                animator.SetFloat("fly", 0);
                animator.SetFloat("attack2", -1);
                rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                for (int i=0;i<30;i++)
                GameObject.Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<BossBullet>().BossBuMove(-9,8,false);                 
                rigidbody.velocity = Vector3.zero;
                rigidbody.isKinematic = true;                
            }
        }
    }
}
