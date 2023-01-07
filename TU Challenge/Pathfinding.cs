using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace TU_Challenge
{
    public class Pathfinding
    {
        string _map;

        public char[,] Grid 
        { 
            get; 
            private set;
        }

        public Pathfinding(string map)
        {
            _map = map;
            int width = 0;
            while (map[width] != '\n')
            {
                width++;
            }
            Grid = new char[map.Length/width, width];
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int y = 0; y < Grid.GetLength(1); y++)
                {
                    Grid[i, y] = map[i * 6 + y];
                }
            }
        }

        public bool IsOutOfBound(Vector2 vector2)
            => vector2.X < 0 || vector2.X >= Grid.GetLength(0)
                || vector2.Y < 0 || vector2.Y >= Grid.GetLength(1);

        public List<Vector2> GetNeighboors(Vector2 vector2)
        {
            List <Vector2> neighboors = new List<Vector2>();
            for (int i = -1; i < 2; i++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if (i == 0 && y == 0) continue;
                    Vector2 currentNeighboor = new Vector2(vector2.X + i, vector2.Y + y);
                    if (IsOutOfBound(currentNeighboor)) continue;
                    if (Grid[currentNeighboor.X, currentNeighboor.Y] == 'X') continue;
                    neighboors.Add(currentNeighboor);
                }
            }
            return neighboors;
        }

        public List<Vector2> GetNeighboors(Vector2 vector2, List<Vector2> excludes)
        {
            List<Vector2> neighboors = new List<Vector2>();
            for (int i = -1; i < 2; i++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if (i == 0 && y == 0) continue;
                    Vector2 currentNeighboor = new Vector2(vector2.X + i, vector2.Y + y);
                    if (excludes.Contains(currentNeighboor)) continue;
                    if (IsOutOfBound(currentNeighboor)) continue;
                    if (Grid[currentNeighboor.X, currentNeighboor.Y] == 'X') continue;
                    neighboors.Add(currentNeighboor);
                }
            }
            return neighboors;
        }

        public char GetCoord(Vector2 el)
        {
            Assert.IsFalse(IsOutOfBound(el));
            return Grid[el.X, el.Y];
        }

        private int DistanceOnSquaredMap(Vector2 a, Vector2 b)
        {
            return Math.Abs(a.Y - b.Y) + Math.Abs(a.X - b.X);
        }

        public Path BreadthFirstSearch(Vector2 start, Vector2 destination)
        {
            Queue<Vector2> frontier = new Queue<Vector2>();
            frontier.Enqueue(start);
            Dictionary<Vector2, Vector2> origin = new Dictionary<Vector2, Vector2>();
            bool notArrived = true;
            while (notArrived && frontier.Count != 0)
            {
                Vector2 currentPosition = frontier.Dequeue();
                foreach (Vector2 item in GetNeighboors(currentPosition))
                {
                    if (item == destination)
                    {
                        notArrived = false;
                        origin.Add(item, currentPosition);
                        break;
                    }
                    if (origin.ContainsKey(item)) continue;
                    origin.Add(item, currentPosition);
                    frontier.Enqueue(item);
                }
            }
            Path path = new Path(start);
            if (!notArrived)
            {
                Vector2 currentPosition = destination;
                while (currentPosition != start)
                {
                    path.CompletePath.Insert(1,currentPosition);
                    currentPosition = origin[currentPosition];
                }
            }
            return path;
        }

        public Path AStarPathFinding(Vector2 start, Vector2 destination)
        {
            PriorityQueue<Vector2, int> frontier = new();
            frontier.Enqueue(start, 0);
            Dictionary<Vector2, Vector2> origin = new();
            Dictionary<Vector2, int> costToStart = new();
            costToStart.Add(start, 0);
            bool notArrived = true;
            while (notArrived && frontier.Count != 0)
            {
                Vector2 currentPosition = frontier.Dequeue();
                foreach (Vector2 item in GetNeighboors(currentPosition))
                {
                    if (item == destination)
                    {
                        notArrived = false;
                        origin.Add(item, currentPosition);
                        break;
                    }
                    int new_cost = costToStart[currentPosition] + 1;
                    if (!origin.ContainsKey(item))
                    {
                        costToStart[item] = new_cost;
                        frontier.Enqueue(item, new_cost + DistanceOnSquaredMap(item, destination));
                        origin.Add(item, currentPosition);
                    }
                    else if (new_cost < costToStart[item])
                    {
                        costToStart[item] = new_cost;
                        frontier.Enqueue(item, new_cost + DistanceOnSquaredMap(item, destination));
                        origin[item] = currentPosition;
                    }
                }
            }
            Path path = new Path(start);
            if (!notArrived)
            {
                Vector2 currentPosition = destination;
                while (currentPosition != start)
                {
                    path.CompletePath.Insert(1, currentPosition);
                    currentPosition = origin[currentPosition];
                }
            }
            return path;
        }
    }

}
