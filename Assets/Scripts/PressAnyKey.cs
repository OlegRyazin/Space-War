using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PressAnyKey : MonoBehaviour
{
    private static Text PressAnyKeyText;
    private static Text PressAnyKeyTextRestart;
    private bool OneTimePress = false;
    public static bool ReadyToRestart = false;
    void Awake()
    {
        PressAnyKeyText = GameObject.Find("PressAnyKey").GetComponent<Text>();
        PressAnyKeyTextRestart = GameObject.Find("PressAnyKeyRestart").GetComponent<Text>();
        Time.timeScale = 0;
    }
    private void Update()
    {
        if(Input.anyKey && OneTimePress == false)//���� ������� ����� ������� ��� ������ ����
        {
            Time.timeScale = 1;
            PressAnyKeyText.enabled = false;
            OneTimePress = true;
        }
        if (Input.anyKey && ReadyToRestart == true)//���� ������� ����� ������� ��� �������� ����
        {
            Time.timeScale = 1;
            AsteroidsAdnBuffsGenerate.Turbo = 2000;//���������� �������� ���������� � �������� �� ���������
            AsteroidsAdnBuffsGenerate.Bullets = 5;
            AsteroidsAdnBuffsGenerate.Score = 0;
            AsteroidsAdnBuffsGenerate.AsteroidsCount = 0;
            AsteroidsAdnBuffsGenerate.HP = 3;
            AsteroidsAdnBuffsGenerate.LastScore = 0;
            AsteroidsAdnBuffsGenerate.ZObject = 420;
            AsteroidsAdnBuffsGenerate.ZObjectStep = 50;
            AsteroidsAdnBuffsGenerate.ScoreMore100 = false;
            AsteroidsAdnBuffsGenerate.ScoreMore300 = false;
            AsteroidsAdnBuffsGenerate.ScoreMore500 = false;
            AsteroidsAdnBuffsGenerate.AsteroidsAndBuffs = new List<GameObject>();
            string scene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);//������ ��������� ��� �� �����
            PressAnyKeyTextRestart.enabled = false;
            ReadyToRestart = false;
        }
    }
}
