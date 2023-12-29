using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gavin.AStar
{
    public enum InputType
    {
        /// <summary>
        /// 围栏
        /// </summary>
        Fence,
        Road,
        Water,
        Start,
        End
    }
    public class InputManager : MonoBehaviour
    {
        [HideInInspector] public static InputManager Instance;
        public InputType currentType=InputType.Road;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
                currentType=InputType.Fence;
            if (Input.GetKeyDown(KeyCode.R))
                currentType = InputType.Road;
            if (Input.GetKeyDown(KeyCode.W))
                currentType = InputType.Water;
            if (Input.GetKeyDown(KeyCode.S))
                currentType = InputType.Start;
            if (Input.GetKeyDown(KeyCode.E))
                currentType = InputType.End;
            if (Input.GetKeyDown(KeyCode.Space))
                GridManager.Instance.SearchShortPath();

        }
    }
}