using UnityEngine;

namespace Assets.Scrypts.InputModule
{
    class TestModule : MonoBehaviour
    {
        void Start()
        {
            InputBehaviour.Subscribe((char c) => Debug.Log(c));
        }
    }
}
