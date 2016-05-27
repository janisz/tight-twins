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
            SetDefaultValues();
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

            var twins = TwinsChecker.FindTightTwins(BoardItems);

            if (twins != null)
            {
                foreach (var item in twins.Item1)
                {
                    item.TwinIndex = 0;
                }

                foreach (var item in twins.Item2)
                {
                    item.TwinIndex = 1;
                }

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
            if (Version == "Komputer vs. Człowiek")
            {
                FirstPlayer = new FirstPlayer();
                SecondPlayer = new HumanPlayer();
            }
            else if (Version == "Komputer II vs. Człowiek")
            {
                FirstPlayer = new BetterFirstPlayer();
                SecondPlayer = new HumanPlayer();
            }
            else if (Version == "Komputer vs. Komputer")
            {
                FirstPlayer = new FirstPlayer();
                SecondPlayer = new SecondPlayer();
            }
            else
            {
                FirstPlayer = new BetterFirstPlayer();
                SecondPlayer = new BetterSecondPlayer();
            }
        }

        private void SetDefaultValues()
        {
            BoardItems = new ObservableCollection<BoardItem>(new List<BoardItem>());
            MoveDelay = 1;
            Version = "Komputer vs. Człowiek";
            BoardSize = 10;
            ColorsCount = 3;
        }
    }
}
