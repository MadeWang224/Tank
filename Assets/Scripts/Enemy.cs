using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// 速度
    /// </summary>
    public float speed = 3;
    /// <summary>
    /// tank随移动切换贴图数组
    /// </summary>
    public Sprite[] moveSprite;//上 右 下 左
    /// <summary>
    /// 贴图渲染组件
    /// </summary>
    private SpriteRenderer tankSr;

    /// <summary>
    /// 子弹预制件
    /// </summary>
    public GameObject bulletPrefab;
    /// <summary>
    /// 子弹旋转角度
    /// </summary>
    private Vector3 bulletEulerAngles;

    /// <summary>
    /// 子弹发射间隔
    /// </summary>
    private float timeInterval;
    /// <summary>
    /// 转向时间间隔
    /// </summary>
    private float timeIntervalDirection;
    /// <summary>
    /// y轴
    /// </summary>
    private float v = -1;
    /// <summary>
    /// x轴
    /// </summary>
    private float h;
    /// <summary>
    /// 爆炸特效
    /// </summary>
    public GameObject explosionPrefab;
    private void Start()
    {
        tankSr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (timeInterval >= 2f)
        {
            Attack();
        }
        else
        {
            timeInterval += Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    /// <summary>
    /// tank移动
    /// </summary>
    private void Move()
    {
        if(timeIntervalDirection>=3)
        {
            int num = Random.Range(0, 8);
            if(num>5)//下
            {
                v = -1;
                h = 0;
            }
            if(num==0)//上
            {
                v = 1;
                h = 0;
            }
            if(num>0&&num<=2)//左
            {
                v = 0;
                h = -1;
            }
            if(num>2&&num<=4)//右
            {
                v = 0;
                h = 1;
            }
            timeIntervalDirection = 0;
        }
        else
        {
            timeIntervalDirection += Time.fixedDeltaTime;
        }
        //y轴
        transform.Translate(Vector3.up * speed * v * Time.fixedDeltaTime, Space.World);
        if (v > 0)
        {
            tankSr.sprite = moveSprite[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }
        if (v < 0)
        {
            tankSr.sprite = moveSprite[2];
            bulletEulerAngles = new Vector3(0, 0, 180);
        }
        //x轴
        transform.Translate(Vector3.right * speed * h * Time.fixedDeltaTime, Space.World);
        if (h > 0)
        {
            tankSr.sprite = moveSprite[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }
        if (h < 0)
        {
            tankSr.sprite = moveSprite[3];
            bulletEulerAngles = new Vector3(0, 0, 90);
        }
    }
    /// <summary>
    /// 攻击方法
    /// </summary>
    private void Attack()
    {
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
            timeInterval = 0;
    }
    /// <summary>
    /// 死亡
    /// </summary>
    private void Die()
    {
        PlayerManager.instance.playerScore++;
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Enemy")
        {
            timeIntervalDirection = 4;
        }
    }
}
