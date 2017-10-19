﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : Entity {
    public float speed = 1.0f;
    public Entity[] shot;
    public GameObject[] gunBarrel;
    public float shotFireRate = 25.0f;
    protected float lastShot;
    private float health;
    public float maxHealth;
    private bool hit;
    public float flashTime;
    private float hitTimer;
    public Color[] colors;

    public GameObject playerTarget;
    void Start () {
        thisTransform = GetComponent<Transform>();
        thisRenderer.sharedMaterial = materials[(int)polarity];
        hitTimer = 0;
        playerTarget = GameObject.FindGameObjectWithTag("Player");
    }
	
	void Update () {

        foreach(GameObject barrel in gunBarrel)
        {
            if (Time.time - lastShot > 1 / shotFireRate)
            {
                if (playerTarget.activeSelf)
                {
                    Vector3 diff = playerTarget.transform.position - barrel.transform.position;
                    float ang = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                    barrel.transform.rotation = Quaternion.AngleAxis(ang -90, Vector3.forward);
                }

                Instantiate(shot[(int)polarity], barrel.transform.position, barrel.transform.rotation);
                lastShot = Time.time;
            }

        }
        if(hit)
        {
            if (Time.time < hitTimer)
            {
                thisRenderer.material.SetColor("_SpecColor", colors[1]);
            }else
            {
                hit = false;
                thisRenderer.material.SetColor("_SpecColor", colors[0]);
            }
        }
        
        
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerBullet") || other.CompareTag("Player"))
        {
            //Debug.Log("Enemy hit!");
            hit = true;
            hitTimer = Time.time + flashTime;
        }
    }
}
