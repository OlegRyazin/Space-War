using UnityEngine;
using System;

public class SpaceshipControl : MonoBehaviour
{
    public GameObject LaserPref;
    public Transform LeftGun;
    public Transform RightGun;
    public float speed = 30;
    public float rollMult = -45;
    float xAxis = 0;
    bool SpaceKeyDown = false;
    float TurboTime = 0;
    private void Start()
    {
        LeftGun = GameObject.Find("LeftGun").GetComponent<Transform>();
        RightGun = GameObject.Find("RightGun").GetComponent<Transform>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))//Определяет нажат ли пробел
        {
            SpaceKeyDown = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SpaceKeyDown = false;
        }
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && AsteroidsAdnBuffsGenerate.Bullets != 0)
        {
            GameObject LeftShot = Instantiate(LaserPref, LeftGun.position, Quaternion.identity);
            LeftShot.transform.Rotate(new Vector3(90, 0, 0));
            GameObject RightShot = Instantiate(LaserPref, RightGun.position, Quaternion.identity);
            RightShot.transform.Rotate(new Vector3(90, 0, 0));
            AsteroidsAdnBuffsGenerate.Bullets--;
        }
        xAxis += Input.GetAxis("Horizontal")/100;
        if (xAxis > 1f)// Лимит скорости
        {
            xAxis = 1f;
        }
        if (xAxis < -1f)
        {
            xAxis = -1f;
        }
        if (Input.GetAxis("Horizontal") == 0 && xAxis != 0) // Плавная остановка корабля если не нажата клавиша
        {
            if (xAxis > 0)
            {
                xAxis -= 0.003f;
            }
            else
            {
                xAxis += 0.003f;
            }
        }
        Vector3 pos = transform.position;
        pos.x += xAxis * speed/3 * Time.deltaTime;
        if (SpaceKeyDown && AsteroidsAdnBuffsGenerate.Turbo > 0)//Если пробел зажат то скорость и очки в секунду увеличиваются в 2 раза
        {
            pos.z += 2 * speed * Time.deltaTime;
            AsteroidsAdnBuffsGenerate.Turbo -= Convert.ToInt32(500 * Time.deltaTime);
            TurboTime += Time.deltaTime;
            if (TurboTime >= 1f)
            {
                AsteroidsAdnBuffsGenerate.Score++;
                TurboTime = 0;
            }
        }
        else
        {
            pos.z += speed * Time.deltaTime;
        }
        if (pos.x > 4.5)// границы полета
        {
            pos.x = 4.5f;
        }
        if (pos.x < -4.5)
        {
            pos.x = -4.5f;
        }
        transform.position = pos;
        transform.rotation = Quaternion.Euler(0, 0, xAxis * rollMult);
    }
    private void ScoreUp2()//Увеличение очков в секунду при Турбо
    {
        AsteroidsAdnBuffsGenerate.Score++;
    }
}
