// See LICENSE file in the root directory
//

using System;

namespace LGK.Inspector
{
    public interface IMemberInfo
    {
        string Name { get; }

        Type TargetType { get; }

        bool IsReadOnly { get; }

        bool IsContainer { get;  }

        byte ChildCount { get; }
    }
}

namespace LGK.Inspector.Internal
{
    public interface IInternalMemberInfo : IMemberInfo
    {
        object GetValue(object owner);

        void SetValue(object owner, object value);
    }
}
