using UnityEngine;

public struct LevelData
{
    public LevelData(int piecesCount, float minPieceSize, float maxPieceSize, Vector3 piecesShift)
    {
        PiecesCount = piecesCount;
        MinPieceSize = minPieceSize;
        MaxPieceSize = maxPieceSize;
        PiecesShift = piecesShift;
    }
    
    
    public int PiecesCount;
    public float MinPieceSize;
    public float MaxPieceSize;
    public  Vector3 PiecesShift;
}