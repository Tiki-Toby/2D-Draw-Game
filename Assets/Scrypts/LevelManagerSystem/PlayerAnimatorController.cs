using Assets.Scrypts.GameData;
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
            anim.SetTrigger(PlayerStateNames.MISTAKE);

        //����� �������� �� ������ ����
        //����� ���� ������ ������
        public void SetAttack() =>
            anim.SetTrigger(PlayerStateNames.ATTACK);

        public void Reset()
        {
            anim.SetBool(PlayerStateNames.SELEBRATE, false);
            anim.SetBool(PlayerStateNames.LOSE, false);
            anim.ResetTrigger(PlayerStateNames.MISTAKE);
            anim.ResetTrigger(PlayerStateNames.ATTACK);
        }

        //�������������� � LevelManager
        //��� ����� ����������
        public void SetVictory() =>
            anim.SetBool(PlayerStateNames.SELEBRATE, true);
        //��� �������� ���������
        public void SetLose() =>
            anim.SetBool(PlayerStateNames.LOSE, true);

    }
}