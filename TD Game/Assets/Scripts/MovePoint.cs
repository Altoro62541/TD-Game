using UnityEngine;

public class MovePoint : MonoBehaviour, IMovePoint
{
    public Vector3 SpawnPos => transform.position;
}
