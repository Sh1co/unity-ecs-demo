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
        _filter = _world.Filter<PlayerTag>().Inc<RigidBodyRef>().End();
    }

    public void Run(EcsSystems systems)
    {
        if (Input.GetMouseButtonDown(0))
        {
            _directionIndex = _directionIndex == 0 ? 1 : 0;
        }
        
        
        var rigidBodyRefs = _world.GetPool<RigidBodyRef>();
        
        foreach (var entity in _filter)
        {
            ref RigidBodyRef rigidBodyRef = ref rigidBodyRefs.Get(entity);
            var rb = rigidBodyRef.Rigidbody;
            Vector3 velocity = GetDirection() * _data.PlayerSpeed;
            velocity.y = rb.velocity.y;
            rb.velocity = velocity;
        }
    }
    
    private Vector3 GetDirection()
    {
        return _directionIndex == 0 ? _data.Direction1 : _data.Direction2;
    }

    private Player _player;
    private EcsWorld _world;
    private EcsFilter _filter;
    private int _directionIndex = 0;
    private PlayerMovementData _data;
}