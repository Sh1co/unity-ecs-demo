using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class Startup : MonoBehaviour
{
    [Header("Level Generation data")] 
    [SerializeField] private ForwardPieceTag _fwPiece;
    [SerializeField] private LeftPieceTag _lfPiece;
    [SerializeField] private FinishTriggerTag _finish;
    [SerializeField] private int _piecesCount = 5;
    [SerializeField] private float _minPieceSize = 5f;
    [SerializeField] private float _maxPieceSize = 25f;
    [SerializeField] private Vector3 _piecesShift = new Vector3(1f, 0, -1f);

    void Start()
    {
        LevelData ld = new LevelData(_piecesCount, _minPieceSize, _maxPieceSize, _piecesShift);

        _world = new EcsWorld();
        _systems = new EcsSystems(_world)
            .Add(new LevelBuilding(_fwPiece, _lfPiece, _finish, ld));
        _systems.Init();
    }

    void Update()
    {
        _systems?.Run();
    }

    void OnDestroy()
    {
        if (_systems != null)
        {
            _systems.Destroy();
            _systems = null;
        }

        if (_world != null)
        {
            _world.Destroy();
            _world = null;
        }
    }

    EcsWorld _world;
    EcsSystems _systems;
}