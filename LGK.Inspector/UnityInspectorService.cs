// See LICENSE file in the root directory
//

using System.Collections.Generic;

namespace LGK.Inspector
{
    public class UnityInspectorService : IInspectorService
    {
        readonly Dictionary<string, EntityInspector> m_Holders = new Dictionary<string, EntityInspector>();

        public void Register(int id, object[] components, byte modeMask = EInspectorMode.All)
        {
            Register("Entities", id, components, modeMask);
        }

        public void Unregister(int id)
        {
            Unregister("Entities", id);
        }

        public void Register(string group, int id, object[] components, byte modeMask = EInspectorMode.All)
        {
            var entityInfo = InspectorUtility.ExtractEntityInfo(id, components, modeMask);

            AddToInspector(group, entityInfo);
        }

        public void Register(string group, IEntityInfo entityInfo)
        {
            AddToInspector(group, entityInfo);
        }

        public void Unregister(string group, int id)
        {
            m_Holders[group].Items.RemoveAll(item => item.Id == id);
        }

        void AddToInspector(string group, IEntityInfo entityInfo)
        {
            EntityInspector inspector;
            if (!m_Holders.TryGetValue(group, out inspector))
            {
                inspector = new UnityEngine.GameObject(group).AddComponent<EntityInspector>();
                m_Holders.Add(group, inspector);
            }

            inspector.Items.Add(entityInfo);
        }
    }
}

