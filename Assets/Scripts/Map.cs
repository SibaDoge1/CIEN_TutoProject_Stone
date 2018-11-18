using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public int Width; //가로 칸수
    public int Height;//세로 칸수
    public int successH;//쌓아야하는 칸(0부터 시작)
    public GameObject[,] map;
    private Text txt;
    // Use this for initialization
    void Awake()
    {
        txt = GameObject.Find("Canvas").transform.Find("Text").GetComponent<Text>();
        map = new GameObject[Width, Height];
    }
    // Update is called once per frame
    void Update()
    {
        printMap();
    }
    public Vector2 roundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public Vector2 rotate(Vector2 vec, float deg)
    {
        Vector2 result;
        result.x = vec.x * Mathf.Cos(Mathf.Deg2Rad * deg) - vec.y * Mathf.Sin(Mathf.Deg2Rad * deg);
        result.y = vec.x * Mathf.Sin(Mathf.Deg2Rad * deg) + vec.y * Mathf.Cos(Mathf.Deg2Rad * deg);
        return result;
    }

    public bool isInside(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < Width && (int)pos.y >= 0);
    }

    public void updateStone(GameObject obj)
    {
        // Remove old children from grid
        for (int y = 0; y < Height; ++y)
            for (int x = 0; x < Width; ++x)
                if (map[x, y] != null)
                    if (map[x, y].transform == obj.transform)
                        map[x, y] = null;
        Vector2 v = roundVec2(obj.transform.position);
        map[(int)v.x, (int)v.y] = obj;
        obj.GetComponent<Stone>().mapPos = v;
    }

    public void deleteStone(GameObject obj)
    {
        for (int y = 0; y < Height; ++y)
            for (int x = 0; x < Width; ++x)
                if (map[x, y] != null)
                    if (map[x, y].transform == obj.transform)
                        map[x, y] = null;
    }

    public void printMap() {
        txt.text = "";
        for (int j = Height-1; j > -1; j--)
        {
            for (int i = 0; i < Width; i++)
            {
                if (map[i, j] == null) txt.text += "0 ";
                else txt.text +=  "1 ";
            }
            txt.text += "\n";
        }
    }
}
