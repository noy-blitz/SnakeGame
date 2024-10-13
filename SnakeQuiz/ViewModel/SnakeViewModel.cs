using SnakeQuiz.Commands;
using SnakeQuiz.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace SnakeQuiz.ViewModel
{
    public class SnakeViewModel : INotifyPropertyChanged
    {
        private SnakeModel _snakeModel;
        public ICommand MoveCommand { get; set; }
        public ICommand ChangeDirectionCommand { get; set; }

        public SnakeViewModel()
        {
            _snakeModel = new SnakeModel();
            MoveCommand = new RelayCommand(o => Move());
            ChangeDirectionCommand = new RelayCommand(o => ChangeDirection(o));
        }

        public void Move()
        {
            _snakeModel.Move();
            OnPropertyChanged(nameof(SnakeParts));
            OnPropertyChanged(nameof(FoodPosition));
            OnPropertyChanged(nameof(IsGameOver));
        }

        public void ChangeDirection(object direction)
        {
            _snakeModel.ChangeDirection((int)direction);
        }

        public List<Point> SnakeParts => _snakeModel.SnakeParts;
        public Point FoodPosition => _snakeModel.FoodPosition;
        public bool IsGameOver => _snakeModel.IsGameOver;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
