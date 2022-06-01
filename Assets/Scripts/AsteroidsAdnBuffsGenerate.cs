using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidsAdnBuffsGenerate : MonoBehaviour
{
    public static int turbo = 2000;
    public static int Turbo//Обновляет значение Турбо на UI при изменении значения переменной
    {
        get
        {
            return turbo;
        }
        set
        {
            turbo = value;
            TurboText.text = "X" + turbo;
        }
    }
    public static int bullets = 5;
    public static int Bullets//Обновляет значение Патронов на UI при изменении значения переменной
    {
        get
        {
            return bullets;
        }
        set
        {
            if (value < 0) value = 0;
            bullets = value;
            BulletsText.text = "X" + bullets;
        }
    }
    public static int HP = 3;
    public static int AsteroidsCount = 0;
    public static int LastScore = 0;
    public static int ZObject = 420;//Значение спавна первого объекта
    public static int ZObjectStep = 50;//Изначальный шаг спавна
    private static int Record;
    private static int score = 0;
    public static int Score//Обновляет значение Счета на UI и изменение сложности игры при изменении значения переменной 
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            AsteroidsAdnBuffsGenerate A = new AsteroidsAdnBuffsGenerate();
            A.ScoreUpdate();
            ScoreText.text = "Score: " + score;
        }
    }
    public static bool ScoreMore100 = false;
    public static bool ScoreMore300 = false;
    public static bool ScoreMore500 = false;
    public GameObject TurboPref;
    public GameObject BulletsPref;
    public GameObject HPPref;
    public GameObject AsteroidPref;
    public GameObject ExplosionPref;
    private static Text ScoreText;
    private static Text RecordScoreText;
    private static Text BulletsText;
    private static Text TurboText;
    private static Text PressAnyKeyTextRestart;
    private static Image[] HPImage = new Image[2];
    public static List<GameObject> AsteroidsAndBuffs = new List<GameObject>();//Тут хранятся все астероиды, патроны, турбо и жизни
    GameObject[] SpawnObjects = new GameObject[4];
    void Start()
    {
        Record = PlayerPrefs.GetInt("SaveRecord");
        ScoreText = GameObject.Find("Score").GetComponent<Text>();
        RecordScoreText = GameObject.Find("Record").GetComponent<Text>();
        RecordScoreText.text = "Record: " + Record;
        BulletsText = GameObject.Find("BulletsText").GetComponent<Text>();
        TurboText = GameObject.Find("TurboText").GetComponent<Text>();
        PressAnyKeyTextRestart = GameObject.Find("PressAnyKeyRestart").GetComponent<Text>();
        HPImage[0] = GameObject.Find("Heart2").GetComponent<Image>();
        HPImage[1] = GameObject.Find("Heart3").GetComponent<Image>();
        SpawnObjects = new GameObject[4] { HPPref, BulletsPref, TurboPref, AsteroidPref };
        for (int i = 50; i < 400; i += 50)//Создание стартовых астероидов
        {
            GameObject AsterOrBuff = Instantiate(SpawnObjects[3], new Vector3(Random.Range(-4, 4), 2f, i), Quaternion.identity);
            AsteroidsAndBuffs.Add(AsterOrBuff);
        }
        InvokeRepeating("ScoreUp", 1f, 1f);//Добавляет по 1 баллу к счету каждую секунду

    }

    void Update()
    {
        if (AsteroidsAndBuffs[0] != null)
            if (transform.position.z > AsteroidsAndBuffs[0].transform.position.z + 15f)
            {//Удаление ненужных объектов
                Destroy(AsteroidsAndBuffs[0]);
                AsteroidsAndBuffs.RemoveAt(0);
            }
        if(transform.position.z + 400f > ZObject)
        {//Спавн новых объектов
            RandomSpawn();
            ZObject += ZObjectStep;
        }

    }
    private void OnTriggerEnter(Collider collision)
    {
        switch (collision.name)
        {
            case "Asteroid(Clone)":
                HP -= 1;
                if (HP == 0) GameOver();//Если кол-во жизней равно 0, то игра проиграна
                HPUpdate();
                GameObject Explosion = Instantiate(ExplosionPref, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(Explosion, 1f);//Взрыв астероида при столкновении с кораблем и удаление взрыва через секунду
                break;
            case "HP(Clone)":
                HP += 1;
                if (HP > 3) HP = 3;//Лимит из 3 жизней
                HPUpdate();
                break;
            case "Bullet(Clone)":
                Bullets += 1;
                break;
            case "OilCan3":
                Turbo += 2000;
                break;
        }
        AsteroidsAndBuffs.Remove(collision.gameObject);
        Destroy(collision.gameObject);
    }

    private void RandomSpawn()//Спавн случайных объектов
    {
        int rand = 0;
        int type = 0;
        rand = Random.Range(0, 20);
        if (rand > 6) type = 3;
        else if (rand > 3) type = 2;
        else if (rand > 0) type = 1;
        else type = 0;
        GameObject AsterOrBuff = Instantiate(SpawnObjects[type], new Vector3(Random.Range(-4, 4), 2f, ZObject), Quaternion.identity);
        AsteroidsAndBuffs.Add(AsterOrBuff);
    }
    void ScoreUpdate()//Повышение сложности игры
    {
        if (LastScore < 100 && Score >= 100)
        {
            ScoreMore100 = true;
            ZObjectStep = 35;
            return;
        }
        if (LastScore < 300 && Score >= 300) 
        {
            ScoreMore300 = true;
            ZObjectStep = 25;
            return;
        }
        if (LastScore < 500 && Score >= 500) 
        {
            ScoreMore500 = true;
            ZObjectStep = 15;
            return;
        }
        LastScore = Score;
    }
    
    private void ScoreUp()//Увеличение счета каждую секунду
    {
        Score++;
    }
    private void HPUpdate()//Изменение кол-во сердец UI
    {
        if (HP == 3)
        {
            HPImage[0].enabled = true;
            HPImage[1].enabled = true;
        }
        if (HP == 2)
        {
            HPImage[0].enabled = true;
            HPImage[1].enabled = false;
        }
        if (HP == 1)
        {
            HPImage[0].enabled = false;
            HPImage[1].enabled = false;
        }
    }
    private void GameOver()//Если кол-во жизней становится равно 0 происходит рестарт по нажатию любой кнопки
    {
        if(Score > Record)//Сохранение рекорда
        {
            PlayerPrefs.SetInt("SaveRecord", Score);
            PlayerPrefs.Save();
        }
        Record = PlayerPrefs.GetInt("SaveRecord"); 
        Time.timeScale = 0;//Остановка времени
        PressAnyKeyTextRestart.enabled = true;
        PressAnyKey.ReadyToRestart = true;
    }
}

