using Assets.Scrypts.LevelManagerSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scrypts.Entity
{
    class HiddenEnemy : Enemy
    {
        Vector2[] points;
        int curPoint;

        void Start()
        {
            curPoint = 0;
            points = LevelData.Data.GetNodesForPath(spawnPoint);
        }
        protected override void StateMachine()
        {
            if (attackData.attackCount == 0)
            {
                anim.SetBool("isHide", true);
                State = new WalkToTargetState(anim, transform, spawnPoint, velocity * 1.5f);
                state.Subscribe(() => Destroy(gameObject));
            }
            else if (points.Length > curPoint)
            {
                State = new WalkToTargetState(anim, transform, points[curPoint], velocity);
                Debug.Log(points[curPoint]);
                state.Subscribe(() => curPoint++);
            }
            else
                State = new WalkToTargetState(anim, transform, FindObjectOfType<FortressController>().transform.position, velocity);
        }
    }
}
