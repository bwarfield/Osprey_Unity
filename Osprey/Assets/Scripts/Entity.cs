﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum polarType: int { LIGHT = 0, DARK = 1 };

public abstract class Entity: MonoBehaviour
{

    //utility vars
    
    public Transform thisTransform = null;
    [SerializeField]
    public Renderer thisRenderer = null;
    [SerializeField]
    public Material[] materials;
    public GameObject gibs = null;


    //weapon vars
    //public Entity[] shot;
    //public GameObject[] gunBarrel;
    //public float shotFireRate = 25.0f;
    //protected float lastShot;

    //status vars
    [SerializeField]
    protected polarType polarity;

    public polarType Polarity
    {
        get
        { return polarity; }
    }

    public virtual void SetPolarity(polarType value) { polarity = value; }

    public virtual void Init()
    {

    }

    public virtual void ExitArena()
    {
        Destroy(gameObject);
    }
}
