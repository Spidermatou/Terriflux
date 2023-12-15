namespace Terriflux.Programs;

public interface IAlert
{
    /// <summary>
    /// Displays a _message on the screen.
    /// </summary>
    /// <param name="message"></param>
    void Say(string message);

    /// <summary>
    /// Close the actual displayed _message.
    /// </summary>
    void Close();
}