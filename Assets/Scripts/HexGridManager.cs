using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridManager : MonoBehaviour
{
    // ���������е�HexTile
    public GameObject hexTilePrefab;

    // ��������
    public int gridWidth = 5;
    public int gridHeight = 4;
    public float hexRadius = 0.5f; // �����ΰ뾶

    // �����η���ѡ��
    public bool flatTopOrientation = true; // true=ƽ�����ϣ�false=�ⶥ����

    // �洢���������ε�����
    private HexagonDrawer[,] hexGrid;

    void Start()
    {
        GenerateHexGrid();
    }

    void GenerateHexGrid()
    {
        // ȷ����һ��Ԥ����
        if (hexTilePrefab == null)
        {
            Debug.LogError("����Inspector������hexTilePrefab��");
            return;
        }

        hexGrid = new HexagonDrawer[gridWidth, gridHeight];

        if (flatTopOrientation)
        {
            GenerateFlatTopGrid();
        }
        else
        {
            GeneratePointyTopGrid();
        }
    }

    // ƽ�����ϵ������������ʺϵ��Σ�
    void GenerateFlatTopGrid()
    {
        // ������֮��ļ����� - ע�⣺����ʹ��ʵ�ʳߴ����������ֵ
        float horizontalSpacing = hexRadius * 1.5f; // ˮƽ����ļ����ֱ����0.75��
        float verticalSpacing = hexRadius * Mathf.Sqrt(3); // ��ֱ����ļ���ǰ뾶�ġ�3��

        // �������������λ�ã��Ա���в���
        Vector3 gridCenter = new Vector3(
            (gridWidth - 1) * horizontalSpacing * 0.5f,
            0,
            (gridHeight - 1) * verticalSpacing * 0.5f
        );

        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                // ����λ��
                float xPos = x * horizontalSpacing;
                float zPos = y * verticalSpacing;

                // ż�������ư����ֱ���
                if (x % 2 == 1)
                {
                    zPos += verticalSpacing * 0.5f;
                }

                // ��λ���������������ƫ�ƣ���ʵ�־��в���
                Vector3 position = new Vector3(xPos, 0, zPos) - gridCenter;

                // ʵ���������������Σ�ע����ת����Ϊ��ȷ�ķ���
                GameObject hexObject = Instantiate(
                    hexTilePrefab,
                    position,
                    flatTopOrientation ? Quaternion.identity : Quaternion.Euler(0, 30, 0)
                );

                hexObject.name = $"Hex_{x}_{y}";
                hexObject.transform.SetParent(transform, false);

                // ��ȡ������HexagonDrawer���
                HexagonDrawer hexDrawer = hexObject.GetComponent<HexagonDrawer>();
                if (hexDrawer != null)
                {
                    hexDrawer.hexRadius = hexRadius;
                    hexDrawer.gridPosition = new Vector2Int(x, y);

                    // �����Ҫ����������������HexagonDrawer������

                    // ��������
                    hexGrid[x, y] = hexDrawer;
                }
            }
        }
    }

    // �ⶥ���ϵ�����������
    void GeneratePointyTopGrid()
    {
        // ������֮��ļ�����
        float horizontalSpacing = hexRadius * Mathf.Sqrt(3); // ˮƽ����ļ���ǰ뾶�ġ�3��
        float verticalSpacing = hexRadius * 1.5f; // ��ֱ����ļ����ֱ����0.75��

        // �������������λ�ã��Ա���в���
        Vector3 gridCenter = new Vector3(
            (gridWidth - 1) * horizontalSpacing * 0.5f,
            0,
            (gridHeight - 1) * verticalSpacing * 0.5f
        );

        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                // ����λ��
                float xPos = x * horizontalSpacing;
                float zPos = y * verticalSpacing;

                // ż�������ư��ˮƽ���
                if (y % 2 == 1)
                {
                    xPos += horizontalSpacing * 0.5f;
                }

                // ��λ���������������ƫ�ƣ���ʵ�־��в���
                Vector3 position = new Vector3(xPos, 0, zPos) - gridCenter;

                // ʵ����������������
                GameObject hexObject = Instantiate(
                    hexTilePrefab,
                    position,
                    flatTopOrientation ? Quaternion.identity : Quaternion.Euler(0, 30, 0)
                );

                hexObject.name = $"Hex_{x}_{y}";
                hexObject.transform.SetParent(transform, false);

                // ��ȡ������HexagonDrawer���
                HexagonDrawer hexDrawer = hexObject.GetComponent<HexagonDrawer>();
                if (hexDrawer != null)
                {
                    hexDrawer.hexRadius = hexRadius;
                    hexDrawer.gridPosition = new Vector2Int(x, y);

                    // �����Ҫ����������������HexagonDrawer������

                    // ��������
                    hexGrid[x, y] = hexDrawer;
                }
            }
        }
    }

    // ��ȡ�ض�λ�õ�������
    public HexagonDrawer GetHexAt(int x, int y)
    {
        if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
        {
            return hexGrid[x, y];
        }
        return null;
    }

    // ������Editor�п��ӻ���ʾ���񲼾�
    void OnDrawGizmos()
    {
        if (!Application.isPlaying && hexTilePrefab != null)
        {
            // ��ʱ��ʾ���񲼾�
            float horizontalSpacing, verticalSpacing;

            if (flatTopOrientation)
            {
                horizontalSpacing = hexRadius * 1.5f;
                verticalSpacing = hexRadius * Mathf.Sqrt(3);
            }
            else
            {
                horizontalSpacing = hexRadius * Mathf.Sqrt(3);
                verticalSpacing = hexRadius * 1.5f;
            }

            Vector3 gridCenter = new Vector3(
                (gridWidth - 1) * horizontalSpacing * 0.5f,
                0,
                (gridHeight - 1) * verticalSpacing * 0.5f
            );

            Gizmos.color = Color.yellow;

            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    float xPos = x * horizontalSpacing;
                    float zPos = y * verticalSpacing;

                    if (flatTopOrientation && x % 2 == 1)
                    {
                        zPos += verticalSpacing * 0.5f;
                    }
                    else if (!flatTopOrientation && y % 2 == 1)
                    {
                        xPos += horizontalSpacing * 0.5f;
                    }

                    Vector3 position = new Vector3(xPos, 0, zPos) - gridCenter;
                    Gizmos.DrawWireSphere(position, hexRadius * 0.8f);
                }
            }
        }
    }
}