using UnityEngine;

namespace _Scripts.MonoBehaviours.UI
{
    public class GameUI : MonoBehaviour
    {
        private bool _isPauseEnabled;
        
        public delegate void SetPauseState(bool isPaused);

        public event SetPauseState OnSetPauseState;
        
        public void SetPause()
        {
            _isPauseEnabled = !_isPauseEnabled;
            OnSetPauseState?.Invoke(_isPauseEnabled);
        }
    }
}