using System;
using System.Linq;
using System.Threading.Tasks;

namespace Twins.Players
{
    public class FirstPlayer : IPlayer
    {
        private Random _random = new Random(DateTime.Now.Millisecond);

        public async Task Move(MainViewModel viewModel)
        {
            await Task.Delay(viewModel.MoveDelay * 1000);

            //Losowy wybór
            var fieldsLeftCount = viewModel.BoardItems.Count(p => p.Color == null);
            var position = _random.Next(fieldsLeftCount);

            for (int i = 0, j = 0; i < viewModel.BoardItems.Count; ++i)
                if (viewModel.BoardItems[i].Color == null)
                {
                    if (j == position)
                    {
                        viewModel.SelectedBoardItem = viewModel.BoardItems[i];
                        return;
                    }
                    ++j;
                }
        }
    }
}
