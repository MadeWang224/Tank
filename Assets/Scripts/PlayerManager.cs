using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public int lifeValue = 3;
    public int playerScore = 0;
    public bool isDead;
    public bool isDefeat;
    public GameObject born;
    public Text playScoreText;
    public Text lifeValueText;
    public GameObject isDefeatUI;
    public static PlayerManager instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        Loss();
        playScoreText.text = playerScore.ToString();
        lifeValueText.text = lifeValue.ToString();
        if (isDefeat)
        {
            isDefeatUI.SetActive(true);
            Invoke("ReturnToTheMainMenu", 3);
        }
    }
    private void Loss()
    {
        if(isDead)
        {
            if(lifeValue<=0)
            {
                isDefeat = true;
            }
            else
            {
                lifeValue--;
                GameObject go = Instantiate(born,new Vector3(-2,-8,0), Quaternion.identity);
                go.GetComponent<Born>().createPlayer = true;
                isDead = false;
            }
        }
    }
    private void ReturnToTheMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
