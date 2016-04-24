using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Color = Twins.Model.Color;

namespace Twins.Players
{
    public class SecondPlayer : IPlayer
    {
        public async Task Move(MainViewModel viewModel)
        {
            await Task.Delay(viewModel.MoveDelay * 1000);

            //Znaki nie powodujące przegranej
            var list = new List<Color>();
            foreach (var color in viewModel.Colors)
            {
                viewModel.SelectedBoardItem.Color = color.Index;
                if (!TwinsChecker.CheckTwins(viewModel.BoardItems))
                {
                    viewModel.SelectedBoardItem.Color = null;
                    list.Add(color);
                }
            }

            //Każdy wybór jest przygrywający
            if (!list.Any())
                viewModel.SelectedColor = viewModel.Colors[0];
            else
            {
                //Losowy wybór nie przegrywający
                var random = new Random(DateTime.Now.Millisecond);
                var colorIndex = random.Next(list.Count);
                viewModel.SelectedColor = viewModel.Colors[colorIndex];
            }
        }
    }
}
