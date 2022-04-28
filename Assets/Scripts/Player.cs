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
        var entity = world.NewEntity();
        var playerTags = world.GetPool<PlayerTag>();
        var rigidBodyRefs = world.GetPool<RigidBodyRef>();
        playerTags.Add(entity);
        ref var transformRef = ref rigidBodyRefs.Add(entity);
        transformRef.Rigidbody = GetComponent<Rigidbody>();
    }
    

}