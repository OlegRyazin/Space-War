using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGeneration : MonoBehaviour
{
    public float speed = 30;
    public int CountOfTiles = 20;
    public GameObject Tile;
    List<GameObject> tiles = new List<GameObject>();//Тут хранятся все участки дороги
    void Start()
    {
        for (int i = 0; i < CountOfTiles; i++)//Создание стартовой дороги
        {
            GameObject TL = Instantiate(Tile, new Vector3(0f, 0f, -10f + 10 * i), Quaternion.identity);
            tiles.Add(TL);
        }
    }
    void Update()
    {
        if(transform.position.z > tiles[0].transform.position.z + 15f)//Удаление ненужных участков дороги и создание новых
        {
            Destroy(tiles[0]);
            tiles.RemoveAt(0);
            GameObject TL = Instantiate(Tile, new Vector3(0f, 0f, tiles[0].transform.position.z + 10f * (CountOfTiles - 1)), Quaternion.identity);
            tiles.Add(TL);
        }
    }
}
