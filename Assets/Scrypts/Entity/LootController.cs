using Assets.Scrypts.LevelManagerSystem;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scrypts.Entity
{
    class LootController : MonoBehaviour
    {
        [SerializeField] float velocity;
        [SerializeField] long valueToAdd;
        public ValutType valutType;

        [SerializeField] Ease x, y;

        private Vector2 targetPosition;
        public void InitLoot(Vector2 position, long value)
        {
            transform.position = position;
            valueToAdd = value;
            switch (valutType)
            {
                case ValutType.Coin:
                    targetPosition = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
                    break;
                case ValutType.Crystal:
                    targetPosition = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));
                    break;
            }
            transform.DOMoveX(targetPosition.x, 1).SetEase(x).OnComplete(() =>
            {
                LevelData.levelData.AddValute(valutType, valueToAdd);
                Destroy(gameObject);
            });
            transform.DOMoveY(targetPosition.y, 1).SetEase(y);
        }
    }
}
