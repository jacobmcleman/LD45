﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Transform initialLocation;
    Vector3 initialPos;

    [SerializeField]
    Transform endLocation;
    Vector3 endPos;

    [SerializeField]
    AnimationCurve t_curve;

    bool returning = false;

    [SerializeField]
    float oneWayTime;
    float timer;

#pragma warning restore 0649

    private Vector3 vel;
    public Vector3 velocity
    {
        get => vel;
    }

    // Start is called before the first frame update
    void Start()
    {
        initialPos = initialLocation.position;
        endPos = endLocation.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > oneWayTime)
        {
            timer = 0;
            returning = !returning;
        }
        float t = timer / oneWayTime;
        t = t_curve.Evaluate(t);
        Vector3 prevPos = transform.position;
        if (!returning)
        {
            transform.position = Vector3.Lerp(initialPos, endPos, t);
        }
        else
        {
            transform.position = Vector3.Lerp(endPos, initialPos, t);
        }
        vel = (transform.position - prevPos) / Time.deltaTime;
    }
}