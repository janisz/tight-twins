using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twins.Model;

namespace Twins.Players
{
    public class BetterFirstPlayer : IPlayer
    {
        public async Task Move(MainViewModel viewModel)
        {
            await Task.Delay(viewModel.MoveDelay * 1000);

            var fieldsLeftCount = viewModel.BoardSize - viewModel.BoardItems.Count();
            var position = MinMove(viewModel.BoardItems.ToList(), viewModel.ColorsCount, viewModel.BoardSize).position;

            var item = new BoardItem() { Value = viewModel.BoardItems.Count() };
            viewModel.BoardItems.Insert(position, item);
            viewModel.SelectedBoardItem = item;
        }

       
        public struct FirstPlayerMove
        {
            public int color;
            public int rank;
        }

        public struct SecondPlayerMove
        {
            public int position;
            public int rank;
        }

        static public FirstPlayerMove MaxMove(List<BoardItem> board, int position, int colorCount, int maxSize)
        {
            if (TwinsChecker.CheckTwins(board))
            {
                // Should never happend
                throw new Exception();
            }

            FirstPlayerMove bestMove = new FirstPlayerMove() { rank = int.MinValue };
                        
            for (int color = 0; color < colorCount; color++)
            {
                var newBoard = board.ConvertAll(_ => new BoardItem(_.Color)).ToList();
                newBoard[position].Color = color;

                // Sprawdzamy czy gra się zakończyła
                if (TwinsChecker.CheckTwins(newBoard)) // Są ciasne bliźniaki (przegrywamy)
                {                    
                    return new FirstPlayerMove() { color = color, rank = -1 };
                }
                else if (newBoard.Count() == maxSize) // Doszliśmy do końca (wygrywamy)
                {                    
                    return new FirstPlayerMove() { color = color, rank = 1 };
                }
                else // Gra się nie skończyła
                {
                    SecondPlayerMove secondPlayerMove = MinMove(newBoard, colorCount, maxSize);
                    if (bestMove.rank >= secondPlayerMove.rank)
                    {
                        bestMove = new FirstPlayerMove() { color = color, rank = secondPlayerMove.rank };
                    }
                }
            }
            return bestMove;
        }

        static public SecondPlayerMove MinMove(List<BoardItem> board, int colorCount, int maxSize)
        {
            if (board.Count() == maxSize || TwinsChecker.CheckTwins(board))
            {
                // Should never happend
                throw new Exception();
            }

            SecondPlayerMove bestMove = new SecondPlayerMove{ rank = int.MinValue };

            for (int position = 0; position < board.Count; position++)
            {
                var newBoard = board.ConvertAll(_ => new BoardItem(_.Color)).ToList();
                //Wastawiamy element
                var item = new BoardItem();
                newBoard.Insert(position, item);                

                // Gra pierwszy gracz
                FirstPlayerMove firstPlayerMove = MaxMove(newBoard, position, colorCount, maxSize);
                if (bestMove.rank <= firstPlayerMove.rank)
                {
                    bestMove = new SecondPlayerMove() { position = position, rank = firstPlayerMove.rank }; ;
                }
            }
            return bestMove;
        }
    }
}
