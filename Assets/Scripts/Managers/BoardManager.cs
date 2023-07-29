using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    [SerializeField] public Tilemap tilemap;
    [SerializeField] private Piece activePiece;
    [SerializeField] private List<PieceData> pieceDatas;
    [SerializeField] private Vector3Int spawnPosition;
    [SerializeField] private Vector2Int boardSize;
    public RectInt Bounds => new(new Vector2Int(-boardSize.x / 2, -boardSize.y / 2), boardSize);

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        SpawnPiece();
    }

    public void Set(Piece piece)
    {
        foreach (var cell in piece.cells)
            tilemap.SetTile(cell + piece.position, piece.data.tile);
    }

    public void Clear(Piece piece)
    {
        foreach (var cell in piece.cells)
            tilemap.SetTile(cell + piece.position, null);
    }

    public void SpawnPiece()
    {
        var data = pieceDatas[Random.Range(0, pieceDatas.Count)];
        activePiece.Initialize(data, spawnPosition);

        if (Validations.IsPieceMovementValid(activePiece, this, spawnPosition))
            Set(activePiece);
        else
            GameOver();
    }

    public void ClearLines()
    {
        var row = Bounds.yMin;
        while (row < Bounds.yMax)
        { 
            if (IsLineFull(row))
            {
                ScoreManager.Instance.CurrentScore = 10;
                
                for (var i = Bounds.xMin; i < Bounds.xMax; i++)
                    tilemap.SetTile(new Vector3Int(i, row, 0), null);

                var shiftingRow = row;
                
                while (shiftingRow < Bounds.yMax)
                {
                    for (var i = Bounds.xMin; i < Bounds.xMax; i++)
                        tilemap.SetTile(new Vector3Int(i, shiftingRow, 0), tilemap.GetTile(new Vector3Int(i, shiftingRow + 1, 0)));

                    shiftingRow++;
                }
            }
            else
                row++;
        }
    }

    public void GameOver()
    {
        tilemap.ClearAllTiles();
        ScoreManager.Instance.CurrentScore = 0;
    }
    private bool IsLineFull(int row)
    {
        for (var i = Bounds.xMin; i < Bounds.xMax; i++)
        {
            if (!tilemap.HasTile(new Vector3Int(i, row, 0)))
                return false;
        }

        return true;
    }
}