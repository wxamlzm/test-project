using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoldierMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // �ƶ��ٶ�
    public Vector3 targetPosition; // Ŀ��λ��
    private bool isMoving = false; // �Ƿ������ƶ�
    private Vector3 startPosition; // ��¼��ʼ��λ��
    private float totalDistance; // ��¼�ܾ���

    void Start()
    {
        // ��ʼ��Ŀ��λ��Ϊ��ǰλ��
        targetPosition = transform.position;
        startPosition = transform.position;
    }

    void Update()
    {
        // �Ҽ�����ƶ�
        if (Input.GetMouseButtonDown(1)) // 1�����Ҽ�
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ������߼���������
            if (Physics.Raycast(ray, out hit))
            {
                startPosition = transform.position; // ��¼��ʼλ��
                // �����µ�Ŀ��λ��
                targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                totalDistance = Vector3.Distance(startPosition, targetPosition); // �����ܾ���
                isMoving = true;
            }
        }

        // ������ȡ���ƶ�
        if(Input.GetMouseButtonDown(0)) // 0�������
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                // ��������Ƿ��ǵ������
                if(hit.collider.gameObject.name == "Ground3")
                {
                    TryCancelMovement();
                }
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

    void TryCancelMovement()
    {
        if (!isMoving) return; // ���û���ƶ�����ֱ�ӷ���
        // �����Ѿ��ƶ��ľ���ռ�ܾ���ı���
        float currentDistance = Vector3.Distance(startPosition, transform.position);
        float moveProgress = currentDistance / totalDistance;

        // ����ƶ�����һ�룬����ȡ��
        if(moveProgress < 0.5f)
        {
            isMoving = false;
            targetPosition = startPosition;
            Debug.Log("�ƶ���ȡ��");
        }
        else 
        {
            Debug.Log("�Ѿ�����һ����룬�޷�ȡ���ƶ�");
        }
    }
}
