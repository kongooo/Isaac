using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shitControl : MonoBehaviour
{

    public Sprite[] sprites;
    private SpriteRenderer spriterender;

    private int count;

    void Awake()
    {
        count = sprites.Length-1;
        spriterender = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "teer")
        {
            if (count >= 0)
            {
                spriterender.sprite = sprites[count];
                count--;
                if (count == -1)
                    GetComponent<BoxCollider>().enabled = false;
            }           

        }
    }
}
