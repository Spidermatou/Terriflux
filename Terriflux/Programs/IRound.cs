using Godot;

namespace Terriflux.Programs
{
    public interface IRound : IPlaceMediator
    {
        /// <returns>The round _number.</returns>
        int GetNumber();

        /// <summary>
        /// Go to next turn.
        /// </summary>
        void Next();
    }
}