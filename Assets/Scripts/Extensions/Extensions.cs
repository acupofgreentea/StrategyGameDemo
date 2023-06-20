﻿using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static T GetRandomItem<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static T GetClosestToTarget<T>(this List<T> list, Transform target) where T : MonoBehaviour
    {
        float minDistance = Mathf.Infinity;
        T closest = default;
        
        foreach (T item in list)
        {
            float distance = Vector3.Distance(item.transform.position, target.position);

            if (distance < minDistance)
            {
                closest = item;
                minDistance = distance;
            }
        }

        return closest;
    }
}