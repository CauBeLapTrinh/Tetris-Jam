using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static readonly Vector2Int Size = new(10, 20);
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Transform cellsTransform;

    private readonly Cell[,] cells = new Cell[Size.y, Size.x];
    private readonly int[,] data = new int[Size.y, Size.x];

    private int tetrominoIndex;
    private Vector2Int piecePoint;
    private int pieceRotationIndex;
    public float dropTime = 0.8f;
    private float pieceDropTime = 0f;

    public readonly List<int> fullRows = new();

    private void Start()
    {
        for (int r = 0; r < Size.y; r++)
        {
            for (int c = 0; c < Size.x; c++)
            {
                cells[r, c] = Instantiate(cellPrefab, cellsTransform);
                cells[r, c].transform.position = new Vector3Int(c, r, 0);
                cells[r, c].Hide();
            }
        }

        SpawnPiece();
    }

    private void Update()
    {
        pieceDropTime += Time.deltaTime;
        if (pieceDropTime >= dropTime)
        {
            pieceDropTime = 0;

            if (!DropPiece())
            {
                LockPiece();
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            MovePiece(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MovePiece(Vector2Int.right);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MovePiece(Vector2Int.down);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            RotatePiece();
        }
    }

    private void SpawnPiece()
    {
        tetrominoIndex = Random.Range(0, Tetrominoes.Length);

        piecePoint = new(3, 17);
        pieceRotationIndex = 0;

        pieceDropTime = 0f;

        ShowPiece();
    }

    private void MovePiece(Vector2Int direction)
    {
        var point = piecePoint + direction;
        if (!IsValidPiece(point, pieceRotationIndex)) return;

        HidePiece();
        piecePoint = point;
        ShowPiece();
    }

    private void ShowPiece()
    {
        var tetromino = Tetrominoes.Get(tetrominoIndex, pieceRotationIndex);
        var pieceColor = Tetrominoes.Colors[tetrominoIndex];

        foreach (var p in tetromino)
        {
            cells[piecePoint.y + p.y, piecePoint.x + p.x].Show(pieceColor);
        }
    }

    private void RotatePiece()
    {
        var rotationIndex = (pieceRotationIndex + 1) % 4;
        if (!IsValidPiece(piecePoint, rotationIndex)) return;

        HidePiece();
        pieceRotationIndex = rotationIndex;
        ShowPiece();
    }

    private void HidePiece()
    {
        var tetromino = Tetrominoes.Get(tetrominoIndex, pieceRotationIndex);

        foreach (var p in tetromino)
        {
            cells[piecePoint.y + p.y, piecePoint.x + p.x].Hide();
        }
    }

    public bool DropPiece()
    {
        var point = piecePoint + Vector2Int.down;
        if (!IsValidPiece(point, pieceRotationIndex)) return false;

        HidePiece();
        piecePoint = point;
        ShowPiece();

        return true;
    }

    public void LockPiece()
    {
        var tetromino = Tetrominoes.Get(tetrominoIndex, pieceRotationIndex);

        foreach (var p in tetromino)
        {
            var lockPoint = piecePoint + p;
            data[lockPoint.y, lockPoint.x] = 1;
            cells[lockPoint.y, lockPoint.x].Show(Color.white);
        }

        ClearFullRows();
        SpawnPiece();
    }

    private bool IsValidPiece(Vector2Int point, int rotationIndex)
    {
        var tetromino = Tetrominoes.Get(tetrominoIndex, rotationIndex);

        foreach (var p in tetromino)
        {
            if (!IsValidPoint(point + p))
            {
                return false;
            }
        }

        return true;
    }

    private bool IsValidPoint(Vector2Int point)
    {
        if (point.x < 0 || Size.x <= point.x) return false;
        if (point.y < 0 || Size.y <= point.y) return false;
        if (data[point.y, point.x] > 0) return false;

        return true;
    }

    private void ClearFullRows()
    {
        FullRows();
        if (fullRows.Count > 0)
        {
            foreach (var r in fullRows)
            {
                for (var c = 0; c < Size.x; c++)
                {
                    data[r, c] = 0;
                    cells[r, c].Hide();
                }
            }

            for (int i = 0; i < fullRows.Count - 1; i++)
            {
                for (var r = fullRows[i] + 1; r < fullRows[i + 1]; r++)
                {
                    DropRow(r, i + 1);
                }
            }

            for (var r = fullRows[^1] + 1; r < Size.y; r++)
            {
                DropRow(r, fullRows.Count);
            }
        }
    }

    private void DropRow(int row, int dropCount)
    {
        for (var c = 0; c < Size.x; c++)
        {
            if (data[row, c] > 0)
            {
                data[row - dropCount, c] = data[row, c];
                cells[row - dropCount, c].Show(Color.white);

                data[row, c] = 0;
                cells[row, c].Hide();
            }
        }
    }

    private void FullRows()
    {
        fullRows.Clear();
        var fromRow = Mathf.Max(0, piecePoint.y);
        var toRowExclusive = Mathf.Min(piecePoint.y + 4, Size.y);

        for (var r = fromRow; r < toRowExclusive; r++)
        {
            var isFullRow = true;
            for (var c = 0; c < Size.x; c++)
            {
                if (data[r, c] == 0)
                {
                    isFullRow = false;
                    break;
                }
            }

            if (isFullRow)
            {
                fullRows.Add(r);
            }
        }
    }
}