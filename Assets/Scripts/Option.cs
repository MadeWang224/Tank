using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    private int choice = 1;
    public Transform pos1;
    public Transform pos2;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            choice = 1;
            transform.position = pos1.position;
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            choice = 2;
            transform.position = pos2.position;
        }
        if(choice==1&&Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(1);
        }
    }
}
