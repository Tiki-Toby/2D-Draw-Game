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
        }
    }
}
