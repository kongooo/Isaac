using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomProp : MonoBehaviour
{

    public GameObject[] props;
    public Sprite open;

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "player")
        {
            if (GetComponent<SpriteRenderer>().sprite != open)
            {
                GetComponent<SpriteRenderer>().sprite = open;
                int propNumber = Random.Range(0, 3);
                GameObject prop = Instantiate(props[propNumber], transform.position, Quaternion.identity);
                prop.GetComponent<Rigidbody>().velocity = new Vector3(-2, -1, 2);
            }           
        }
    }
}
