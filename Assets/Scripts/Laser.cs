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
        Invoke("Suicide", 10);//Если за 10 секунд снаряд не поразит никакой цели то он уничтожится
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.z += 50 * Time.deltaTime;
        transform.position = pos;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.name == "Asteroid(Clone)")//Если попал в астероид то изменяем значения количества сбитых астероидов и счета
        {
            AsteroidsAdnBuffsGenerate.AsteroidsCount++;
            AsteroidsText.text = "X" + AsteroidsAdnBuffsGenerate.AsteroidsCount;
            AsteroidsAdnBuffsGenerate.Score += 5;
        }
        GameObject Explosion = Instantiate(ExplosionPref, collision.gameObject.transform.position, Quaternion.identity);//Эффект взрыва
        AsteroidsAdnBuffsGenerate.AsteroidsAndBuffs.Remove(collision.gameObject);     
        Destroy(collision.gameObject);
        Destroy(Explosion, 1f);
        Destroy(this.gameObject);//После попадания по объекту снаряд исчезает
    }
    private void Suicide()
    {
        Destroy(this.gameObject);
    }
}
