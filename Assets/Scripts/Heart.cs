using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public GameObject explosionPrefab;
    public Sprite broken;
    private SpriteRenderer sr;
    public AudioClip dieAudio;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public void Die()
    {
        sr.sprite = broken;
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        PlayerManager.instance.isDefeat = true;
        AudioSource.PlayClipAtPoint(dieAudio, transform.position);

    }
}
