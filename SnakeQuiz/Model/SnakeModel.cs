using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SnakeQuiz.Model
{
    // The SnakeModel class represents the state and movement logic of the snake in the game.
    public class SnakeModel
    {
        // A list of Points representing the segments of the snake's body.
        // Each Point defines the X and Y coordinates of a segment in the grid.
        public List<Point> Body { get; private set; }
        // The size of the grid on which the snake can move, specified in number of cells.
        public int GridSize { get; private set; }

        // Constructor that initializes the snake with a specified grid size and initial length.
        public SnakeModel(int gridSize, int initialLength)
        {
            // Ensure initial length does not exceed grid size.
            if (initialLength > gridSize)
            {
                throw new ArgumentException("Initial length of the snake cannot be greater than the grid size.");
            }
            GridSize = gridSize;
            InitializeSnake(initialLength);
        }
        // Initializes the snake at the start of the game with a specified initial length.
        private void InitializeSnake(int initialLength)
        {
            Body = new List<Point>(); // Creates a new list to store the snake's body segments.
            for (int i = 0; i < initialLength; i++) // Loops to add initial segments to the snake.
            {
                // Adds each segment horizontally to the list, starting at the center Y position.
                Body.Add(new Point((GridSize + initialLength)/ 2 - 1 - i, GridSize / 2));
            }
        }
        // Moves the snake in the direction specified by the Point parameter.
        // Returns true if the move is successful, false if it hits the boundary or itself.
        public bool Move(Point direction)
        {
            // Calculates the new head position based on the current head and the movement direction.
            Point newHead = new Point(Body[0].X + direction.X, Body[0].Y + direction.Y);

            //extra checks (in case of view change):

            // Checks if the new head position is out of grid bounds. Returns false if it is.
            if (newHead.X < 0 || newHead.Y < 0 || newHead.X >= GridSize || newHead.Y >= GridSize)
                return false;

            // Checks if the new head position would collide with the body. Returns false if it does.
            if (Body.Skip(1).Contains(newHead))
                return false;


            // Inserts the new head position at the beginning of the list (snake moves forward).
            Body.Insert(0, newHead);
            // Removes the last segment of the snake's body to maintain its length.
            Body.RemoveAt(Body.Count - 1);
            // Returns true, indicating that the move was successful.
            return true;
        }
    }
}
