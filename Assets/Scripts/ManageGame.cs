using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageGame : MonoBehaviour
{
    public Image pausePanel;
   
	void Start ()
	{
	    Color color1= pausePanel.color;
	    color1.a = 0;
	    pausePanel.color = color1;
	}

	
	void Update () {
	    PauseController();
        end();
	}

    void PauseController()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0;
            Color color2 = pausePanel.color;
            color2.a = 1;
            pausePanel.color = color2;
        }
        if (pausePanel.color.a == 1)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Time.timeScale = 1;
                Color color3 = pausePanel.color;
                color3.a = 0;
                pausePanel.color = color3;
            }
            else if (Input.GetKey(KeyCode.Tab))
            {
                Application.Quit();
            }
        }
    }

    void end()
    {
        if((BasePlayer.Instance.HP<=0))
            Invoke("changeScene",2.5f);           
    }

    void changeScene()
    {
        SceneManager.LoadScene("stop");
    }
}
