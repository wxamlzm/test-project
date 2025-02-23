using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoldierMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 移动速度
    public Vector3 targetPosition; // 目标位置
    private bool isMoving = false; // 是否正在移动
    private Vector3 startPosition; // 记录开始的位置
    private float totalDistance; // 记录总距离

    void Start()
    {
        // 初始化目标位置为当前位置
        targetPosition = transform.position;
        startPosition = transform.position;
    }

    void Update()
    {
        // 右键点击移动
        if (Input.GetMouseButtonDown(1)) // 1代表右键
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 如果射线集中了物体
            if (Physics.Raycast(ray, out hit))
            {
                startPosition = transform.position; // 记录开始位置
                // 设置新的目标位置
                targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                totalDistance = Vector3.Distance(startPosition, targetPosition); // 计算总距离
                isMoving = true;
            }
        }

        // 左键点击取消移动
        if(Input.GetMouseButtonDown(0)) // 0代表左键
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                // 检查点击的是否是第三块地
                if(hit.collider.gameObject.name == "Ground3")
                {
                    TryCancelMovement();
                }
            }
        }

        // 如果正在移动
        if (isMoving)
        {
            // 计算向目标移动的方向
            Vector3 moveDirection = (targetPosition - transform.position).normalized;

            // 移动物体
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 检查是否达到目标位置
            if(Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
            }
        }

    }

    void TryCancelMovement()
    {
        if (!isMoving) return; // 如果没在移动，则直接返回
        // 计算已经移动的距离占总距离的比例
        float currentDistance = Vector3.Distance(startPosition, transform.position);
        float moveProgress = currentDistance / totalDistance;

        // 如果移动超过一半，允许取消
        if(moveProgress < 0.5f)
        {
            isMoving = false;
            targetPosition = startPosition;
            Debug.Log("移动已取消");
        }
        else 
        {
            Debug.Log("已经超过一半距离，无法取消移动");
        }
    }
}
