using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{

    public int mapHeight;
    public int mapWidth;
    public GameObject[] tiles = new GameObject[8];  // El array con los bloques que amos ausar para crear el nivel.

    public GameObject mapTile;
    public GameObject[,] mapArray;
    private int x, y = 0;


    /*void Start()
    {
        Debug.Log("Start loading map");
        LoadMap(1);
    }*/


    void LoadMap(int numLevel)
    {
        string input = File.ReadAllText(Application.dataPath + "/Resources/Levels/Level"+numLevel.ToString()+".txt");

        GetMapSize(input);

        if (mapHeight > 0 && mapWidth > 0)
        {
            mapArray = new GameObject[mapHeight, mapWidth];
            x = 0;
            y = 0;

            foreach (string row in input.Split('\n'))
            {
                x = 0;
                if (y >= 2)
                {
                    //Debug.Log("x: " + x.ToString() + " y: " + y.ToString() + " row: " + row);
                    ReadRowNumber(row);
                }
                y++;
            }
        }

    }

    private void GetMapSize(string input)
    {
        string[] f = input.Split(new string[] { "\n", "\r", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

        /*for (int i = 0; i < f.Length; i++)
        {
            Debug.Log("GetMapSize("+i.ToString()+")-> " + f[i]);
        }*/

        if (f[0].Length > 0 && f[1].Length > 0)
        {
            int.TryParse(f[0], out mapHeight);  //mapHeight = f[0];
            int.TryParse(f[1], out mapWidth);  //mapWidth = f[1];
        }
    }

    private void ReadRowNumber(string row)
    {
        Vector2 start = new Vector2(-2, 4);
        Vector2 pos = Vector2.zero;

        foreach (string col in row.Split(' '))
        {
            int tileNumber = int.Parse(col.Trim());

            pos.x = start.x + x;
            pos.y = start.y - (y - 2) * 0.5f;

            if(y >= 2 && tileNumber >= 1 && tileNumber <= 5)
            {
                mapTile = tiles[tileNumber];
                mapArray[y - 2, x] = mapTile;
                GameObject obj = Instantiate(mapTile, pos, Quaternion.identity);
                obj.transform.parent = ArkanoidManager.Instance.blockContainer.transform;
            }

            x++;
        }
    }

}
