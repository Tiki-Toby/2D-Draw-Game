using Assets.Scrypts.InputModule;
using Assets.Scrypts.LevelManagerSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scrypts.Entity
{
    public enum SymbolCloseType
    {
        Left,
        Right,
        AnyOrder
    }
    public abstract class EnemyState : IDisposable
    {
        protected Action onEnd;
        protected Animator anim;
        private string stateKey;
        public void Subscribe(Action sub) => onEnd += sub;
        public EnemyState(Animator anim, string stateKey)
        {
            this.anim = anim;
            anim.SetBool(stateKey, true);
            this.stateKey = stateKey;
        }
        public abstract bool EndCondition();
        public abstract void Update();

        protected void SetAnimState(bool isState) =>
            anim.SetBool(stateKey, isState);
        public void Dispose()
        {
            Debug.Log(stateKey);
            anim.SetBool(stateKey, false);
            if(onEnd != null)
                onEnd.Invoke();
        }
    }
    class IdleState : EnemyState
    {
        float timer;
        public IdleState(Animator anim, float time) : base(anim, "")
        {
            timer = Time.time + time;
        }
        public override bool EndCondition()
        {
            return timer < Time.time;
        }
        public override void Update()
        {

        }
    }
    class WalkToTargetState : EnemyState
    {
        Transform transform;
        Vector2 direction;
        Vector3 distance;
        public WalkToTargetState(Animator anim, Transform transform, Vector2 target, float velocity) : base(anim, "isMove")
        {
            Debug.Log("Entity Walk");
            Debug.Log("Target: " + target.ToString());
            this.transform = transform;
            distance = (Vector2)transform.localPosition - target;
            direction = distance.normalized * velocity;
            transform.GetComponent<SpriteRenderer>().flipX = direction.x > 0;
        }
        public override bool EndCondition()
        {
            return Mathf.Sqrt(distance.sqrMagnitude) < 0.02f;
        }

        public override void Update()
        {
            Vector3 offset = direction * Time.deltaTime;
            transform.position -= offset;
            distance -= offset;
        }
    }
    class AttackState : EnemyState
    {
        FortressController shelter;
        AttackData attackData;
        float timer;
        Action onAttack;

        public AttackState(Animator anim, FortressController shelter, AttackData attackData, Action onAttack) : base(anim, "isAttack")
        {
            Debug.Log("StartAttack");
            anim.GetBehaviour<ControllAttack>().attackDelay = attackData.AttackDelay;
            this.attackData = attackData;
            this.onAttack = onAttack;
            this.shelter = shelter;
            timer = Time.time + 0.2f;
            anim.SetFloat("AttackSpeed", 1);
            SetAnimState(false);
        }
        public override bool EndCondition() => attackData.attackCount == 0 && !anim.GetBool("isAttack");
        public override void Update()
        {
            if(timer < Time.time)
            {
                timer += attackData.AttackDelay;
                shelter.TakeDamage(attackData.dmg);
                attackData.attackCount--;
                onAttack.Invoke();
                SetAnimState(true);
            }
        }
    }
    [Serializable]
    public struct AttackData
    {
        [SerializeField] public float dmg;
        [SerializeField] public float heatPerSecond;
        [SerializeField] public int attackCount;

        public float AttackDelay { get => 1f / heatPerSecond; } 

        public AttackData(float dmg, float heatPerSecond, int attackCount)
        {
            this.dmg = dmg;
            this.heatPerSecond = heatPerSecond;
            this.attackCount = attackCount;
        }
    }
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] protected Animator anim;
        [SerializeField] protected SymbolCloseType closeType;
        [SerializeField] protected int countSymbol;
        [SerializeField] protected float velocity;
        [SerializeField] protected AttackData attackData;

        protected Vector2 spawnPoint;
        protected EnemyState state { get; private set; }
        protected EnemyState State
        {
            set
            {
                state.Dispose();
                state = value;
            }
        }
        private List<char> hpSymbols;
        private Func<char, bool> onTakeDamage;

        void Awake()
        {
            InitTakeDamage(closeType);
            state = new IdleState(anim, 0);
            InitSymbols(LevelData.Data.symbols);
        }
        public void SetSpawnPoint(Vector2 spawnPoint)
        {
            transform.position = spawnPoint;
            this.spawnPoint = spawnPoint;
        }
        public void InitSymbols(char[] symbols)
        {
            hpSymbols = new List<char>();
            int last = symbols.Length;
            for(int i = 0; i < countSymbol; i++)
            {
                int index = UnityEngine.Random.Range(0, last);
                hpSymbols.Add(symbols[index]);
                Debug.Log(symbols[index]);
            }    
        }
        protected abstract void StateMachine();
        private void InitTakeDamage(SymbolCloseType closeType)
        {
            switch (closeType)
            {
                case SymbolCloseType.AnyOrder:
                    onTakeDamage = (char c) =>
                    {
                        bool isContain = hpSymbols.Contains(c);
                        if (isContain)
                            hpSymbols.Remove(c);
                        return isContain;
                    };
                    break;
                case SymbolCloseType.Left:
                    onTakeDamage = (char c) =>
                    {
                        bool isContain = hpSymbols[0] == c;
                        if (isContain)
                            hpSymbols.RemoveAt(0);
                        return isContain;
                    };
                    break;
                case SymbolCloseType.Right:
                    onTakeDamage = (char c) =>
                    {
                        bool isContain = hpSymbols.Last() == c;
                        if (isContain)
                            hpSymbols.RemoveAt(hpSymbols.Count - 1);
                        return isContain;
                    };
                    break;
            }
            InputBehaviour.Subscribe(TakeDamage);
        }
        private void TakeDamage(char c)
        {
            if (onTakeDamage.Invoke(c))
            {
                anim.SetTrigger("TakingDamage");
                if (hpSymbols.Count == 0)
                {
                    anim.SetBool("isDead", true);
                    State = new WalkToTargetState(anim, transform, spawnPoint, velocity * 2);
                    state.Subscribe(() => Destroy(gameObject));
                }
            }
        }
        private void Update()
        {
            if (state.EndCondition())
                StateMachine();
            else
               state.Update();
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            FortressController shelter = collider.GetComponent<FortressController>();
            if(shelter != null)
                State = new AttackState(anim, shelter, attackData, () => attackData.attackCount--);
        }
        private void OnDestroy()
        {
            InputBehaviour.UnSubscribe(TakeDamage);
        }
    }
}
