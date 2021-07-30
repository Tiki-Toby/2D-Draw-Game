using UnityEngine;

namespace Assets.Scrypts.Entity
{
    abstract class WalkElement
    {
        protected Transform transform;
        protected Vector3 distance;
        protected Vector3 direction;
        protected float velocity;
        public WalkElement(Transform transform, float velocity)
        {
            this.velocity = velocity;
            this.transform = transform;
        }
        public void SetTarget(Vector3 target)
        {
            distance = transform.localPosition - target;
            direction = distance.normalized * velocity;
        }
        public abstract void UpdatePosition();
    }
    class WalkLinear : WalkElement
    {
        public WalkLinear(Transform transform, Vector3 target, float velocity) : base(transform, velocity)
        {
            SetTarget(target);
        }
        public override void UpdatePosition()
        {
            if(Mathf.Sqrt(distance.sqrMagnitude) > 0.02f)
            {
                Vector3 offset = direction * Time.deltaTime;
                transform.position -= offset;
                distance -= offset;
            }
        }
    }
    class WalkZigZag : WalkElement
    {
        public WalkZigZag(Transform transform, Vector2[] shelters, float velocity) : base(transform, velocity)
        {

        }
        public override void UpdatePosition()
        {

        }
    }
}
