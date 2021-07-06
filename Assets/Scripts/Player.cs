using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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
    /// 爆炸特效
    /// </summary>
    public GameObject explosionPrefab;
    /// <summary>
    /// 无敌特效
    /// </summary>
    public GameObject defendEffectPrefab;
    /// <summary>
    /// 是否处于无敌状态
    /// </summary>
    private bool isDefend = true;
    /// <summary>
    /// 无敌时间
    /// </summary>
    private float defendTime = 3;

    /// <summary>
    /// 声音组件
    /// </summary>
    public AudioSource moveAudio;
    public AudioClip[] tankAudio;
    private void Start()
    {
        tankSr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Defend();
        if (timeInterval >= 0.5f)
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
        //y轴
        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * speed * v * Time.fixedDeltaTime, Space.World);
        if(v>0)
        {
            tankSr.sprite = moveSprite[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }
        if(v<0)
        {
            tankSr.sprite = moveSprite[2];
            bulletEulerAngles = new Vector3(0, 0, 180);
        }
        if(!Mathf.Approximately(v,0))
        {
            moveAudio.clip = tankAudio[1];
            if(!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        //设置优先级
        if(v!=0)
        {
            return;
        }
        //x轴
        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * speed * h * Time.fixedDeltaTime, Space.World);
        if(h>0)
        {
            tankSr.sprite = moveSprite[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }
        if(h<0)
        {
            tankSr.sprite = moveSprite[3];
            bulletEulerAngles = new Vector3(0, 0, 90);
        }
        if(!Mathf.Approximately(h,0))
        {
            moveAudio.clip = tankAudio[1];
            if(!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        else
        {
            moveAudio.clip = tankAudio[0];
            if(!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
    }
    /// <summary>
    /// 攻击方法
    /// </summary>
    private void Attack()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
            bullet.GetComponent<Bullect>().isPlayBullet = true;
            timeInterval = 0;
        }
    }
    /// <summary>
    /// 死亡
    /// </summary>
    private void Die()
    {
        if(isDefend)
        {
            return;
        }
        PlayerManager.instance.isDead = true;
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    /// <summary>
    /// 无敌
    /// </summary>
    private void Defend()
    {
        if(isDefend)
        {
            defendEffectPrefab.SetActive(true);
            defendTime -= Time.deltaTime;
            if(defendTime<=0)
            {
                isDefend = false;
                defendEffectPrefab.SetActive(false);
            }
        }
    }
}
