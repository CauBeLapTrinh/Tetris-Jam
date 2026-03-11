using UnityEngine;

public static class Tetrominoes
{
    private static readonly Vector2Int[][][] tetrominoes = new Vector2Int[][][]
    {
        // I
        new Vector2Int[][]
        {
            new Vector2Int[] {new(0,2), new(1,2), new(2,2),new(3,2)},
            new Vector2Int[] {new(2,0), new(2,1), new(2,2),new(2,3)},
            new Vector2Int[] {new(0,1), new(1,1), new(2,1),new(3,1)},
            new Vector2Int[] {new(1,0), new(1,1), new(1,2),new(1,3)},
        },

        // J
        new Vector2Int[][]
        {
            new Vector2Int[] {new(0,1), new(1,1), new(2,1),new(0,2)},
            new Vector2Int[] {new(1,0), new(1,1), new(1,2),new(2,2)},
            new Vector2Int[] {new(2,0), new(0,1), new(1,1),new(2,1)},
            new Vector2Int[] {new(0,0), new(1,0), new(1,1),new(1,2)},
        },

        // L
        new Vector2Int[][]
        {
            new Vector2Int[] {new(0,1), new(1,1), new(2,1),new(2,2)},
            new Vector2Int[] {new(1,0), new(2,0), new(1,1),new(1,2)},
            new Vector2Int[] {new(0,0), new(0,1), new(1,1),new(2,1)},
            new Vector2Int[] {new(1,0), new(1,1), new(0,2),new(1,2)},
        },

        // O
        new Vector2Int[][]
        {
            new Vector2Int[] {new(1,1), new(2,1), new(1,2),new(2,2)},
            new Vector2Int[] {new(1,1), new(2,1), new(1,2),new(2,2)},
            new Vector2Int[] {new(1,1), new(2,1), new(1,2),new(2,2)},
            new Vector2Int[] {new(1,1), new(2,1), new(1,2),new(2,2)},
        },

        // S
        new Vector2Int[][]
        {
            new Vector2Int[] {new(0,1), new(1,1), new(1,2),new(2,2)},
            new Vector2Int[] {new(2,0), new(1,1), new(2,1),new(1,2)},
            new Vector2Int[] {new(0,0), new(1,0), new(1,1),new(2,1)},
            new Vector2Int[] {new(1,0), new(0,1), new(1,1),new(0,2)},
        },

        // T
        new Vector2Int[][]
        {
            new Vector2Int[] {new(0,1), new(1,1), new(2,1),new(1,2)},
            new Vector2Int[] {new(1,0), new(1,1), new(2,1),new(1,2)},
            new Vector2Int[] {new(1,0), new(0,1), new(1,1),new(2,1)},
            new Vector2Int[] {new(1,0), new(0,1), new(1,1),new(1,2)},
        },

        // Z
        new Vector2Int[][]
        {
            new Vector2Int[] {new(1,1), new(2,1), new(0,2),new(1,2)},
            new Vector2Int[] {new(1,0), new(1,1), new(2,1),new(2,2)},
            new Vector2Int[] {new(1,0), new(2,0), new(0,1),new(1,1)},
            new Vector2Int[] {new(0,0), new(0,1), new(1,1),new(1,2)},
        },
    };

    public static readonly Color[] Colors = new Color[]
    {
        new (0f,.84f,.96f),
        new (0.14f,0.4f,1f),
        new (1f,0.54f,0f),
        new (1f,0.88f,0f),
        new (0f,.83f,0f),
        new (0.77f,0.27f,0.87f),
        new (1f,0.22f,0.4f),
    };

    public static Vector2Int[] Get(int index, int rotationIndex) => tetrominoes[index][rotationIndex];
    public static int Length => tetrominoes.Length;
}