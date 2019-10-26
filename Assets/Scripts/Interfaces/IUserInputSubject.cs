using Enums;
using UnityEngine.EventSystems;

namespace UnityEngine
{
    public interface IUserInputSubject: IEventSystemHandler
    {
        void OnUserInput(InputDirection direction);
    }
}