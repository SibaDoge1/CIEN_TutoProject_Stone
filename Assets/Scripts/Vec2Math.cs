using UnityEngine;

public static class Vec2Math
{

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
}
