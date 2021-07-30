using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scrypts.InputModule
{
    public class KeyBoardInput : InputBehaviour
    {
        protected override char InputSymbol()
        {
            return Input.inputString[0];
        }
        void Update()
        {
            if (Input.anyKeyDown)
                OnSymbolInput();
        }
    }
}
