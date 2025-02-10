using UniRx;
using UnityEngine;
namespace TDGame.GO
{
    public class TowerPlace : MonoBehaviour
    {
    private readonly ReactiveProperty<bool> _isOccupied = new ReactiveProperty<bool>(false);

    public IReadOnlyReactiveProperty<bool> IsOccupied => _isOccupied;

    public void SetOccupied(bool occupied)
    {
        _isOccupied.Value = occupied;
    }
    }
}

