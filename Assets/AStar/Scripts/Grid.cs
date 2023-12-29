using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gavin.AStar
{
    /// <summary>
    /// 寻路网格
    /// </summary>
    public class Grid : MonoBehaviour, IPointerClickHandler
    {
        private Image image;
        private Image stateImg;
        private InputType inputType = InputType.Road;
        public InputType InputType
        {
            get { return inputType; } 
            set
            {
                inputType = value;
                SetImage();
            }
        }
        public Vector2 pos;
        public Grid parentGrid;
        public int cost;

        private void Start()
        {
            image = GetComponent<Image>();
            stateImg = GetComponentInChildren<Image>();
            SetImage();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            inputType = InputManager.Instance.currentType;

            //如果自己从特殊节点改变就通知管理器
            if (inputType == InputType.Start || inputType == InputType.End)
            {
                GridManager.Instance.IsStartOrEndChange(inputType, this);
            }

            //根据类型确定自己应该显示什么图片

            SetImage();

        }
        private void SetImage()
        {
            string imageName = inputType.ToString();
            string path = "AStar/Image/" + imageName;
            Sprite sprite = Resources.Load<Sprite>(path);
            image.sprite = sprite;
            Debug.Log("被设置成"+imageName);
        }

        public void SetSearchState(SearchState searchState)
        {
            //根据传入的枚举改变图片颜色
            switch (searchState)
            {
                case SearchState.Normal: break;
                case SearchState.Open: break;
                case SearchState.Close: break;
                case SearchState.Path: break;

            }
        }

    }
}