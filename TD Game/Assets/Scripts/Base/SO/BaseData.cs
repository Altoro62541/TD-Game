using UnityEngine;
using Sirenix.OdinInspector;
namespace TDGame.BaseSpace.SO
{
    [CreateAssetMenu(menuName = "Base/New Data")]
    public class BaseData : SerializedScriptableObject
    {
        [SerializeField] private float _health;

        public float Health => _health;
    }
}

