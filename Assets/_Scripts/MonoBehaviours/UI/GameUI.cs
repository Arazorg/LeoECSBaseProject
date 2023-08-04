using TMPro;
using UnityEngine;

namespace _Scripts.MonoBehaviours.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeText;
        
        private bool _isPauseEnabled;
        
        public delegate void SetPauseState(bool isPaused);

        public event SetPauseState OnSetPauseState;
        
        public void SetPause()
        {
            _isPauseEnabled = !_isPauseEnabled;
            OnSetPauseState?.Invoke(_isPauseEnabled);
        }

        public void SetTimeText(string time)
        {
            _timeText.text = time;
        }
    }
}