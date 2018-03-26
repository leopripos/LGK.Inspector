// See LICENSE file in the root directory
//

using System;

namespace LGK.Inspector
{
    public interface ITypeDrawer
    {
        Type TargetType { get; }

        void Draw(IFieldInfo fieldInfo, object owner);

        void Draw(IPropertyInfo propertyInfo, object owner);
    }
}

