using System.Collections.ObjectModel;
using System.Linq;
using PropertyChanged;
using Twins.Helpers;
using Twins.Model;
using Twins.Players;
using System.Collections;
using System.Collections.Generic;

namespace Twins
{
    public enum Turn
    {
        First,
        Second
    }
    [ImplementPropertyChanged]
    public class MainViewModel
    {
        public MainViewModel()
        {
            MessageBoxService = new MessageBoxService();
            GameStarted = false;
            MoveDelay = 1;
            BoardItems = new ObservableCollection<BoardItem>(new List<BoardItem>());
        }
        public IMessageBoxService MessageBoxService { get; private set; }
        public bool GameStarted { get; set; }
        /// <summary>
        /// Wesja gry: B/C
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// Długość słowa
        /// </summary>
        public int BoardSize { get; set; }
        /// <summary>
        /// Rozmiar alfabetu
        /// </summary>
        public int ColorsCount { get; set; }
        public ObservableCollection<Color> Colors { get; set; }
        [DoNotCheckEquality]
        public BoardItem SelectedBoardItem { get; set; }
        public Color SelectedColor { get; set; }
        public ObservableCollection<BoardItem> BoardItems { get; set; }
        public int CurrentRound { get {
             return BoardItems.Count();
        } }
        public Turn CurrentTurn { get; set; }
        public int MoveDelay { get; set; }
        public IPlayer FirstPlayer { get; set; }
        public IPlayer SecondPlayer { get; set; }

        public async void OnSelectedColorChanged()
        {
            if (!GameStarted)
                return;

            if (SelectedColor == null)
                return;

            SelectedBoardItem.Color = SelectedColor.Index;

            var twinsExist = TwinsChecker.CheckTwins(BoardItems);

            if (twinsExist)
            {
                MessageBoxService.ShowMessage("Blizniaki wygrały!", "");
                GameStarted = false;
                return;
            }
            else if (CurrentRound >= BoardSize)
            {
                MessageBoxService.ShowMessage("Blizniaki przegrały!", "");
                GameStarted = false;
                return;
            }

            CurrentTurn = Turn.First;

            await FirstPlayer.Move(this);

            SelectedBoardItem.IsSelected = true;

            SelectedColor = null;
        }

        public async void OnSelectedBoardItemChanged()
        {
            if (!GameStarted)
                return;

            CurrentTurn = Turn.Second;

            await SecondPlayer.Move(this);
        }

        public async void StartGame()
        {
            GameStarted = true;
            InitialCheck();
            SetupVersion();
            CurrentTurn = Turn.First;
            await FirstPlayer.Move(this);
            SelectedBoardItem.IsSelected = true;
        }

        public void OnColorsCountChanged()
        {
            var integerList = Enumerable.Range(0, ColorsCount);
            var newColors = integerList.Select(i => new Color { Index = i }).ToList();
            Colors = new ObservableCollection<Color>(newColors);
        }

        public void OnBoardSizeChanged()
        {
            var integerList = Enumerable.Range(1, BoardSize);            
        }

        /// <summary>
        /// Sprawdzenie warunków dla których z góry znany jest zwycięzca
        /// </summary>
        private void InitialCheck()
        {
        }

        private void SetupVersion()
        {
            if (Version == null || Version == "B")
            {
                Version = "B";
                FirstPlayer = new FirstPlayer();
                SecondPlayer = new HumanPlayer();
            }
            else if (Version == "C")
            {
                Version = "C";
                FirstPlayer = new FirstPlayer();
                SecondPlayer = new SecondPlayer();
            }
            else if(Version == "B2")
            {
                Version = "B2";
                FirstPlayer = new BetterFirstPlayer();
                SecondPlayer = new HumanPlayer();
            }
            else
            {
                Version = "C2";
                FirstPlayer = new BetterFirstPlayer();
                SecondPlayer = new BetterSecondPlayer();
            }
        }
    }
}
