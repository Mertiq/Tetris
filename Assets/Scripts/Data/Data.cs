using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    private static readonly float cos = Mathf.Cos(Mathf.PI / 2f);
    private static readonly float sin = Mathf.Sin(Mathf.PI / 2f);
    public static readonly float[] RotationMatrix = { cos, sin, -sin, cos };

    public static readonly Dictionary<PieceType, Vector2Int[]> Cells = new()
    {
        {
            PieceType.I,
            new Vector2Int[]
                { new(-1, 1), new(0, 1), new(1, 1), new(2, 1) }
        },
        {
            PieceType.J,
            new Vector2Int[]
                { new(-1, 1), new(-1, 0), new(0, 0), new(1, 0) }
        },
        {
            PieceType.L,
            new Vector2Int[]
                { new(1, 1), new(-1, 0), new(0, 0), new(1, 0) }
        },
        {
            PieceType.O,
            new Vector2Int[]
                { new(0, 1), new(1, 1), new(0, 0), new(1, 0) }
        },
        {
            PieceType.S,
            new Vector2Int[]
                { new(0, 1), new(1, 1), new(-1, 0), new(0, 0) }
        },
        {
            PieceType.T,
            new Vector2Int[]
                { new(0, 1), new(-1, 0), new(0, 0), new(1, 0) }
        },
        {
            PieceType.Z,
            new Vector2Int[]
                { new(-1, 1), new(0, 1), new(0, 0), new(1, 0) }
        },
    };
    
    private static readonly Vector2Int[,] WallKicksI =
    {
        {
            new(0, 0), new(-2, 0), new(1, 0), new(-2, -1),
            new(1, 2)
        },
        {
            new(0, 0), new(2, 0), new(-1, 0), new(2, 1),
            new(-1, -2)
        },
        {
            new(0, 0), new(-1, 0), new(2, 0), new(-1, 2),
            new(2, -1)
        },
        {
            new(0, 0), new(1, 0), new(-2, 0), new(1, -2),
            new(-2, 1)
        },
        {
            new(0, 0), new(2, 0), new(-1, 0), new(2, 1),
            new(-1, -2)
        },
        {
            new(0, 0), new(-2, 0), new(1, 0), new(-2, -1),
            new(1, 2)
        },
        {
            new(0, 0), new(1, 0), new(-2, 0), new(1, -2),
            new(-2, 1)
        },
        {
            new(0, 0), new(-1, 0), new(2, 0), new(-1, 2),
            new(2, -1)
        },
    };
    
    private static readonly Vector2Int[,] WallKicksRest =
    {
        {
            new(0, 0), new(-1, 0), new(-1, 1), new(0, -2),
            new(-1, -2)
        },
        {
            new(0, 0), new(1, 0), new(1, -1), new(0, 2),
            new(1, 2)
        },
        {
            new(0, 0), new(1, 0), new(1, -1), new(0, 2),
            new(1, 2)
        },
        {
            new(0, 0), new(-1, 0), new(-1, 1), new(0, -2),
            new(-1, -2)
        },
        {
            new(0, 0), new(1, 0), new(1, 1), new(0, -2),
            new(1, -2)
        },
        {
            new(0, 0), new(-1, 0), new(-1, -1), new(0, 2),
            new(-1, 2)
        },
        {
            new(0, 0), new(-1, 0), new(-1, -1), new(0, 2),
            new(-1, 2)
        },
        {
            new(0, 0), new(1, 0), new(1, 1), new(0, -2),
            new(1, -2)
        },
    };
    
    public static readonly Dictionary<PieceType, Vector2Int[,]> WallKicks =
        new()
        {
            { PieceType.I, WallKicksI },
            { PieceType.J, WallKicksRest },
            { PieceType.L, WallKicksRest },
            { PieceType.O, WallKicksRest },
            { PieceType.S, WallKicksRest },
            { PieceType.T, WallKicksRest },
            { PieceType.Z, WallKicksRest }
        };
}