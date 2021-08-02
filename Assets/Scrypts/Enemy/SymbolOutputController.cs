using Assets.Scrypts.LevelManagerSystem;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scrypts.Enemy
{
    public class SymbolOutputController : MonoBehaviour
    {
        private Transform _transform;
        float width;
        private void Awake()
        {
            _transform = transform;
        }
        public void InitSymbolChain(string[] symbols)
        {
            width = LevelData.levelData.GetSpriteOf(symbols[0]).rect.width * 0.01f;
            int size = symbols.Length;
            for(int i = 0; i < size; i++)
                CreateSpriteObject(LevelData.levelData.GetSpriteOf(symbols[i]), (i - (size - 1) / 2f) * width);
        }

        public void AddSymbol(string symbolSpriteName, SymbolCloseType addType = SymbolCloseType.Right) =>
            AddSymbol(LevelData.levelData.GetSpriteOf(symbolSpriteName), addType);
        public void AddSymbol(Sprite symbolSprite, SymbolCloseType addType = SymbolCloseType.Right)
        {
            float width = this.width / 2f;
            if (addType == SymbolCloseType.Left)
                width *= -1;
            foreach (Transform symbol in _transform)
                symbol.localPosition += Vector3.left * width;
            if (addType == SymbolCloseType.Left)
                CreateSpriteObject(symbolSprite, -(_transform.childCount - 1) / 2f * width);
            else
                CreateSpriteObject(symbolSprite, (_transform.childCount - 1) / 2f * width);
        }

        public void TakeDamage(int index)
        {
            float width = this.width / 2f;

            for (int i = 0; i < index; i++)
                _transform.GetChild(i).localPosition += Vector3.right * width;

            Destroy(_transform.GetChild(index).gameObject);

            int size = _transform.childCount - 1;
            for (int i = index + 1; i < size; i++)
                _transform.GetChild(i).localPosition += Vector3.left * width;
        }

        public void HideSymbols(bool isNeedHide = true)
        {
            SpriteRenderer[] sprites = _transform.GetComponentsInChildren<SpriteRenderer>();
            if (isNeedHide)
                for (int i = 0; i < sprites.Length; i++)
                    sprites[i].DOFade(0.2f, 0.5f);
            else
                for (int i = 0; i < sprites.Length; i++)
                    sprites[i].DOFade(1, 0.5f);
        }
        private SpriteRenderer CreateSpriteObject(Sprite sprite, float pos)
        {
            SpriteRenderer spriteRenderer = new GameObject("SymbolIcon").AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingOrder = 2;
            spriteRenderer.transform.SetParent(_transform);
            spriteRenderer.transform.localPosition = new Vector2(pos, 0);
            return spriteRenderer;
        }
    }
}