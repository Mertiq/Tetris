using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] private float stepDelay = 1f;
    [SerializeField] private float lockDelay = .5f;
    [SerializeField] private float moveDelay = .1f;

    [HideInInspector] public PieceData data;
    [HideInInspector] public Vector3Int[] cells;
    [HideInInspector] public Vector3Int position;

    private int rotationIndex;
    private float stepTimer;
    private float lockTimer;
    private float moveTimer;

    private bool CanStep() => Time.time > stepTimer;
    private bool CanMove() => Time.time > moveTimer;
    private bool CanLocked() => lockTimer >= lockDelay;

    public void Initialize(PieceData data, Vector3Int position)
    {
        this.data = data;
        this.position = position;
        rotationIndex = 0;
        lockTimer = 0;
        stepTimer = Time.time + stepDelay;
        moveTimer = Time.time + moveDelay;

        data.Initialize();

        cells = new Vector3Int[data.cells.Length];

        for (var i = 0; i < cells.Length; i++)
            cells[i] = (Vector3Int)data.cells[i];
    }

    private void Update()
    {
        BoardManager.Instance.Clear(this);

        IncreaseLockTimer();

        if (Input.GetKeyDown(KeyCode.Q))
            Rotate(-1);

        if (Input.GetKeyDown(KeyCode.E))
            Rotate(1);

        if (Input.GetKeyDown(KeyCode.Space))
            HardDrop();

        if (CanMove())
        {
            if (Input.GetKeyDown(KeyCode.A))
                Move(Vector2Int.left);

            if (Input.GetKeyDown(KeyCode.D))
                Move(Vector2Int.right);

            if (Input.GetKeyDown(KeyCode.S))
            {
                if (Move(Vector2Int.down))
                    stepTimer += Time.time + stepDelay;
            }
        }

        if (CanStep())
            Step();

        BoardManager.Instance.Set(this);
    }

    private bool Move(Vector2Int translation)
    {
        var newPosition = new Vector3Int(position.x + translation.x, position.y + translation.y, position.z);

        var isMovementValid = Validations.IsPieceMovementValid(this, BoardManager.Instance, newPosition);

        if (isMovementValid)
        {
            position = newPosition;
            moveTimer = Time.time + moveDelay;
            ResetLockTimer();
        }

        return isMovementValid;
    }

    private void Rotate(int direction)
    {
        var startRotationIndex = rotationIndex;

        rotationIndex = Extensions.Wrap(rotationIndex + direction, 0, 4);

        ApplyRotationMatrix(direction);

        if (!HasWallKicks(rotationIndex, direction)) return;

        rotationIndex = startRotationIndex;
        ApplyRotationMatrix(-direction);
    }

    private void ApplyRotationMatrix(int direction)
    {
        for (var i = 0; i < cells.Length; i++)
        {
            Vector3 cell = cells[i];

            int x, y;

            if (data.pieceType is PieceType.I or PieceType.O)
            {
                x = Mathf.CeilToInt(RotateCoordinate(cell.x, cell.y, direction));
                y = Mathf.CeilToInt(RotateCoordinate(cell.x, cell.y, direction, 2));
            }
            else
            {
                x = Mathf.RoundToInt(RotateCoordinate(cell.x, cell.y, direction));
                y = Mathf.RoundToInt(RotateCoordinate(cell.x, cell.y, direction, 2));
            }


            cells[i] = new Vector3Int(x, y, 0);
        }
    }

    private float RotateCoordinate(float x, float y, int direction, int index = 0)
    {
        var rotationMatrix = Data.RotationMatrix;
        var result = x * rotationMatrix[index] * direction + y * rotationMatrix[index + 1] * direction;

        if (data.pieceType is PieceType.I or PieceType.O)
            result -= 0.5f;

        return result;
    }

    private void Step()
    {
        stepTimer = Time.time + stepDelay;

        Move(Vector2Int.down);

        if (CanLocked()) Lock();
    }

    private void HardDrop()
    {
        while (Move(Vector2Int.down))
        {
        }

        Lock();
    }

    private void Lock()
    {
        BoardManager.Instance.Set(this);
        BoardManager.Instance.ClearLines();
        BoardManager.Instance.SpawnPiece();
    }

    private bool HasWallKicks(int rotationIndex, int rotationDirection)
    {
        var wallKickIndex = rotationIndex * 2;

        if (rotationDirection > 0)
            wallKickIndex--;

        wallKickIndex = Extensions.Wrap(wallKickIndex, 0, data.wallKicks.GetLength(0));

        for (var i = 0; i < data.wallKicks.GetLength(1); i++)
        {
            var translation = data.wallKicks[wallKickIndex, i];

            if (Move(translation))
                return false;
        }

        return true;
    }

    private void ResetLockTimer() => lockTimer = 0;
    private void IncreaseLockTimer() => lockTimer += Time.deltaTime;
}