using System;
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
            var position = viewModel.BoardItems.Count() / 2;

            var item = new BoardItem() { Value = viewModel.BoardItems.Count() };
            viewModel.BoardItems.Insert(position, item);
            viewModel.SelectedBoardItem = item;
        }
    }
}
