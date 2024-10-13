using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SnakeQuiz.Model
{
    public class SnakeModel
    {
        public List<Point> SnakeParts { get; set; }
        public Point FoodPosition { get; set; }
        public int Direction { get; set; } // 0: Up, 1: Right, 2: Down, 3: Left
        public bool IsGameOver { get; set; }

        public SnakeModel()
        {
            SnakeParts = new List<Point> { new Point(5, 5) }; // Initial position
            GenerateFood();
            Direction = 1; // Initial direction (Right)
            IsGameOver = false;
        }

        public void Move()
        {
            if (IsGameOver) return;

            // Move snake logic here
        }

        public void ChangeDirection(int newDirection)
        {
            // Change direction logic here
        }

        public void GenerateFood()
        {
            // Food generation logic here
        }
    }
}
