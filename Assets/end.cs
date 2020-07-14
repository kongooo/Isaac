using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class end : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="player")
            SceneManager.LoadScene("stop");
    }
}
