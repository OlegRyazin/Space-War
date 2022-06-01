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
        transform.position = new Vector3(transform.position.x, transform.position.y, SpaceShip.position.z + 1000f);//Планета всегда находится на растоянии 1000 от корабля игрока
        transform.Rotate(new Vector3(2, 2, 2) * Time.deltaTime);//Вращение планеты для красоты
    }
}
