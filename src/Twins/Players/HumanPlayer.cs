using System.Threading.Tasks;

namespace Twins.Players
{
    public class HumanPlayer : IPlayer
    {
        public Task Move(MainViewModel viewModel)
        {
            var task = new TaskCompletionSource<object>();
            task.SetResult(null);
            return task.Task;
        }
    }
}
