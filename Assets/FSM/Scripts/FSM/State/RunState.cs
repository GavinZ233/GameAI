


using UnityEngine;

namespace Gavin.FSM
{
    public class RunState : BaseState
    {
        public override E_AI_State AI_State => E_AI_State.Run;
        public RunState(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void EnterState()
        {
            Debug.Log("进入逃跑状态");
            stateMachine.aiObj.Move(stateMachine.aiObj.bornPos);
        }

        public override void QuitState()
        {
            
        }

        public override void UpdateState()
        {
            //判断是否回到出生点
            if (Vector3.Distance(stateMachine.aiObj.nowPos,stateMachine.aiObj.bornPos)<=0.5f)
            {
                stateMachine.ChangeState(E_AI_State.Patrol);
            }
        }
    }
}

