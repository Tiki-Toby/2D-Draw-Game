using Assets.Scrypts.Entity;
using Assets.Scrypts.LevelManagerSystem;
using UnityEngine;

namespace Assets.Scrypts.Enemy
{
    enum WalkType
    {
        Linear,
        Hidden
    }
    public class WalkEnemy : Enemy
    {
        [SerializeField] WalkType walkType;
        [SerializeField] protected float velocity;
        private Vector2 spawnPoint;
        private Vector2[] points;
        private int curPoint;

        private void Start()
        {
            //пока не доработан алгоритм поиска пути
            spawnPoint = _transform.position;
            //определение поведения врага
            switch (walkType)
            {
                case WalkType.Linear:
                    points = new Vector2[1];
                    points[0] = LevelData.levelData.GetClosestFortress(transform.position);
                    break;
                case WalkType.Hidden:
                    points = LevelData.levelData.GetNodesForPath(transform.position);
                    break;
            }
            curPoint = 0;
            State = new WalkToTargetState(anim, _transform, points[curPoint], velocity);
            State.Subscribe(() => curPoint++);
        }

        protected override void StateMachine()
        {
            //состояние когда лимит атак врага достигнут
            //уходит с наворованным
            if (attackData.attackCount == 0 || !GameObject.FindGameObjectWithTag("Fortress"))
            {
                anim.SetBool("isStolen", true);
                anim.SetBool("isHide", true);
                enemyHp.HideGUI();
                State = new WalkToTargetState(anim, transform, spawnPoint, velocity * 1.5f);
                State.Subscribe(() => Destroy(gameObject));
            }
            //проверка жизней
            else if (!enemyHp.isAlive)
            {
                State = new WalkToTargetState(anim, _transform, spawnPoint, velocity * 2);
                State.Subscribe(() => Destroy(gameObject));
            }
            //Если путь ломанный, то прячется
            else if (anim.GetBool("isMove"))
                State = new HideState(anim, enemyHp.SymbolContainer, 1);
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
        //private void OnTriggerExit2D(Collider2D collider)
        //{
        //    if (LevelData.levelData.fortressCount.Value > 0)
        //        if (attackData.attackCount > 0 && isAlive)
        //        {
        //            switch (walkType)
        //            {
        //                case WalkType.Linear:
        //                    points = new Vector2[1];
        //                    points[0] = LevelData.levelData.GetClosestFortress(transform.position);
        //                    break;
        //                case WalkType.Hidden:
        //                    points = LevelData.levelData.GetNodesForPath(transform.position);
        //                    break;
        //            }
        //            curPoint = 0;
        //            State = new WalkToTargetState(anim, _transform, points[curPoint], velocity);
        //        }
        //}
    }
}
