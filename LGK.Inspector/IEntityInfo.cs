// See LICENSE file in the root directory
//

namespace LGK.Inspector
{
    public interface IEntityInfo
    {
        int Id { get; }

        IComponentInfo[] Components { get; }

        bool ToggleView { get; set; }
    }


    public class EntityInfo : IEntityInfo
    {
        public int Id { get; private set; }

        public IComponentInfo[] Components { get; private set; }

        public bool ToggleView { set; get; }

        public EntityInfo(int id, int componentCount)
        {
            this.Id = id;
            this.Components = new ComponentInfo[componentCount];
        }
    }
}

