using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoldierMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 移动速度
    public Vector3 targetPosition; // 目标位置
    private bool isMoving = false; // 是否正在移动

    void Start()
    {
        // 初始化目标位置为当前位置
        targetPosition = transform.position;
    }

    void Update()
    {
        // 检查是否按下鼠标左键
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 如果射线集中了物体
            if (Physics.Raycast(ray, out hit))
            {
                // 设置新的目标位置
                targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                isMoving = true;
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
}
