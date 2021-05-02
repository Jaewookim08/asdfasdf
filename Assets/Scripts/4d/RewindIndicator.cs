using UnityEngine;

namespace _4d
{
    public class RewindIndicator: MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _targetComponent;

        private void Update()
        {
            _targetComponent.enabled = TimeRunningState.IsRewindOn;
        }

    }
}