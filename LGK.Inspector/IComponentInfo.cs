// See LICENSE file in the root directory
//

using LGK.Inspector.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace LGK.Inspector
{
    public interface IComponentInfo
    {
        string Name { get; }

        Type ComponentType { get; }

        object Value { get; }

        IList<IInternalMemberInfo> Members { get; }

        bool ToggleView { get; set; }
    }

    public class ComponentInfo : IComponentInfo
    {
        public object Value { get; private set; }

        public string Name { get; private set; }

        public Type ComponentType { get; private set; }

        public IList<IInternalMemberInfo> Members { get; private set; }

        public bool ToggleView { get; set; }

        protected ComponentInfo(Object value, Type type)
        {
            Value = value;

            if (type.IsGenericType)
                Name = CreateGenericName(type);
            else
                Name = type.Name;

            ComponentType = type;
        }

        public ComponentInfo(Object value, Type type, IList<IInternalMemberInfo> members) 
            : this(value, type)
        {
            Members = members;
        }

        string CreateGenericName(Type type)
        {
            var builder = new StringBuilder();

            builder.Append(type.Name);
            builder.Append('<');

            var genericArguments = type.GetGenericArguments();
            for (int i = 0; i < genericArguments.Length; i++)
            {
                if (i != 0)
                    builder.Append(',');

                builder.Append(genericArguments[i].Name);
            }
            builder.Append('>');

            return builder.ToString();
        }
    }
}

