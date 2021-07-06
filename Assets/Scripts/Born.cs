using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject[] enemyPrefabs;
    public bool createPlayer;
    private void Awake()
    {
        Invoke("BornTank", 1f);
        Destroy(gameObject, 1);
    }
    private void BornTank()
    {
        if(createPlayer)
        {
            GameObject play = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            int num = Random.Range(0, 2);
            Instantiate(enemyPrefabs[num], transform.position, Quaternion.identity);
        }
    }
}
