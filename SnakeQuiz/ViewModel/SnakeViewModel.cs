using SnakeQuiz.Commands;
using SnakeQuiz.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SnakeQuiz.ViewModel
{
    // The SnakeViewModel class manages the state of the game and binds to the UI.
    public class SnakeViewModel : ViewModelBase
    {
        private SnakeModel _snake;// Instance of SnakeModel representing the snake's body and movement logic.

        // Observable collection of bools representing the game grid.
        // Each cell in the grid is either true (snake present) or false (empty).
        public ObservableCollection<bool> GridCells { get; set; }

        public ICommand MoveCommand { get; private set; }

        // Properties to track which directions the snake can legally move.
        // These properties are used to enable/disable direction buttons in the UI.
        public bool CanMoveUp { get; private set; }
        public bool CanMoveDown { get; private set; }
        public bool CanMoveLeft { get; private set; }
        public bool CanMoveRight { get; private set; }

        private readonly int _gridSize;
        // Points representing movement directions (Up, Down, Left, Right) as changes in coordinates.
        private readonly Point _up = new Point(0, -1);
        private readonly Point _down = new Point(0, 1);
        private readonly Point _left = new Point(-1, 0);
        private readonly Point _right = new Point(1, 0);

        // Property to expose the grid size to the UI.
        public int GridSize => _gridSize;

        // Constructor to initialize the snake, grid, and commands.
        public SnakeViewModel()
        {
            _gridSize = 10; // Example grid size
            int initialLength = 9; // Example initial snake length

            _snake = new SnakeModel(_gridSize, initialLength); // Initialize the snake model.
            GridCells = new ObservableCollection<bool>(new bool[_gridSize * _gridSize]); // Initialize grid cells to false (empty).
            InitializeGrid(); // Update the grid to reflect the snake's initial position.

            MoveCommand = new RelayCommand(ExecuteMove, CanExecuteMove);
        }

        // Updates the grid to reflect the snake's current position.
        // was changed to initializGrid() for optimization(not moving through all the matrix)
        //private void UpdateGrid()
        //{
        //    // Clear all cells in the grid by setting them to false.
        //    for (int i = 0; i < GridCells.Count; i++)
        //    {
        //        GridCells[i] = false;
        //    }

        //    // Set snake body cells to true in the grid to show the snake's position.
        //    foreach (var segment in _snake.Body)
        //    {
        //        int index = (int)(segment.Y * _gridSize + segment.X); // Calculate the index in the 1D collection.
        //        GridCells[index] = true; // Set cell to true where the snake is present.
        //    }

        //    OnPropertyChanged(nameof(GridCells)); // Notify the UI that GridCells has been updated.
        //}


        // Initializes the grid based on the initial snake position.
        private void InitializeGrid()
        {
            foreach (var segment in _snake.Body)
            {
                int index = (int)(segment.Y * _gridSize + segment.X);
                GridCells[index] = true;
            }

            OnPropertyChanged(nameof(GridCells)); // Notify the UI that GridCells has been updated.
        }


        private void ExecuteMove(object parameter)
        {
            Point direction = parameter switch
            {
                "Up" => _up,
                "Down" => _down,
                "Left" => _left,
                "Right" => _right,
                _ => throw new ArgumentException("Invalid move direction")
            };

            // Get the current tail position for updating it later.
            var oldTail = _snake.Body.Last();

            // Move the snake and update the grid if the move is successful.
            if (_snake.Move(direction))
            {
                // Set the new head cell to true
                var newHead = _snake.Body.First();
                int newHeadIndex = (int)(newHead.Y * _gridSize + newHead.X);
                GridCells[newHeadIndex] = true;

                // Set the old tail cell to false
                int oldTailIndex = (int)(oldTail.Y * _gridSize + oldTail.X);
                GridCells[oldTailIndex] = false;

                ((RelayCommand)MoveCommand).RaiseCanExecuteChanged(); // Notify that CanExecute conditions may have changed- in order to auto disable\enable buttons.
            }
        }

        // Determines whether a move is valid based on the specified direction.
        private bool CanExecuteMove(object parameter)
        {
            var head = _snake.Body[0]; // Get the snake's head position.

            // Check if the move is valid based on the direction and return the result.
            return parameter switch
            {
                "Up" => head.Y > 0 && !_snake.Body.Contains(new Point(head.X, head.Y - 1)), // Ensure no collision or out-of-bounds.
                "Down" => head.Y < _gridSize - 1 && !_snake.Body.Contains(new Point(head.X, head.Y + 1)),
                "Left" => head.X > 0 && !_snake.Body.Contains(new Point(head.X - 1, head.Y)),
                "Right" => head.X < _gridSize - 1 && !_snake.Body.Contains(new Point(head.X + 1, head.Y)),
                _ => false // Return false for invalid direction parameters.
            };
        }
    }
}
