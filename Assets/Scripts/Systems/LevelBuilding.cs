using Leopotam.EcsLite;
using UnityEditorInternal;
using UnityEngine;

public class LevelBuilding : IEcsInitSystem
{
    public LevelBuilding(ForwardPieceTag fwPiece, LeftPieceTag lfPiece, FinishTriggerTag finish, LevelData data)
    {
        _fwPiece = fwPiece;
        _lfPiece = lfPiece;
        _finish = finish;
        _data = data;
    }
    
    
    public void Init(EcsSystems systems)
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
         bool dir1 = true;
            Vector3 startPoint = new Vector3(0, -1, 0);
            for (int i = 0; i < _data.PiecesCount; i++)
            {
                float pieceSize = Random.Range(_data.MinPieceSize, _data.MaxPieceSize);
                var coinDistance = Random.Range((-pieceSize / 2) + 0.5f, (pieceSize / 2) - 0.5f);
                
                if (dir1)
                {
                    var piece = GameObject.Instantiate(_fwPiece.gameObject);
                    var newScale = piece.transform.localScale;
                    newScale.z = pieceSize;
                    piece.transform.localScale = newScale;
                    
                    startPoint.z += pieceSize / 2;
                    piece.transform.position = startPoint;

                    // var coinPos = startPoint;
                    // coinPos.z += coinDistance;
                    // coinPos.y = 0;
                    // var coin = GameObject.Instantiate(_coin, coinPos, _coin.transform.rotation);
                    // coin.transform.SetParent(_level.transform);
                    
                    startPoint.z += pieceSize / 2;
                    startPoint += _data.PiecesShift;
                }
                else
                {
                    var piece = GameObject.Instantiate(_lfPiece.gameObject);
                    var newScale = piece.transform.localScale;
                    newScale.z = pieceSize;
                    piece.transform.localScale = newScale;
                    
                    startPoint.x -= pieceSize / 2;
                    piece.transform.position = startPoint;
                    
                    // var coinPos = startPoint;
                    // coinPos.x -= coinDistance;
                    // coinPos.y = 0;
                    // var coin = GameObject.Instantiate(_coin, coinPos, _coin.transform.rotation);
                    // coin.transform.SetParent(_level.transform);
                    
                    startPoint.x -= pieceSize / 2;
                    startPoint += _data.PiecesShift;
                }
                dir1 = !dir1;
            }

            GameObject.Instantiate(_finish.gameObject, startPoint - _data.PiecesShift, Quaternion.identity);
    }
    
    private ForwardPieceTag _fwPiece;
    private LeftPieceTag _lfPiece;
    private FinishTriggerTag _finish;
    private LevelData _data;
}