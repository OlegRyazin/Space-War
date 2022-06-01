using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public Transform SpaceShip;
    void Start()
    {
        SpaceShip = GameObject.Find("Spaceship").GetComponent<Transform>();
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, SpaceShip.position.z + 1000f);//������� ������ ��������� �� ��������� 1000 �� ������� ������
        transform.Rotate(new Vector3(2, 2, 2) * Time.deltaTime);//�������� ������� ��� �������
    }
}
