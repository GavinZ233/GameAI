using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
namespace Gavin.AStar2D
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
            ClearGridPathColor();

            if (start == null || end == null)
                return;

            //开始搜索

            //将格子转成数据
            int[,] gridData = new int[xCount,yCount];
            (int,int) startPos=(0,0);
            (int, int) endPos=(0,0);
            for (int x = 0; x < xCount; x++)
            {
                for (int y = 0; y < yCount; y++)
                {
                    gridData[x, y] = gridDic[new Vector2(x, y)].cost;
                    if (gridDic[new Vector2(x, y)].InputType == InputType.Start)
                        startPos = (x, y);
                    if (gridDic[new Vector2(x, y)].InputType == InputType.End)
                        endPos = (x, y);

                }
            }
            if (startPos==(0,0)&& (0, 0) == endPos)
                UnityEngine.Debug.LogError("起点和终点数据未录入");
            UnityEngine.Debug.Log("开始寻路算法");
            Stopwatch sw = Stopwatch.StartNew();

            ReBuildGrid(MyStar.FindPath(gridData, startPos, endPos));
            sw.Stop();
            UnityEngine.Debug.Log($"寻路耗时: {sw.ElapsedMilliseconds} ms");

        }

        private void ReBuildGrid(List<(int, int)> path)
        {
            foreach (var item in path)
            {
                gridDic[new Vector2(item.Item1, item.Item2)].SetSearchState(SearchState.Path);
            }
        }
        public void ClearGridPathColor()
        {
            foreach (var item in gridDic.Values)
            {
                item.SetSearchState(SearchState.Normal);
            }
        }

        public void ClearGridData()
        {
            foreach (var item in gridDic.Values)
            {
                item.SetSearchState(SearchState.Normal);
                item.InputType = InputType.Road;
                item.cost = 0;
            }
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