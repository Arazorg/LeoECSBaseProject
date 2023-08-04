using System.Collections;
using UnityEngine;

namespace _Scripts.MonoBehaviours.Coroutines
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator enumerator);
    }
}