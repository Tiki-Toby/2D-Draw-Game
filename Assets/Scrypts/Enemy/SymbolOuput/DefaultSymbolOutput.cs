using Assets.Scrypts.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scrypts.Enemy.SymbolOuput
{
    class DefaultSymbolOutput : SymbolOutputController
    {
        protected override void AlignSprites()
        {
            int size = sprites.Count;
            float width = this.width / 2f;
            for (int i = 0; i < size; i++)
                sprites[i].transform.localPosition = Vector2.right * width * (i - (size - 1) / 2f);

            ScaleCurrentSymbol();
        }
        private void ScaleCurrentSymbol()
        {
            if (closeType == SymbolCloseType.Left)
            {
                sprites[0].transform.localScale = Vector3.one * EnemyData.ScaleSymbolByOrder;
                sprites[0].transform.localPosition += Vector3.left * width * (EnemyData.ScaleSymbolByOrder - 1) / sprites.Count;
            }
            else if (closeType == SymbolCloseType.Right)
            {
                int size = sprites.Count;
                if (size > 0)
                {
                    sprites[size - 1].transform.localScale = Vector3.one * EnemyData.ScaleSymbolByOrder;
                    sprites[size - 1].transform.localPosition += Vector3.right * width * (EnemyData.ScaleSymbolByOrder - 1) / (size);
                }
            }
        }
    }
}
