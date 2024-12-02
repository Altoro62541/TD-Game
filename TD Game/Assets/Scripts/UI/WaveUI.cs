using TMPro;
using Unity.VisualScripting;
using UnityEngine;
namespace TDGame.UI
{
    public class WaveUI : MonoBehaviour
    {
    private TextMeshProUGUI _waveText;
    private void Start()
    {
        _waveText = gameObject.GetComponent<TextMeshProUGUI>();
    }
    public void UpdateWave(int currentWave, int totalWaves)
    {
        _waveText.text = $"Wave {currentWave}/{totalWaves}";
    }
    }
}

