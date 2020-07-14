using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private Renderer renderer;
    public int Hp,playerDamage;
    public float deathTime;
    public GameObject[] DeathSpecial;
    private bool isdeath,isspecial=false;
    private GameObject player;
    public GameObject endImage;
    private int specilaNumber;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        player=GameObject.FindGameObjectWithTag("player");
    }

    void Update()
    {
        renderer.material.color = Color.Lerp(renderer.material.color, Color.white, Time.deltaTime * 5);
        if (Hp <= 0)
            isdeath = true;
        death();
        if (gameObject.name != "boss")
            if (RoomManage.Instance.islast)
                transform.localPosition = new Vector3(
                    Mathf.Clamp(transform.localPosition.x, -1.7f, 1.7f),
                    Mathf.Clamp(transform.localPosition.y, -0.9f, 0.9f),
                    transform.localPosition.z);
    }
    public void EnemyDamage(int damage)
    {
        if (Hp > 0)
        {
            Hp -= damage;
            renderer.material.color = Color.red;
        }
    }
    private void death()
    {
        if (isdeath)
        {
            GetComponent<Animator>().SetBool("isdeath", true);           
            Invoke("destroy", deathTime);
        }
    }

    private void destroy()
    {
        if (GetComponent<GRID>())
        {
            for(int i=0;i<GetComponent<GRID>().PathObject.Count;i++)
                Destroy(GetComponent<GRID>().PathObject[i]);
        }
        specilaNumber = Random.Range(0, 32);
        Instantiate(DeathSpecial[specilaNumber], transform.position, Quaternion.identity);
        if (GetComponent<BossMove>())
        {
            GameObject End= Instantiate(endImage, Vector3.zero, Quaternion.identity);
            End.transform.SetParent(gameObject.transform.parent,false);
        }
        Destroy(gameObject);      
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag=="player")
            col.gameObject.GetComponent<BasePlayer>().SufferDamage(playerDamage);
    }

}
