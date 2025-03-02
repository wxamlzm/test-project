using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonDrawer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public float hexRadius = 0.5f;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        DrawHexagon();
    }

    void DrawHexagon()
    {
        lineRenderer.positionCount = 7;
        for(int i = 0; i < 6; i++)
        {
            float angle = i * 60 * Mathf.Deg2Rad;
            float x = hexRadius * Mathf.Cos(angle);
            float z = hexRadius * Mathf.Sin(angle);
            lineRenderer.SetPosition(i, new Vector3(x, 0, z));
        }
        // 闭合到一个点
        lineRenderer.SetPosition(6, lineRenderer.GetPosition(0));
    }

}
