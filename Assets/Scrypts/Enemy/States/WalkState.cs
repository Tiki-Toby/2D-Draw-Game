using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scrypts.Enemy
{
    class WalkToTargetState : EnemyState
    {
        float minOffset;
        Transform transform;
        Vector2 direction;
        Vector3 distance;
        public WalkToTargetState(Animator anim, Transform transform, Vector2 target, float velocity) : base(anim, "isMove")
        {
            minOffset = 0.02f;
            Debug.Log("Target: " + target.ToString());
            this.transform = transform;
            distance = (Vector2)transform.localPosition - target;
            direction = distance.normalized * velocity;
            transform.GetComponent<SpriteRenderer>().flipX = direction.x > 0;
        }
        public override bool EndCondition()
        {
            return Mathf.Sqrt(distance.sqrMagnitude) < minOffset;
        }

        public override void Update()
        {
            Vector3 offset = direction * Time.deltaTime;
            transform.position -= offset;
            distance -= offset;
        }
    }
}
