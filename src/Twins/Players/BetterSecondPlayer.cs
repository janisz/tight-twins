using System.Linq;
using System.Threading.Tasks;

namespace Twins.Players
{
    public class BetterSecondPlayer : IPlayer
    {
        public async Task Move(MainViewModel viewModel)
        {
            await Task.Delay(viewModel.MoveDelay * 1000);

            //Wybór znaku którego jeszcze nie było
            var notSelectedColor = viewModel.Colors.FirstOrDefault(p => !viewModel.BoardItems.Any(q => q.Color != null && q.Color == p.Index));

            if (notSelectedColor != null)
            {
                viewModel.SelectedColor = notSelectedColor;
                return;
            }

            //Pierwszy znak nie powodujący przegranej
            foreach (var color in viewModel.Colors)
            {
                viewModel.SelectedBoardItem.Color = color.Index;
                if (!TwinsChecker.CheckTwins(viewModel.BoardItems))
                {
                    viewModel.SelectedBoardItem.Color = null;
                    viewModel.SelectedColor = color;
                    return;
                }
            }

            //Każdy wybór jest przygrywający
            viewModel.SelectedColor = viewModel.Colors[0];
        }
    }
}
