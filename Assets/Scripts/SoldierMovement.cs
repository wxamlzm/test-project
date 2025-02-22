using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoldierMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // �ƶ��ٶ�
    public Vector3 targetPosition; // Ŀ��λ��
    private bool isMoving = false; // �Ƿ������ƶ�

    void Start()
    {
        // ��ʼ��Ŀ��λ��Ϊ��ǰλ��
        targetPosition = transform.position;
    }

    void Update()
    {
        // ����Ƿ���������
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ������߼���������
            if (Physics.Raycast(ray, out hit))
            {
                // �����µ�Ŀ��λ��
                targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                isMoving = true;
            }
        }

        // ��������ƶ�
        if (isMoving)
        {
            // ������Ŀ���ƶ��ķ���
            Vector3 moveDirection = (targetPosition - transform.position).normalized;

            // �ƶ�����
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // ����Ƿ�ﵽĿ��λ��
            if(Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
            }
        }

    }
}
