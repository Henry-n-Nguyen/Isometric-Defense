using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [Header("Transform")]
    [SerializeField] private List<Transform> wayPoints = new List<Transform>();

    public static List<Transform[]> points = new List<Transform[]>();

    private void Awake()
    {
        for (int i = 0; i < wayPoints.Count; i++)
        {
            if (wayPoints[i].childCount == 0)
            {
                continue;
            }

            Transform[] tmpPoints = new Transform[wayPoints[i].childCount];

            for (int j = 0; j < tmpPoints.Length; j++)
            {
                tmpPoints[j] = wayPoints[i].GetChild(j);
            }

            points.Add(tmpPoints);
        }
    }
}
