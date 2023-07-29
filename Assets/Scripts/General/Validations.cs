using UnityEngine;

public static class Validations
{
    public static bool IsPieceMovementValid(Piece piece, BoardManager boardManager, Vector3Int newPosition)
    {
        foreach (var cell in piece.cells)
        {
            var tilePosition = cell + newPosition;

            if (!boardManager.Bounds.Contains((Vector2Int)tilePosition))
                return false;

            if (boardManager.tilemap.HasTile(tilePosition))
                return false;
        }

        return true;
    }
}