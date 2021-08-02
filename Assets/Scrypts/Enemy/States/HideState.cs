using UnityEngine;

namespace Assets.Scrypts.Enemy
{
    public class HideState : EnemyState
    {
        float time;
        public HideState(Animator anim, SymbolOutputController symbolContainer, float time) : base(anim, "isHide")
        {
            this.time = Time.time + time;
            symbolContainer.HideSymbols();
            onEnd += () => symbolContainer.HideSymbols(false);
        }
        public override bool EndCondition()
        {
            return Time.time > time;
        }

        public override void Update()
        {
            //sitting
        }
    }
}
