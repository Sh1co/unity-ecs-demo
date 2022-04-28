using System;
using Leopotam.EcsLite;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 6;
    [SerializeField] private Vector3 Direction1 = Vector3.forward;
    [SerializeField] private Vector3 Direction2 = Vector3.left;
    

    public void CreateEntity(EcsWorld world)
    {
        _world = world;
        _entity = world.NewEntity();
        var playerTags = world.GetPool<PlayerTag>();
        var rigidBodyRefs = world.GetPool<RigidBodyRef>();
        playerTags.Add(_entity);
        ref var transformRef = ref rigidBodyRefs.Add(_entity);
        transformRef.Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            var finishTags = _world.GetPool<FinishTag>();
            ref var finishTag = ref finishTags.Add(_entity);
            finishTag.Player = gameObject;
        }
    }

    private EcsWorld _world;
    private int _entity;
}