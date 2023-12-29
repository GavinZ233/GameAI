using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gavin.AStar
{
    public enum SearchState
    {
        Normal,
        Open,
        Close,
        Path
    }
    public class GridManager : MonoBehaviour
    {
        [HideInInspector] public static GridManager Instance;
        private Dictionary<Vector2, Grid> gridDic = new Dictionary<Vector2, Grid>();
        public int xCount;
        public int yCount;
        private RectTransform rectTransform;
        private Grid start;
        private Grid end;

        public List<Grid> openGrids=new List<Grid>();
        public List<Grid> closeGrids=new List<Grid>();
        public Stack<Grid> pathGrids=new Stack<Grid>();

        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            //创建grid
            Grid gridPre = Resources.Load<Grid>("AStar/Grid");
            for (int y = 0; y < xCount; y++)
            {
                for (int x = 0; x < yCount; x++)
                {
                    Grid grid = GameObject.Instantiate(gridPre);
                    grid.pos = new Vector2(x, y);
                    grid.GetComponent<RectTransform>().SetParent(rectTransform, false);
                    gridDic[new Vector2(x, y)] = grid;
                }
            }
        }

        private Grid GetGrid(Vector2 pos)
        {
            if (gridDic.ContainsKey(pos))
                return gridDic[pos];
            else return null;
        }

        public void SearchShortPath()
        {
            if (start == null || end == null)
                return;

            //开始搜索
        }

        


        /// <summary>
        /// 如果是起点或者终点，取消之前的起点终点，保证唯一
        /// </summary>
        /// <param name="inputType"></param>
        /// <param name="grid"></param>
        public void IsStartOrEndChange(InputType inputType, Grid grid)
        {
            switch (inputType)
            {
                case InputType.Start:
                    if (start!=null)
                    {
                        start.InputType = InputType.Road;
                    }
                    start = grid;
                    break;

                case InputType.End:
                    if (end != null)
                    {
                        end.InputType = InputType.Road;
                    }
                    end = grid;
                    break;
            }
        }
    }
}