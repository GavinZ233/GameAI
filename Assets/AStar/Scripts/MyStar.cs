

using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Gavin.AStar2D
{
    public class MyStar
    {
        public class Node
        {
            public int X, Y;
            public Node Parent;
            public int G, H, F; // g:实际代价, h:启发式代价, f:总代价

            public Node(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        private static readonly (int dx, int dy)[] Directions =
        {(0, 1), (1, 0), (0, -1), (-1, 0)};// 上、右、下、左

        public static List<(int x, int y)> FindPath(int[,] grid, (int x, int y) start, (int x, int y) end)
        {
            var openList = new List<Node>();
            var closedList = new HashSet<(int, int)>();

            var startNode = new Node(start.x, start.y);
            var endNode = new Node(end.x, end.y);

            openList.Add(startNode);

            while (openList.Count > 0)
            {
                // 从开放列表中选择 f 值最小的节点
                var currentNode = openList.OrderBy(n => n.F).First();
                openList.Remove(currentNode);
                closedList.Add((currentNode.X, currentNode.Y));

                // 检查是否到达目标
                if (currentNode.X == endNode.X && currentNode.Y == endNode.Y)
                    return ReconstructPath(currentNode);

                // 遍历邻居节点
                foreach (var (dx, dy) in Directions)
                {
                    var neighborX = currentNode.X + dx;
                    var neighborY = currentNode.Y + dy;

                    // 检查邻居是否在地图范围内
                    if (neighborX < 0 || neighborX >= grid.GetLength(0) ||
                        neighborY < 0 || neighborY >= grid.GetLength(1))
                        continue;

                    // 检查是否是障碍物或已处理节点
                    if (grid[neighborX, neighborY] == 1 || closedList.Contains((neighborX, neighborY)))
                        continue;

                    // 计算邻居节点的代价
                    var neighborNode = new Node(neighborX, neighborY)
                    {
                        Parent = currentNode,
                        G = currentNode.G + 1,
                        H = Math.Abs(endNode.X - neighborX) + Math.Abs(endNode.Y - neighborY),
                    };
                    neighborNode.F = neighborNode.G + neighborNode.H;

                    // 如果邻居已经在开放列表中，并且新的 g 值更高，则跳过
                    var existingNode = openList.FirstOrDefault(n => n.X == neighborX && n.Y == neighborY);
                    if (existingNode != null && existingNode.G <= neighborNode.G)
                        continue;

                    // 将邻居加入开放列表
                    if (existingNode == null)
                        openList.Add(neighborNode);
                }
            }

            // 未找到路径
            return null;
        }

        private static List<(int x, int y)> ReconstructPath(Node node)
        {
            var path = new List<(int x, int y)>();
            while (node != null)
            {
                path.Add((node.X, node.Y));
                node = node.Parent;
            }
            path.Reverse();
            return path;
        }

        public static void Main()
        {
            int[,] grid = {
            { 0, 0, 0, 0 },
            { 1, 1, 0, 1 },
            { 0, 0, 0, 0 },
            { 0, 1, 1, 0 }
        };

            var start = (0, 0);
            var end = (3, 3);

            var path = FindPath(grid, start, end);

            if (path != null)
            {
                Debug.Log("Path found:");
                foreach (var point in path)
                    Debug.Log($"({point.x}, {point.y})");
            }
            else
            {
                Debug.Log("No path found.");
            }
        }

    }




}