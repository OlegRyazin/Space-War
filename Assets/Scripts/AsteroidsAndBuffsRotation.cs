using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsAndBuffsRotation : MonoBehaviour
{
    int RotationXRand = 0;
    int RotationYRand = 0;
    int RotationZRand = 0;

    void Start()
    {
        RotationXRand = Random.Range(-30, 30);
        RotationYRand = Random.Range(-30, 30);
        RotationZRand = Random.Range(-30, 30);
    }

    void Update()
    {//Вращение объектов в случайные стороны с случайной скоростью
        transform.Rotate(new Vector3(RotationXRand, RotationYRand, RotationZRand) * Time.deltaTime);
    }
}
