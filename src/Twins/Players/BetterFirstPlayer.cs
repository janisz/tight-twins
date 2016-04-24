using System.Threading.Tasks;

namespace Twins.Players
{
    public class BetterFirstPlayer : FirstPlayer
    {
        public new Task Move(MainViewModel viewModel)
        {
            //TODO: właściwa implementacja

            return base.Move(viewModel);
        }
    }
}
