using TMPro;
using UnityEngine;
namespace TDGame.UI
{
    public class WaveUI : MonoBehaviour
    {
    [SerializeField] private TextMeshProUGUI _waveText;
    public void UpdateWave(int currentWave, int totalWaves)
    {
        if (_waveText == null)
        {
            Debug.LogError("WaveText is not assigned!");
            return;
        }

        _waveText.text = $"Wave: {currentWave} / {totalWaves}";
    }
    }
}

