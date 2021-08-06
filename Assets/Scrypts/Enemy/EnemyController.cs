using Assets.Scrypts.Entity;
using Assets.Scrypts.InputModule;
using Assets.Scrypts.LevelManagerSystem;
using System;
using System.Collections.Generic;
using UnityEngine;  

namespace Assets.Scrypts.Enemy
{
    public abstract class EnemyController : MonoBehaviour
    {
        [SerializeField] protected Animator anim;
        [SerializeField] protected EnemyHP enemyHp;
        protected ref EnemyHP HpStruct => ref enemyHp;
        [SerializeField] protected AttackData attackData;
        [SerializeField] private Loot[] loots;

        protected Transform _transform;
        //ткущее состояние врага
        private EnemyState state;
        //Свойство для изменения состояния и коректного завершения предыдущего
        protected EnemyState State
        {
            get => state;
            set
            {
                state.Dispose();
                state = value;
                state.SetAnimState(true);
            }
        }

        void Awake()
        {
            _transform = transform;
            //состояние по дефолту
            //В Start() наследника можно задать другое
            state = new IdleState(anim, -1);

            //определение данных о хп
            enemyHp.CreateHp(_transform);

            //Подписка на уорректный ввод
            InputBehaviour.Subscribe(TakeDamage);

            //Создание указателя, пока враг за экраном
            Instantiate(Resources.Load<EnemyPointer>("Prefabs/Objects/EnemyPointer"), _transform)
                .InitPointer(_transform);
        }
        //Задаются следующие состояния, по завершению предыдущего
        protected abstract void StateMachine();

        //Обработка введенного символа
        private void TakeDamage(string c)
        {
            if (enemyHp.isTakeDamage(c))
            {
                //состояние получения урона
                anim.SetTrigger("TakingDamage");
                if (!enemyHp.isAlive)
                {
                    //уменьшаем счетчик врагов
                    LevelData.levelData.enemyCount.Value--;
                    //состояние смерти
                    anim.SetTrigger("DeadTrigger");
                    //спавним лут
                    SpawnLoot();
                    //состояние ожидание (в StateMachine() соверается переход)
                    State = new IdleState(anim, -1);
                }
            }
        }
        protected void SpawnLoot()
        {
            //определяем дроп
            Dictionary<ValueType, Loot> onSpawnLoot = new Dictionary<ValueType, Loot>();
            foreach (Loot loot in loots)
            {
                float randomValue = UnityEngine.Random.value;
                float offset = loot.propability / 2;
                if (randomValue > 0.5f - offset && randomValue < 0.5f + offset)
                {
                    ValueType valueType = loot.lootPrefab.valutType;
                    if (onSpawnLoot.ContainsKey(valueType))
                        onSpawnLoot[valueType] += loot;
                    else
                        onSpawnLoot.Add(valueType, loot);
                }
            }
            //спавним дроп
            foreach (Loot loot in onSpawnLoot.Values)
                loot.Spawn(_transform.position);
        }
        private void Update()
        {
            //если состояние окончено определяем следующее, иначе апдейтим
            if (state.EndCondition())
                StateMachine();
            else
               state.Update();
        }

        //тригер на атаку владения
        protected void OnTriggerEnter2D(Collider2D collider)
        {
            FortressController fortress = collider.GetComponent<FortressController>();
            if(fortress)
                State = new AttackState(anim, fortress, attackData, () => attackData.attackCount--);
        }

        //отписка от ввода символов
        private void OnDestroy()
        {
            InputBehaviour.UnSubscribe(TakeDamage);
        }
    }
}
