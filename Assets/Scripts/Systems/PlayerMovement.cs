using Leopotam.EcsLite;
using UnityEngine;

public class PlayerMovement : IEcsRunSystem, IEcsInitSystem
{
    public PlayerMovement(Player player, EcsWorld world, PlayerMovementData data)
    {
        _player = player;
        _world = world;
        _data = data;
    }
    
    public void Init(EcsSystems systems)
    {
        _player.CreateEntity(_world);
        _rigidBodyFilter = _world.Filter<PlayerTag>().Inc<RigidBodyRef>().End();
        _rigidBodyRefs = _world.GetPool<RigidBodyRef>();
        _finishFilter = _world.Filter<FinishTag>().End();
    }

    public void Run(EcsSystems systems)
    {
        if (Input.GetMouseButtonDown(0))
        {
            _directionIndex = _directionIndex == 0 ? 1 : 0;
        }

        

        foreach (var entity in _rigidBodyFilter)
        {
            ref RigidBodyRef rigidBodyRef = ref _rigidBodyRefs.Get(entity);
            var rb = rigidBodyRef.Rigidbody;
            Vector3 velocity = GetDirection() * _data.PlayerSpeed;
            velocity.y = rb.velocity.y;
            rb.velocity = velocity;
        }

        var _finishTags = _world.GetPool<FinishTag>();
        
        foreach (var entity in _finishFilter)
        {
            ref FinishTag finishTag = ref _finishTags.Get(entity);
            Object.Destroy(finishTag.Player);
            _world.DelEntity(entity);
        }
    }
    
    private Vector3 GetDirection()
    {
        return _directionIndex == 0 ? _data.Direction1 : _data.Direction2;
    }

    private Player _player;
    private EcsWorld _world;
    private EcsFilter _rigidBodyFilter;
    private EcsFilter _finishFilter;
    private EcsPool<RigidBodyRef> _rigidBodyRefs;
    private int _directionIndex = 0;
    private PlayerMovementData _data;
}