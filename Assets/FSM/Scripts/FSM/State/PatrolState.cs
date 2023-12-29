


using UnityEngine;

namespace Gavin.FSM
{
    /// <summary>
    /// 巡逻状态
    /// </summary>
    public class PatrolState : BaseState
    {
        public override E_AI_State AI_State => E_AI_State.Patrol;

        //巡逻的中心点
        private Vector3 centerPos;
        //x和z方向的偏移量
        private float xRange;

        private float zRange;
        //记录时间计时器
        private float timer;

        private float waitTime;

        private Vector3 targetPos;

        public PatrolState(StateMachine stateMachine) : base(stateMachine)
        {
            waitTime = 2;
        }

        public override void EnterState()
        {
            Debug.Log("进入巡逻状态");
            timer = waitTime;
        }

        public override void QuitState()
        {
            stateMachine.aiObj.StopMove();
        }

        public override void UpdateState()
        {

            if (timer <= 0)
            {
                //计算随机巡逻点
                GetTargetPos();
                //执行移动
                stateMachine.aiObj.Move(targetPos);
                //当走近目标点，停止移动等待
                if (Vector3.Distance(targetPos, stateMachine.aiObj.nowPos) < 0.2f)
                {
                    stateMachine.aiObj.StopMove();
                    timer = waitTime;
                }

            }
            else
                timer -= Time.deltaTime;

            //以防万一也进行脱离范围判断
            if (stateMachine.aiObj.IsLeavingStationedArea())
                stateMachine.ChangeState(E_AI_State.Run);
        }

        private void GetTargetPos()
        {

            float x = Random.Range(centerPos.x - xRange, centerPos.x + xRange);
            float z = Random.Range(centerPos.z - zRange, centerPos.z + zRange);
            targetPos = new Vector3(x, 0.5f, z);
        }

    }
}

