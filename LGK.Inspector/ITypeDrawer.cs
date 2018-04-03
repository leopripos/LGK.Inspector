// See LICENSE file in the root directory
//

using System;

namespace LGK.Inspector
{
    public interface ITypeDrawer
    {
        Type TargetType { get; }

        object Draw(IMemberInfo memberInfo, object memberValue);
    }
}

