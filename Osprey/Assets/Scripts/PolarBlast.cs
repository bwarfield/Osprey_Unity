﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PolarBlast : MonoBehaviour
{

    public List<Transform> transformChain;
    public int chainJumps;
    //private QuadraticBezierChain beamPath;
    public GameObject parent;
    public PlayerLaserTorpedo beamProjectile;
    //private List<GameObject> targets;
    public Vector3 offsetFromParent;
    private Quaternion rotation;
    // Use this for initialization

    private Transform thisTransform;
	void Start () {
        //beamPath = GetComponent<QuadraticBezierChain>();
        thisTransform = GetComponent<Transform>();
        rotation = thisTransform.rotation;
        //targets = new List<GameObject>();
	}


    

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire3"))
        {
            GetPath();
            PlayerLaserTorpedo beam = Instantiate(beamProjectile, thisTransform.position, thisTransform.rotation) as PlayerLaserTorpedo;
            beam.Init(transformChain);
        }
	}


    private void GetPath()
    {
        List<GameObject> allEnemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        //targets.Clear();
        transformChain.Clear();
        transformChain.Add(thisTransform);

        for (int i = 0; i < chainJumps; i++)
        {

            if (allEnemies.Count < 1)
            {
                break;
            }

            float nearestSqrDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;


            foreach (GameObject enemy in allEnemies)
            {
                float sqrDistance = (enemy.transform.position - transformChain[0].position).sqrMagnitude;
                if (sqrDistance < nearestSqrDistance)
                {
                    nearestSqrDistance = sqrDistance;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy)
            {
                transformChain.Add(nearestEnemy.transform);
                //targets.Add(nearestEnemy);
                allEnemies.Remove(nearestEnemy);
            }
        }

        //List<QuadraticBezierPoints> chain = new List<QuadraticBezierPoints>();

        //for (int i = 0; i < transformChain.Count; i++)
        //{
        //    Vector3 start, pull, end;

        //    start = transformChain[i].position;
        //    if (i == 0)
        //    {
        //        pull = transformChain[i].position + transformChain[i].up * 3.0f;
        //        if (i == transformChain.Count - 1)
        //        {
        //            end = transformChain[i].position + transformChain[i].up * 4.0f;
        //        }
        //        else
        //        {
        //            end = transformChain[i + 1].position;
        //        }

        //    }
        //    else if (i == transformChain.Count - 1)
        //    {
        //        pull = transformChain[i].position + (transformChain[i].position - chain[i - 1].p1).normalized * 4;
        //        end = transformChain[i].position + (transformChain[i].position - chain[i - 1].p1).normalized * 8;
        //    }
        //    else
        //    {
        //        pull = transformChain[i].position + (transformChain[i].position - chain[i - 1].p1).normalized * 4.0f;
        //        end = transformChain[i + 1].position;
        //    }

        //    start.z = pull.z = end.z = 0;

        //    QuadraticBezierPoints link = new QuadraticBezierPoints(start, pull, end);

        //    chain.Add(link);
        //}

        //beamPath.SetBezierChain(TransformsToBezierPoints(transformChain));
    }


    private void LateUpdate()
    {
        thisTransform.position = parent.transform.position + offsetFromParent;
        thisTransform.rotation = rotation;
    }
}
