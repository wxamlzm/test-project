using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonDrawer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public float hexRadius = 0.5f;

    // ���HexTitle������
    public bool isWalkable = true;
    public Vector2Int gridPosition;
    public float movementCost = 1f;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        // ����������
        DrawHexagon();

        // �����ײ��
        AddCollider();
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
        // �պϵ�һ����
        lineRenderer.SetPosition(6, lineRenderer.GetPosition(0));
    }

    void AddCollider()
    {
        // ����Ƿ�������ײ��
        if(GetComponent<Collider2D>() == null)
        {
            // Ϊ2D��Ϸ��Ӷ������ײ��
            PolygonCollider2D collider = gameObject.AddComponent<PolygonCollider2D>();
            Vector2[] points = new Vector2[6];

            for( int i = 0; i < 6; i++)
            {
                float angle = i * 60f * Mathf.Deg2Rad;
                points[i] = new Vector2(Mathf.Cos(angle) * hexRadius, Mathf.Sin(angle) * hexRadius);
            }

            collider.points = points;
        }
    }

}
