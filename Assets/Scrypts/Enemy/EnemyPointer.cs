using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;

namespace Assets.Scrypts.Enemy
{
    class EnemyPointer : MonoBehaviour
    {
        private Transform parentEnemy;
        private Transform _transform;
        //границы экрана
        private Vector2 leftBottom, rightTop;

        private void Start()
        {
            //высчитываем границы экрана с учетом размера спрайта
            _transform = transform;
            Sprite sprite = GetComponent<SpriteRenderer>().sprite;
            Vector2 spriteSize = Camera.main.ScreenToWorldPoint(sprite.rect.size) * _transform.localScale.x;
            Debug.Log(spriteSize);

            leftBottom = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            leftBottom.x -= spriteSize.x / 4f;
            leftBottom.y -= spriteSize.y / 4f;

            rightTop = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
            rightTop.x += spriteSize.x / 4f;
            rightTop.y += spriteSize.y / 4f;
        }

        //ставим наблюдателя
        public void InitPointer(Transform parentEnemy) =>
            this.parentEnemy = parentEnemy;

        private void Update()
        {
            Vector2 position = parentEnemy.position;

            if (Condition(position))
                UpdatePostion(position);
            else
                gameObject.SetActive(false);
        }

        //условие врага за областью видимости
        public bool Condition(Vector2 position)
        {
            bool isIn = position.x > rightTop.x || position.x < leftBottom.x || position.y > rightTop.y;
            return isIn;
        }
        
        private void UpdatePostion(Vector2 position)
        {
            //считаем позицию
            Vector2 newPosition;
            newPosition.x = Mathf.Clamp(position.x, leftBottom.x, rightTop.x);
            newPosition.y = Mathf.Clamp(position.y, leftBottom.y, rightTop.y);
            _transform.position = newPosition;

            //считаем угол
            float angle = Vector2.Angle(newPosition, Vector2.down);
            if (position.x < 0)
                angle *= -1;
            Quaternion target = Quaternion.Euler(0, 0, angle);
            transform.rotation = target;
        }
    }
}
