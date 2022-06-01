using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{
    private static Text AsteroidsText;
    public GameObject ExplosionPref;
    void Start()
    {
        AsteroidsText = GameObject.Find("AsteroidsText").GetComponent<Text>();
        Invoke("Suicide", 10);//���� �� 10 ������ ������ �� ������� ������� ���� �� �� �����������
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.z += 50 * Time.deltaTime;
        transform.position = pos;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.name == "Asteroid(Clone)")//���� ����� � �������� �� �������� �������� ���������� ������ ���������� � �����
        {
            AsteroidsAdnBuffsGenerate.AsteroidsCount++;
            AsteroidsText.text = "X" + AsteroidsAdnBuffsGenerate.AsteroidsCount;
            AsteroidsAdnBuffsGenerate.Score += 5;
        }
        GameObject Explosion = Instantiate(ExplosionPref, collision.gameObject.transform.position, Quaternion.identity);//������ ������
        AsteroidsAdnBuffsGenerate.AsteroidsAndBuffs.Remove(collision.gameObject);     
        Destroy(collision.gameObject);
        Destroy(Explosion, 1f);
        Destroy(this.gameObject);//����� ��������� �� ������� ������ ��������
    }
    private void Suicide()
    {
        Destroy(this.gameObject);
    }
}
