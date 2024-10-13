using SnakeQuiz.Commands;
using SnakeQuiz.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SnakeQuiz.ViewModel
{
    public class SnakeViewModel : ViewModelBase
    {
        private SnakeModel _snake;
        public ObservableCollection<bool> GridCells { get; set; }

        public ICommand MoveCommand { get; private set; }

        public bool CanMoveUp { get; private set; }
        public bool CanMoveDown { get; private set; }
        public bool CanMoveLeft { get; private set; }
        public bool CanMoveRight { get; private set; }

        private readonly int _gridSize;
        private readonly Point _up = new Point(0, -1);
        private readonly Point _down = new Point(0, 1);
        private readonly Point _left = new Point(-1, 0);
        private readonly Point _right = new Point(1, 0);

        public int GridSize => _gridSize;

        public SnakeViewModel()
        {
            _gridSize = 10; // Example grid size
            int initialLength = 5; // Example initial snake length

            _snake = new SnakeModel(_gridSize, initialLength);
            GridCells = new ObservableCollection<bool>(new bool[_gridSize * _gridSize]);
            UpdateGrid();

            MoveCommand = new RelayCommand(ExecuteMove);
            UpdateButtonStates();
        }

        private void UpdateGrid()
        {
            // Clear all cells
            for (int i = 0; i < GridCells.Count; i++)
            {
                GridCells[i] = false;
            }

            // Set snake body cells to true
            foreach (var segment in _snake.Body)
            {
                int index = (int)(segment.Y * _gridSize + segment.X);
                GridCells[index] = true;
            }

            OnPropertyChanged(nameof(GridCells));
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

            if (_snake.Move(direction))
            {
                UpdateGrid();
                UpdateButtonStates();
            }
        }

        private void UpdateButtonStates()
        {
            var head = _snake.Body[0];
            CanMoveUp = head.Y > 0 && !_snake.Body.Contains(new Point(head.X, head.Y - 1));
            CanMoveDown = head.Y < _gridSize - 1 && !_snake.Body.Contains(new Point(head.X, head.Y + 1));
            CanMoveLeft = head.X > 0 && !_snake.Body.Contains(new Point(head.X - 1, head.Y));
            CanMoveRight = head.X < _gridSize - 1 && !_snake.Body.Contains(new Point(head.X + 1, head.Y));

            OnPropertyChanged(nameof(CanMoveUp));
            OnPropertyChanged(nameof(CanMoveDown));
            OnPropertyChanged(nameof(CanMoveLeft));
            OnPropertyChanged(nameof(CanMoveRight));
        }
    }
}
