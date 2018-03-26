// See LICENSE file in the root directory
//

namespace LGK.Inspector
{
    public interface IInspectorService
    {
        void Register(int id, object[] components, byte modeMask = EInspectorMode.All);

        void Register(string group, int id, object[] components, byte modeMask = EInspectorMode.All);

        void Register(string group, IEntityInfo entityInfo);

        void Unregister(int id);

        void Unregister(string group, int id);
    }
}

