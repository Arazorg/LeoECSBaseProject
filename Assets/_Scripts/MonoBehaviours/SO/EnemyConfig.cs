using UnityEngine;

namespace _Scripts.MonoBehaviours.SO
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemies/New Enemy Data")]
    public class EnemyConfig: ScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        
        public GameObject Prefab => _prefab;
    }
}