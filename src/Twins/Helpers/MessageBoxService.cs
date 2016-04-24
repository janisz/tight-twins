namespace Twins.Helpers
{
    public class MessageBoxService : IMessageBoxService
    {
        public void ShowMessage(string text, string caption)
        {
            System.Windows.MessageBox.Show(text, caption);
        }

    }
}