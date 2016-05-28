using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twins.Helpers;
using Twins.Model;

namespace Twins.Players
{
    public class BetterFirstPlayer : IPlayer
    {
        private static readonly Random _random = new Random();

        static Dictionary<string, SecondPlayerMove> MinMoveCache = new Dictionary<string, SecondPlayerMove>(1000);

        public async Task Move(MainViewModel viewModel)
        {
            await Task.Delay(viewModel.MoveDelay * 1000);

            var fieldsLeftCount = viewModel.BoardSize - viewModel.BoardItems.Count();
            var position = MinMove(viewModel.BoardItems.ToList(), viewModel.ColorsCount, viewModel.BoardSize).Position;

            var item = new BoardItem() { Value = viewModel.BoardItems.Count() };
            viewModel.BoardItems.Insert(position, item);
            viewModel.SelectedBoardItem = item;
        }

        public struct FirstPlayerMove
        {
            public int Color;
            public int Rank;
        }

        public struct SecondPlayerMove
        {
            public int Position;
            public int Rank;
        }

        public static FirstPlayerMove MaxMove(List<BoardItem> board, int position, int colorCount, int maxSize)
        {
            if (TwinsChecker.CheckTwins(board))
            {
                // Should never happend
                throw new Exception();
            }
            var bestMove = new FirstPlayerMove() { Rank = int.MinValue };

            var colors = Enumerable.Range(0, colorCount);
            foreach(var color in colors)    
            {
                var newBoard = board.ConvertAll(_ => new BoardItem(_.Color)).ToList();
                newBoard[position].Color = color;

                // Sprawdzamy czy gra się zakończyła
                if (TwinsChecker.CheckTwins(newBoard)) // Są ciasne bliźniaki (przegrywamy)
                {
                    if (bestMove.Rank < newBoard.Count)
                    {
                        bestMove = new FirstPlayerMove() { Color = color, Rank = newBoard.Count };
                    }
                }
                else if (newBoard.Count == maxSize) // Doszliśmy do końca (wygrywamy)
                {
                    return new FirstPlayerMove() { Color = color, Rank = int.MaxValue };
                }
                else // Gra się nie skończyła
                {
                    // Gra drugi gracz (maksymalizujemy)
                    SecondPlayerMove secondPlayerMove = MinMoveCached(newBoard, colorCount, maxSize);
                    if (bestMove.Rank < secondPlayerMove.Rank)
                    {
                        bestMove = new FirstPlayerMove() { Color = color, Rank = secondPlayerMove.Rank };
                    }
                }
            }
            return bestMove;
        }

        public static SecondPlayerMove MinMove(List<BoardItem> board, int colorCount, int maxSize)
        {
            if (board.Count() == maxSize || TwinsChecker.CheckTwins(board))
            {
                // Should never happend
                throw new Exception();
            }
            var bestMove = new SecondPlayerMove() { Rank = int.MaxValue };

            var positions = Enumerable.Range(0, board.Count);
            foreach(var position in positions)
            {
                var newBoard = board.ConvertAll(_ => new BoardItem(_.Color)).ToList();
                //Wastawiamy element
                var item = new BoardItem();
                newBoard.Insert(position, item);

                // Gra pierwszy gracz (minimalizujemy)
                FirstPlayerMove firstPlayerMove = MaxMove(newBoard, position, colorCount, maxSize);
                if (bestMove.Rank > firstPlayerMove.Rank)
                {
                    bestMove = new SecondPlayerMove() { Position = position, Rank = firstPlayerMove.Rank }; ;
                }
            }
            return bestMove;
        }

        public static SecondPlayerMove MinMoveCached(List<BoardItem> board, int colorCount, int maxSize)
        {
            var serialized = SerializeMinMoveParameters(board, colorCount, maxSize);

            if (MinMoveCache.ContainsKey(serialized))
            {
                return MinMoveCache[serialized];
            }

            var minMove = MinMove(board, colorCount, maxSize);
            MinMoveCache[serialized] = minMove;
            return minMove;
        }

        public static string SerializeMinMoveParameters(List<BoardItem> board, int colorCount, int maxSize)
        {
            var colorMap = new Dictionary<int, int>(100);

            return string.Join(" ", board.Select(x => ColorNormalizer.NormalizeColor(x.Color ?? -1, colorMap).ToString())) + ", " + colorCount + ", " + maxSize;
        }
    }
}
