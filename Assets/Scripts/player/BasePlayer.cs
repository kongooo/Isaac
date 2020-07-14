using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePlayer : MonoBehaviour
{
    private static BasePlayer _instance;
    private int change = 5;
    public static BasePlayer Instance { get { return _instance; } }

    public int HP;
    public GameObject deathBody,head,body,soul;
    public Slider hpslider,speedSlider,AttackSlider;
    public Text hpText;
    public int maxhp = 6;

    private bool isdeath=false;
    private Renderer renderer1,renderer2;
	void Awake ()
	{
	    HP = maxhp;       
	    _instance = this;
        setTransparency(soul,0);
	    setTransparency(deathBody,0);
	    renderer1 = head.GetComponent<Renderer>();
	    renderer2 = body.GetComponent<Renderer>();
	}

    void Update()
    {
        renderer1.material.color = Color.Lerp(renderer1.material.color, Color.white, Time.deltaTime * change);
        renderer2.material.color = Color.Lerp(renderer2.material.color, Color.white, Time.deltaTime * change);
        hpslider.value = (float) HP / maxhp;
        hpText.text = HP + "/" + maxhp;
        AttackSlider.value =(float)PlayerAttack.Instance.fireRate / 4;
        speedSlider.value = (float) PlayerMove.Instance.speed / 10;
        SetDeath();
    }

    public void SufferDamage(int reduce)
    {
        if (HP > 0)
        {
            HP -= reduce;
            renderer1.material.color=Color.red;
            renderer2.material.color = Color.red; 
        }        
    }

    public void AddHP(int hp)
    {
        if(HP<maxhp)
        HP += hp;
    }
    //检测何时死亡
    private void SetDeath()
    {
        if (HP == 0)
        {
            head.SetActive(false);
            body.SetActive(false);
            setTransparency(deathBody,1);
            deathBody.GetComponent<Animator>().SetTrigger("death");          
            Invoke("setGravity",0.8f);
            HP = -1;
            gameObject.GetComponent<PlayerMove>().enabled = false;
            gameObject.GetComponent<PlayerAttack>().enabled = false;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void setTransparency(GameObject go,float change)
    {
        Color color1 = go.GetComponent<SpriteRenderer>().color;
        color1.a = change;
        go.GetComponent<SpriteRenderer>().color = color1;
    }

    private void setGravity()
    {
        //soul.GetComponent<SpriteRenderer>().DOFade(1, 1f);
        soul.GetComponent<Rigidbody2D>().gravityScale = -1;
    }

    void OnCollisionEnter(Collision Other)
    {
        switch (Other.collider.tag)
        {
            case "heart":
            {
                if (HP >= maxhp - 2)
                {
                    HP = maxhp;
                    if (HP < maxhp)
                        Destroy(Other.gameObject);
                }
                else if (HP < maxhp - 2)
                {
                    HP += 2;
                    Destroy(Other.gameObject);
                }
            }
                break;
            case "speed":
            {
                if (PlayerMove.Instance.speed < 9)
                {
                    PlayerMove.Instance.speed += 2;
                    Destroy(Other.gameObject);
                }
            }
                break;
            case "Attack":
            {
                if (PlayerAttack.Instance.fireRate < 4)
                {
                    PlayerAttack.Instance.fireRate += 1;
                    Destroy(Other.gameObject);
                }
            }
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "thorn":
            {
                SufferDamage(1);
            }
                break;
            case "updoor":
            {
                Camera.main.GetComponent<Transform>().Translate(new Vector3(0,10,0));
                    transform.Translate(0,4,0);
            }
                break;
            case "downdoor":
            {
                Camera.main.GetComponent<Transform>().Translate(new Vector3(0, -10, 0));
                    transform.Translate(0,-4,0);
            }
                break;
            case "leftdoor":
            {
                Camera.main.GetComponent<Transform>().Translate(new Vector3(-18, 0, 0));
                    transform.Translate(-6,0,0);
            }
                break;
            case "rightdoor":
            {
                Camera.main.GetComponent<Transform>().Translate(new Vector3(18, 0, 0));
                    transform.Translate(6,0,0);
            }
                break;

        }
    }
}
