using UnityEngine;
using UnityEngine.AI;

namespace TDGame.UnitEntity.Movement
{
    public class UnitMovement : MonoBehaviour
    {
    private NavMeshAgent _agent;
    [SerializeField] private Transform _target;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _agent.updateUpAxis = false;
        _agent.updateRotation = false;
    }

    private void Start()
    {
        if (_target != null)
        {
            _agent.SetDestination(_target.position);
        }
    }

    private void Update()
    {
        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            Debug.Log("Враг достиг базы!");
            Destroy(gameObject);
        }

        var position = transform.position;
        position.z = 0;
        transform.position = position;

        Vector3 direction = _agent.desiredVelocity;
        if (direction.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    }
    
}
