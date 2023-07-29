using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = nameof(PieceData), menuName = nameof(PieceData))]
public class PieceData : ScriptableObject
{
    public PieceType pieceType;
    public Tile tile;

    [HideInInspector] public Vector2Int[] cells;
    public Vector2Int[,] wallKicks;

    public void Initialize()
    {
        cells = Data.Cells[pieceType];
        wallKicks = Data.WallKicks[pieceType];
    }
}