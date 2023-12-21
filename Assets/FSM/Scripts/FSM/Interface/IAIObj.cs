
using UnityEngine;

namespace Gavin.FSM
{
    /// <summary>
    /// AI对象接口
    /// </summary>
    public interface IAIObj
    {
        public Transform objTransform { get; }

        public Vector3 nowPos { get; }

        public Vector3 targetObjPos { get; }

        public float atkRange { get; }

        public Vector3 bornPos { get; }

        /// <summary>
        /// 移动方法
        /// </summary>
        /// <param name="dirOrPos">目标点或者方向</param>
        public void Move(Vector3 dirOrPos);

        public void StopMove();

        public void Atk();
        /// <summary>
        /// 切换动作
        /// </summary>
        /// <param name="action">动作枚举</param>
        public void ChangeAction(E_Action action);
    }
}