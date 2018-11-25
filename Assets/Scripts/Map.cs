using UnityEngine;

public class Map : MonoBehaviour
{

    public int width; //가로 칸수
    public int height;//세로 칸수
	private Stone[,] map;
    private static Map instance = null;

    void Awake()
    {
        map = new Stone[width, height];
    }

    void Update()
    {
        if (instance == null)
        {
            instance = this;
        }
        visualizeMap();
    }

    public static Map Instance
    {
        get
        {
            return instance;
        }
    }

    public bool isInside(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < width && (int)pos.y >= 0 && pos.y < height);
    }

    public void updateStone(Stone stone, Vector2 v)
    {
        Vector2 origin = Vec2Math.roundVec2(stone.mapPos);
        v = Vec2Math.roundVec2(v);
        if (!isInside(v)) return;

        if (isInside(origin) && map[(int)origin.x, (int)origin.y] == stone)
            map[(int)origin.x, (int)origin.y] = null;
        map[(int)v.x, (int)v.y] = stone;
        stone.GetComponent<Stone>().mapPos = v;
    }



    public void deleteStone(Stone stone)
    {
        for (int y = 0; y < height; ++y)
            for (int x = 0; x < width; ++x)
                if (map[x, y] != null)
                    if (map[x, y].transform == stone.transform)
                        map[x, y] = null;
    }

    public Stone getStone(Vector2 vec)
    {
        vec = Vec2Math.roundVec2(vec);
        if (!isInside(vec) || map[(int)vec.x, (int)vec.y] == null) return null;
        return map[(int)vec.x, (int)vec.y];
    }

    private void visualizeMap()
    {
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                if (map[x, y] != null)
                {
                    Vector2 mapPos = map[x, y].mapPos;
                    map[x, y].transform.position = new Vector3(mapPos.x, mapPos.y, 0);
                }
            }
        }
    }
}
