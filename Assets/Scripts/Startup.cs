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
    [Header("Player Movement data")]
    [SerializeField] private float _playerSpeed = 6.0f;
    [SerializeField] private Vector3 Direction1 = Vector3.forward;
    [SerializeField] private Vector3 Direction2 = Vector3.left;
    [SerializeField] private Player _player;

    void Start()
    {
        LevelData levelData = new LevelData(_piecesCount, _minPieceSize, _maxPieceSize, _piecesShift);
        PlayerMovementData playerMovementData = new PlayerMovementData(_playerSpeed, Direction1, Direction2);

        _world = new EcsWorld();
        _systems = new EcsSystems(_world)
            .Add(new LevelBuilding(_fwPiece, _lfPiece, _finish, levelData))
            .Add(new PlayerMovement(_player, _world, playerMovementData));
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