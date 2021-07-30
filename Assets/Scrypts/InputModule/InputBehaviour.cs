using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scrypts.InputModule
{
    public abstract class InputBehaviour : MonoBehaviour
    {
        [SerializeField] static UnityEvent<char> onSymbolInput = new UnityEvent<char>();
        public static void Subscribe(UnityAction<char> sub) => onSymbolInput.AddListener(sub);
        public static void UnSubscribe(UnityAction<char> sub) => onSymbolInput.RemoveListener(sub);
        public void OnSymbolInput() => onSymbolInput.Invoke(InputSymbol());
        protected abstract char InputSymbol();
    }
}
