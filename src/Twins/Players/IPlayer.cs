using System.Threading.Tasks;

namespace Twins.Players
{
    public interface IPlayer
    {
        Task Move(MainViewModel viewModel);
    }
}
