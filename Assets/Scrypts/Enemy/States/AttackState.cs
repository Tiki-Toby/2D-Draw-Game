using Assets.Scrypts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scrypts.Enemy
{

    //состояние 
    class AttackState : EnemyState
    {
        FortressController fortress;
        AttackData attackData;
        float timer;
        Action onAttack;

        public AttackState(Animator anim, FortressController fortress, AttackData attackData, Action onAttack) : base(anim, "isAttack")
        {
            anim.GetBehaviour<ControllAttack>().attackDelay = attackData.AttackDelay;
            this.attackData = attackData;
            this.onAttack = onAttack;
            this.fortress = fortress;
            timer = Time.time;
            anim.SetFloat("AttackSpeed", 1);
            SetAnimState(false);
        }
        public override bool EndCondition() => (attackData.attackCount == 0 && !anim.GetBool("isAttack")) || !fortress;
        public override void Update()
        {
            if (timer < Time.time)
            {
                timer += attackData.AttackDelay;
                fortress.TakeDamage(attackData.dmg);
                attackData.attackCount--;
                onAttack.Invoke();
                SetAnimState(true);
            }
        }
    }
}
