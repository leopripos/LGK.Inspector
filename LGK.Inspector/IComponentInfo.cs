// See LICENSE file in the root directory
//

using System;
using System.Text;

namespace LGK.Inspector
{
    public interface IComponentInfo
    {
        string Name { get; }

        Type ComponentType { get; }

        object Value { get; }

        IFieldInfo[] Fields { get; }

        IPropertyInfo[] Properties { get; }

        bool ToggleView { get; set; }
    }

    public class ComponentInfo : IComponentInfo
    {
        public object Value { get; private set; }

        public string Name { get; private set; }

        public Type ComponentType { get; private set; }

        public IFieldInfo[] Fields { get; private set; }

        public IPropertyInfo[] Properties { get; private set; }

        public bool ToggleView { get; set; }

        public ComponentInfo(Object value, Type type, IFieldInfo[] fields, IPropertyInfo[] properties)
        {
            Value = value;

            if (type.IsGenericType)
                Name = CreateGenericName(type);
            else
                Name = type.Name;

            ComponentType = type;
            Fields = fields;
            Properties = properties;
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

