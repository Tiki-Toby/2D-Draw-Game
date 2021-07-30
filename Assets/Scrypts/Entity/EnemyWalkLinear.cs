using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scrypts.Entity
{

    class EnemyWalkLinear : Enemy
    {
        protected override void StateMachine()
        {
            if (attackData.attackCount == 0)
            {
                anim.SetBool("isHide", true);
                State = new WalkToTargetState(anim, transform, spawnPoint, velocity * 1.5f);
                state.Subscribe(() => Destroy(gameObject));
            }
            else
                State = new WalkToTargetState(anim, transform, FindObjectOfType<FortressController>().transform.position, velocity);
        }
    }
}
