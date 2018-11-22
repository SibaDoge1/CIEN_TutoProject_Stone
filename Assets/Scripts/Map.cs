using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public int width; //가로 칸수
    public int height;//세로 칸수
    public int successH;//쌓아야하는 칸(0부터 시작)
	public GameObject[,] map;
    private Text txt;
    // Use this for initialization

    void Awake()
    {
        txt = GameObject.Find("Canvas").transform.Find("Text").GetComponent<Text>();
        map = new GameObject[width, height];
    }
    // Update is called once per frame
    void Update()
    {
        printMap();
        visualizeMap();
    }
    public static Vector2 roundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public static Vector2 rotate(Vector2 vec, float deg)
    {
        Vector2 result;
        result.x = vec.x * Mathf.Cos(Mathf.Deg2Rad * deg) - vec.y * Mathf.Sin(Mathf.Deg2Rad * deg);
        result.y = vec.x * Mathf.Sin(Mathf.Deg2Rad * deg) + vec.y * Mathf.Cos(Mathf.Deg2Rad * deg);
        return result;
    }

    public bool isInside(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < width && (int)pos.y >= 0 && pos.y < height);
    }

    public void updateStone(GameObject obj, Vector2 v)
    {
        Vector2 origin = Map.roundVec2(obj.GetComponent<Stone>().mapPos);
        v = Map.roundVec2(v);
        if (!isInside(v)) return;

        if (isInside(origin) && map[(int)origin.x, (int)origin.y].gameObject == obj)
            map[(int)origin.x, (int)origin.y] = null;
        map[(int)v.x, (int)v.y] = obj;
        Debug.Log(obj.GetComponent<Stone>().mapPos + "->" + v);
        obj.GetComponent<Stone>().mapPos = v;
    }

    public void visualizeMap()
    {
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                if (map[x, y] != null)
                {
                    Vector2 mapPos = map[x, y].transform.GetComponent<Stone>().mapPos;
                    map[x, y].transform.position = new Vector3(mapPos.x, mapPos.y, 0);
                }
            }
        }
    }

    public Vector2 findStone(GameObject obj)
    {
        for (int y = 0; y < height; ++y)
            for (int x = 0; x < width; ++x)
                if (map[x, y] != null)
                    if (map[x, y].transform == obj.transform)
                        return new Vector2(x, y);
        return new Vector2(-1,-1);
    }

    public void deleteStone(GameObject obj)
    {
        for (int y = 0; y < height; ++y)
            for (int x = 0; x < width; ++x)
                if (map[x, y] != null)
                    if (map[x, y].transform == obj.transform)
                        map[x, y] = null;
    }

    public void printMap() {
        txt.text = "";
        for (int j = height-1; j > -1; j--)
        {
            for (int i = 0; i < width; i++)
            {
                if (map[i, j] == null) txt.text += "0 ";
                else txt.text +=  map[i,j].GetComponent<Stone>().myCol + " ";
            }
            txt.text += "\n";
        }
    }
}
