using Assets.Scrypts.InputModule;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scrypts.Enemy
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        [SerializeField] Animator anim;

        private void Start()
        {
            InputBehaviour.Subscribe((string _) => SetAttack());
        }

        //событие в DrawInput
        //игрок ввел неверный символ
        public void SetMistake() =>
            anim.SetTrigger("Mistake");

        //через подписку на верный ввод
        //игрок ввел верный символ
        public void SetAttack() =>
            anim.SetTrigger("Attack");

        public void Reset()
        {
            anim.SetBool("isVictory", false);
            anim.SetBool("isLose", false);
            anim.ResetTrigger("Mistake");
            anim.ResetTrigger("Attack");
        }

        //обрабатывается в LevelManager
        //все враги побежденны
        public void SetVictory() =>
            anim.SetBool("isVictory", true);
        //все владения разрушены
        public void SetLose() =>
            anim.SetBool("isLose", true);

    }
}