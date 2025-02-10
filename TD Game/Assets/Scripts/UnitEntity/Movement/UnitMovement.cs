using UnityEngine;
using UnityEngine.AI;

namespace TDGame.UnitEntity.Movement
{
    public class UnitMovement : MonoBehaviour, IEnabledComponents
    {
    private NavMeshAgent _agent;
    [SerializeField] private Transform _target;
    private Transform _cachedTransform;

    public bool Enabled
    {
        get => enabled;
        set => enabled = value;
    }

    private void Awake()
    {
        _cachedTransform = transform;
        _agent = GetComponent<NavMeshAgent>();

        if (_agent == null)
        {
            Debug.LogError("NavMeshAgent component is missing on " + gameObject.name);
            enabled = false;
            return;
        }

        _agent.updateUpAxis = false;
        _agent.updateRotation = false;
    }

    private void Start()
    {
        if (_target != null)
        {
            _agent.SetDestination(_target.position);
        }
        else
        {
            Debug.LogWarning("Target is not assigned for UnitMovement on " + gameObject.name);
        }
    }

    private void Update()
    {
        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            Debug.Log("Враг достиг базы!");
            Destroy(gameObject);
            return;
        }

        Vector3 position = _cachedTransform.position;
        if (!Mathf.Approximately(position.z, 0f))
        {
            position.z = 0;
            _cachedTransform.position = position;
        }

        Vector3 desiredVelocity = _agent.desiredVelocity;
        if (desiredVelocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(desiredVelocity.y, desiredVelocity.x) * Mathf.Rad2Deg;
            _cachedTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    
}
}