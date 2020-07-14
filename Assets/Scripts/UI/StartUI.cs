using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour {

	void Update () {
		if(Input.GetKey(KeyCode.Space))
            SceneManager.LoadScene("main");
        if(Input.GetKey(KeyCode.Escape))
            Application.Quit();
	}
}
