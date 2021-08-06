using Assets.Scrypts.Entity;
using Assets.Scrypts.GameData;
using Assets.Scrypts.LevelManagerSystem;
using UniRx;
using UnityEngine;

namespace Assets.Scrypts.Enemy
{
    enum WalkType
    {
        Linear,
        Hidden
    }
    public class WalkEnemy : EnemyController
    {
        [SerializeField] WalkType walkType;
        [SerializeField] protected float velocity;
        private Vector2 spawnPoint;
        private Vector2[] points;
        private int curPoint;

        private void Start()
        {
            spawnPoint = _transform.position;
            LevelData.levelData.fortressCount
                .Subscribe((int count) =>
                {
                    if (count > 0)
                    {
                        switch (walkType)
                        {
                            case WalkType.Linear:
                                points = new Vector2[1];
                                points[0] = PathManager.pathManager.ClosestFortress(transform.position);
                                break;
                            case WalkType.Hidden:
                                points = PathManager.pathManager.SearchPath(transform.position);
                                break;
                        }
                        curPoint = 0;
                        if (!(State is AttackState))
                        {
                            State = new WalkToTargetState(anim, _transform, points[curPoint], velocity);
                            State.Subscribe(() => curPoint++);
                            curPoint = 0;
                        }
                    }
                    else
                        EnemyWin();
                })
                .AddTo(this);
        }
        private void EnemyWin()
        {
            anim.SetBool("isStolen", true);
            enemyHp.SwitchHide();
            State = new WalkToTargetState(anim, transform, spawnPoint, velocity * 1.5f);
            State.Subscribe(() => Destroy(gameObject));
        }
        protected override void StateMachine()
        {
            //состояние когда лимит атак врага достигнут
            //уходит с наворованным
            if (attackData.attackCount == 0 && !enemyHp.isHide)
            {
                EnemyWin();
                if(PathManager.pathManager.GetFortress().Length > 0)
                    //уменьшаем счетчик врагов
                    LevelData.levelData.enemyCount.Value--;
            }
            //проверка жизней
            else if (!enemyHp.isAlive)
            {
                State = new WalkToTargetState(anim, _transform, spawnPoint, velocity * 2);
                State.Subscribe(() => Destroy(gameObject));
            }
            //Если путь ломанный, то прячется
            else if (anim.GetBool("isMove"))
                State = new HideState(anim, HpStruct, EnemyData.TimeInHideState);
            //если цель не достигнута
            else if (points.Length > curPoint)
            {
                State = new WalkToTargetState(anim, transform, points[curPoint], velocity);
                State.Subscribe(() => curPoint++);
            }
            //если что то идет не так, идет к владению напрямую
            else
                State = new WalkToTargetState(anim, transform, points[points.Length - 1], velocity);
        }
    }
}
