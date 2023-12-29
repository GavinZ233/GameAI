

using UnityEngine;

namespace Gavin.FSM
{
    public class ChaseState : BaseState
    {
        public override E_AI_State AI_State => E_AI_State.Chase;
        /// <summary>
        /// 延迟多少帧检测
        /// </summary>
        private int delay;
        private int index;
        public ChaseState(StateMachine stateMachine) : base(stateMachine)
        {
            delay = 6;
        }

        public override void EnterState()
        {
            Debug.Log("进入追逐状态");
            index = 0;
        }

        public override void QuitState()
        {
            
        }

        public override void UpdateState()
        {
            if (index%delay==0)
                stateMachine.aiObj.Move(stateMachine.aiObj.targetObjPos);

            ++index;

            if (Vector3.Distance(stateMachine.aiObj.nowPos, stateMachine.aiObj.targetObjPos) <= stateMachine.aiObj.atkRange)
                stateMachine.ChangeState(E_AI_State.Atk);

            if (stateMachine.aiObj.IsLeavingStationedArea())
                stateMachine.ChangeState(E_AI_State.Run);
        }
    }
}

