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

        //������� � DrawInput
        //����� ���� �������� ������
        public void SetMistake() =>
            anim.SetTrigger("Mistake");

        //����� �������� �� ������ ����
        //����� ���� ������ ������
        public void SetAttack() =>
            anim.SetTrigger("Attack");

        public void Reset()
        {
            anim.SetBool("isVictory", false);
            anim.SetBool("isLose", false);
            anim.ResetTrigger("Mistake");
            anim.ResetTrigger("Attack");
        }

        //�������������� � LevelManager
        //��� ����� ����������
        public void SetVictory() =>
            anim.SetBool("isVictory", true);
        //��� �������� ���������
        public void SetLose() =>
            anim.SetBool("isLose", true);

    }
}