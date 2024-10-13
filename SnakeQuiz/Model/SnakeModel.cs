using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SnakeQuiz.Model
{
    public class SnakeModel
    {
        public List<Point> Body { get; private set; }
        public int GridSize { get; private set; }

        public SnakeModel(int gridSize, int initialLength)
        {
            GridSize = gridSize;
            InitializeSnake(initialLength);
        }

        private void InitializeSnake(int initialLength)
        {
            Body = new List<Point>();
            for (int i = 0; i < initialLength; i++)
            {
                Body.Add(new Point(i, 0)); // Initialize snake horizontally from the top-left
            }
        }

        public bool Move(Point direction)
        {
            Point newHead = new Point(Body[0].X + direction.X, Body[0].Y + direction.Y);

            if (newHead.X < 0 || newHead.Y < 0 || newHead.X >= GridSize || newHead.Y >= GridSize)
                return false;

            if (Body.Skip(1).Contains(newHead))
                return false;

            Body.Insert(0, newHead);
            Body.RemoveAt(Body.Count - 1);
            return true;
        }
    }
}
