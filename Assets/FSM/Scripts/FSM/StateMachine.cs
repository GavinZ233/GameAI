using System;
using System.Collections.Generic;

namespace Gavin.FSM
{
    /// <summary>
    /// 状态机
    /// 负责切换状态
    /// </summary>
    public class StateMachine
    {


        private Dictionary<E_AI_State, BaseState> stateDic = new Dictionary<E_AI_State, BaseState>();

        private BaseState nowState;

        public IAIObj aiObj;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="aiObj">空置的ai对象</param>
        public void Init(IAIObj aiObj)
        {
            this.aiObj = aiObj;
        }


        public void AddState(E_AI_State state)
        {
            string typeName = state.ToString() + "State";
            Type type = Type.GetType(typeName);
            BaseState newState = Activator.CreateInstance(type, this) as BaseState;
            stateDic.Add(state, newState);
        }

        public void ChangeState(E_AI_State state)
        {
            if (nowState != null)
                nowState.QuitState();

            if (stateDic.ContainsKey(state))
            {
                nowState = stateDic[state];
                nowState.EnterState();
            }
        }

        public void UpdateState()
        {
            if (nowState != null)
            {
                nowState.UpdateState();
            }
        }

    }
}