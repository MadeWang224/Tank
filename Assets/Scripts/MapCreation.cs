using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    /// <summary>
    /// 资源数组:0家,1墙,2障碍,3出生效果,4河流,5草,6空气墙
    /// </summary>
    public GameObject[] item;
    /// <summary>
    /// 存放已有东西的位置
    /// </summary>
    private List<Vector3> itemPositionList = new List<Vector3>();
    private int enemyCounts;
    private float timeInterval = 3.0f;
    private void Awake()
    {
        InitMap();
    }
    private void Update()
    {
        if(timeInterval >= 3 && enemyCounts < 16)
        {
            CreateEnemy();
            timeInterval = 0;
            enemyCounts++;
        }
        else
        {
            timeInterval += Time.deltaTime;
        }
    }
    private void InitMap()
    {
        CreateHeart();
        CreateAirBarrier();
        CreateMap();
        BornEffect();
        
    }
    /// <summary>
    /// 封装一个实例化预制件方法
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="position"></param>
    /// <param name="quaternion"></param>
    private void CreateItem(GameObject gameObject,Vector3 position,Quaternion quaternion )
    {
        GameObject go = Instantiate(gameObject, position, quaternion);
        go.transform.SetParent(this.gameObject.transform);
        itemPositionList.Add(position);
    }
    /// <summary>
    /// 家的实例化
    /// </summary>
    private void CreateHeart()
    {
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        for(int i=-1;i<2;i++)
        {
            CreateItem(item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }
    }
    /// <summary>
    /// 空气墙的实例化
    /// </summary>
    private void CreateAirBarrier()
    {
        for(int i=-11;i<12;i++)
        {
            CreateItem(item[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        for (int i = -11; i < 12; i++)
        {
            CreateItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            CreateItem(item[6], new Vector3(-11, i, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            CreateItem(item[6], new Vector3(11, i, 0), Quaternion.identity);
        }
    }
    /// <summary>
    /// 创建随机位置
    /// </summary>
    /// <returns></returns>
    private Vector3 CreateRandomPosition()
    {
        while(true)
        {
            Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
            if(!itemPositionList.Contains(createPosition))
            {
                return createPosition;
            }
        }
    }
    /// <summary>
    /// 地图的实例化
    /// </summary>
    private void CreateMap()
    {
        for (int i = 0; i < 60; i++)
        {
            CreateItem(item[1], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(item[2], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(item[4], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(item[5], CreateRandomPosition(), Quaternion.identity);
        }
    }
    /// <summary>
    /// 玩家出生特效实例化
    /// </summary>
    private void BornEffect()
    {
        GameObject player = Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        player.GetComponent<Born>().createPlayer = true;
    }
    /// <summary>
    /// 敌人出生特效实例化
    /// </summary>
    private void CreateEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3 enemyPos = new Vector3();
        if(num==0)
        {
            enemyPos = new Vector3(-10, 8, 0);
        }
        if (num == 1)
        {
            enemyPos = new Vector3(0, 8, 0);
        }
        if (num == 2)
        {
            enemyPos = new Vector3(10, 8, 0);
        }
        CreateItem(item[3], enemyPos, Quaternion.identity);
    }
}
