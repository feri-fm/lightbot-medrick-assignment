
using UnityEngine;

public struct BlockRotation
{
    public int value;

    public BlockRotation(int v)
    {
        if (v < 0)
            value = 4 + (v % 4);
        else
            value = v % 4;
    }

    public Vector3Int Rotate(Vector3Int v)
    {
        switch (value)
        {
            case 0: return v;
            case 1: return new Vector3Int(v.z, 0, -v.x);
            case 2: return new Vector3Int(-v.x, 0, -v.z);
            case 3: return new Vector3Int(-v.z, 0, v.x);
        }
        return v;
    }

    public static BlockRotation FromAngle(float angle)
    {
        if (angle < 0) angle += 360;
        var value = Mathf.RoundToInt(angle / 90f);
        return new BlockRotation(value);
    }

    public static readonly BlockRotation forward = new BlockRotation(0);
    public static readonly BlockRotation right = new BlockRotation(1);
    public static readonly BlockRotation back = new BlockRotation(2);
    public static readonly BlockRotation left = new BlockRotation(3);

    public static BlockRotation operator +(BlockRotation left, BlockRotation right)
    {
        return new BlockRotation(left.value + right.value);
    }
    public static BlockRotation operator +(BlockRotation left, int right)
    {
        return new BlockRotation(left.value + right);
    }
    public static BlockRotation operator -(BlockRotation left, int right)
    {
        return new BlockRotation(left.value - right);
    }
}
