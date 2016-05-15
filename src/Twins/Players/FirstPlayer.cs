using System;
using System.Linq;
using System.Threading.Tasks;
using Twins.Model;

namespace Twins.Players
{
    public class FirstPlayer : IPlayer
    {
        private Random _random = new Random(DateTime.Now.Millisecond);

        public async Task Move(MainViewModel viewModel)
        {
            await Task.Delay(viewModel.MoveDelay * 1000);

            //Losowy wybór
            var fieldsLeftCount = viewModel.BoardSize - viewModel.BoardItems.Count();
            var position = _random.Next(viewModel.BoardItems.Count());

            var item = new BoardItem() { Value = viewModel.BoardItems.Count() };
            viewModel.BoardItems.Insert(position, item);
            viewModel.SelectedBoardItem = item;
        }
    }
}
