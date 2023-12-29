


using UnityEngine;

namespace Gavin.FSM
{
    public class AtkState : BaseState
    {
        public override E_AI_State AI_State => E_AI_State.Atk;

        private float nextAtkTime;

        private float deleyTime = 1f;



        public AtkState(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void EnterState()
        {
            Debug.Log("进入战斗");
            nextAtkTime = Time.time;
        }

        public override void QuitState()
        {
            Debug.Log("退出战斗");

        }

        public override void UpdateState()
        {
            //先看向
            stateMachine.aiObj.objTransform.LookAt(stateMachine.aiObj.targetObjPos + Vector3.up * 0.5f);


            //开始攻击
            if (Time.time>=nextAtkTime)
            {
                stateMachine.aiObj.Atk();
                nextAtkTime += deleyTime;
            }


            //距离是否符合,超出返回回到追逐范围
            if (Vector3.Distance(stateMachine.aiObj.nowPos,stateMachine.aiObj.targetObjPos)>stateMachine.aiObj.atkRange)
            {
                nextAtkTime = deleyTime;
            }

            //检测是否超出范围
            if (stateMachine.aiObj.IsLeavingStationedArea())
            {
                stateMachine.ChangeState(E_AI_State.Run);
            }

        }

    }
}

