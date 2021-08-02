using Assets.Scrypts.LevelManagerSystem;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scrypts.Entity
{
    class FortressController : MonoBehaviour
    {
        [SerializeField] Sprite[] views;
        [SerializeField] float hp;
        [SerializeField] bool isRegenable;
        [SerializeField] float regenHpPerSecond;

        private SpriteRenderer image;
        private int curSprite;
        private float curHP;

        void Start()
        {
            image = GetComponent<SpriteRenderer>();
            Array.Reverse(views);
            curHP = hp;
            UpdateImage();

            if (isRegenable)
                StartCoroutine(Regenerate());
        }
        public void TakeDamage(float dmg)
        {
            curHP -= dmg;
            if (curHP > 0)
                UpdateImage();
            else
            {
                LevelData.levelData.fortressCount.Value--;
                Destroy(gameObject);
            }
        }
        private void UpdateImage()
        {
            int index = (int)(curHP * (views.Length - 1) / hp);
            if (index != curSprite)
            {
                image.sprite = views[index];
                curSprite = index;
            }
        }
        private IEnumerator Regenerate()
        {
            while (gameObject.activeSelf)
            {

                if (curHP < hp)
                {
                    float add = regenHpPerSecond * Time.deltaTime;
                    curHP = Mathf.Clamp(curHP + add, curHP, hp);
                    UpdateImage();
                }
                yield return null;
            }
        }
        private void OnDestroy()
        {

        }
    }
}
