namespace Terriflux.Programs.Alert
{
    public interface IAlert
    {
        /// <summary>
        /// Displays a message on the screen.
        /// </summary>
        /// <param name="message"></param>
        void Say(string message);

        /// <summary>
        /// Close the actual displayed message.
        /// </summary>
        void Close();
    }
}