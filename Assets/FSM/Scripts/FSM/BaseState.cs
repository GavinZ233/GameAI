namespace Gavin.FSM
{

    public abstract class BaseState
    {
        protected StateMachine stateMachine;

        public BaseState(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public virtual E_AI_State AI_State { get; }

        public abstract void QuitState();

        public abstract void EnterState();

        public abstract void UpdateState();
    }
}