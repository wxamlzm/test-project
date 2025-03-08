using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridManager : MonoBehaviour
{
    // 引用您现有的HexTile
    public GameObject hexTilePrefab;

    // 网格配置
    public int gridWidth = 5;
    public int gridHeight = 4;
    public float hexRadius = 0.5f; // 六边形半径

    // 六边形方向选择
    public bool flatTopOrientation = true; // true=平顶朝上，false=尖顶朝上

    // 存储所有六边形的引用
    private HexagonDrawer[,] hexGrid;

    void Start()
    {
        GenerateHexGrid();
    }

    void GenerateHexGrid()
    {
        // 确保有一个预制体
        if (hexTilePrefab == null)
        {
            Debug.LogError("请在Inspector中设置hexTilePrefab！");
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

    // 平顶朝上的六边形网格（适合地形）
    void GenerateFlatTopGrid()
    {
        // 六边形之间的间距计算 - 注意：我们使用实际尺寸而不是任意值
        float horizontalSpacing = hexRadius * 1.5f; // 水平方向的间距是直径的0.75倍
        float verticalSpacing = hexRadius * Mathf.Sqrt(3); // 垂直方向的间距是半径的√3倍

        // 计算网格的中心位置，以便居中布局
        Vector3 gridCenter = new Vector3(
            (gridWidth - 1) * horizontalSpacing * 0.5f,
            0,
            (gridHeight - 1) * verticalSpacing * 0.5f
        );

        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                // 计算位置
                float xPos = x * horizontalSpacing;
                float zPos = y * verticalSpacing;

                // 偶数列上移半个垂直间距
                if (x % 2 == 1)
                {
                    zPos += verticalSpacing * 0.5f;
                }

                // 将位置相对于网格中心偏移，以实现居中布局
                Vector3 position = new Vector3(xPos, 0, zPos) - gridCenter;

                // 实例化并放置六边形，注意旋转设置为正确的方向
                GameObject hexObject = Instantiate(
                    hexTilePrefab,
                    position,
                    flatTopOrientation ? Quaternion.identity : Quaternion.Euler(0, 30, 0)
                );

                hexObject.name = $"Hex_{x}_{y}";
                hexObject.transform.SetParent(transform, false);

                // 获取并配置HexagonDrawer组件
                HexagonDrawer hexDrawer = hexObject.GetComponent<HexagonDrawer>();
                if (hexDrawer != null)
                {
                    hexDrawer.hexRadius = hexRadius;
                    hexDrawer.gridPosition = new Vector2Int(x, y);

                    // 如果需要，在这里设置其他HexagonDrawer的属性

                    // 保存引用
                    hexGrid[x, y] = hexDrawer;
                }
            }
        }
    }

    // 尖顶朝上的六边形网格
    void GeneratePointyTopGrid()
    {
        // 六边形之间的间距计算
        float horizontalSpacing = hexRadius * Mathf.Sqrt(3); // 水平方向的间距是半径的√3倍
        float verticalSpacing = hexRadius * 1.5f; // 垂直方向的间距是直径的0.75倍

        // 计算网格的中心位置，以便居中布局
        Vector3 gridCenter = new Vector3(
            (gridWidth - 1) * horizontalSpacing * 0.5f,
            0,
            (gridHeight - 1) * verticalSpacing * 0.5f
        );

        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                // 计算位置
                float xPos = x * horizontalSpacing;
                float zPos = y * verticalSpacing;

                // 偶数行右移半个水平间距
                if (y % 2 == 1)
                {
                    xPos += horizontalSpacing * 0.5f;
                }

                // 将位置相对于网格中心偏移，以实现居中布局
                Vector3 position = new Vector3(xPos, 0, zPos) - gridCenter;

                // 实例化并放置六边形
                GameObject hexObject = Instantiate(
                    hexTilePrefab,
                    position,
                    flatTopOrientation ? Quaternion.identity : Quaternion.Euler(0, 30, 0)
                );

                hexObject.name = $"Hex_{x}_{y}";
                hexObject.transform.SetParent(transform, false);

                // 获取并配置HexagonDrawer组件
                HexagonDrawer hexDrawer = hexObject.GetComponent<HexagonDrawer>();
                if (hexDrawer != null)
                {
                    hexDrawer.hexRadius = hexRadius;
                    hexDrawer.gridPosition = new Vector2Int(x, y);

                    // 如果需要，在这里设置其他HexagonDrawer的属性

                    // 保存引用
                    hexGrid[x, y] = hexDrawer;
                }
            }
        }
    }

    // 获取特定位置的六边形
    public HexagonDrawer GetHexAt(int x, int y)
    {
        if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
        {
            return hexGrid[x, y];
        }
        return null;
    }

    // 用于在Editor中可视化显示网格布局
    void OnDrawGizmos()
    {
        if (!Application.isPlaying && hexTilePrefab != null)
        {
            // 临时显示网格布局
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