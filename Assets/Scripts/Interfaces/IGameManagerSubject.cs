using UnityEngine.EventSystems;

namespace UnityEngine
{
    public interface IGameManagerSubject: IEventSystemHandler

    {
        void OnAttach(Transform attachment);
    }
}