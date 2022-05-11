using UnityEngine;

public struct PlayerMovementData
{
    public PlayerMovementData(float playerSpeed, Vector3 direction1, Vector3 direction2)
    {
        PlayerSpeed = playerSpeed;
        Direction1 = direction1;
        Direction2 = direction2;
    }
    
    public float PlayerSpeed;
    public Vector3 Direction1;
    public Vector3 Direction2;
}