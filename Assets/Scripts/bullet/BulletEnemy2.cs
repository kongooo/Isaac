using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy2 : MonoBehaviour
{

    public float speed;
    
    private Animator animator;
    private Rigidbody rigidbody;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();        
    }

    
    public void BulletMove(Vector3 player)
    {
        Vector3 posDif = Vector3.Normalize(player - transform.position);
        GetComponent<Rigidbody>().velocity = posDif*speed;       
        GetComponent<Rigidbody>().velocity+=new Vector3(0,0,-0.25f);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "room" || col.tag == "player" || col.tag == "magic"||col.tag=="up")
        {
            animator.SetTrigger("break");    
            GetComponent<Rigidbody>().velocity=Vector3.zero;
            Invoke("destroyafter", 0.5f);
        }           
    }

    private void destroyafter()
    {
        Destroy(gameObject);
    }
}
