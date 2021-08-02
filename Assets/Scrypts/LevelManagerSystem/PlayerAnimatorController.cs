using Assets.Scrypts.InputModule;
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

        //�������������� � LevelManager
        //��� ����� ����������
        public void SetVictory() =>
            anim.SetTrigger("Victory");
        //��� �������� ���������
        public void SetLose() =>
            anim.SetTrigger("Lose");

    }
}