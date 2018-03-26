// See LICENSE file in the root directory
//

using System;
using ReflectionField = System.Reflection.FieldInfo;

namespace LGK.Inspector
{
    public interface IFieldInfo
    {
        string Name { get; }

        Type FieldType { get; }

        object GetValue(object owner);

        void SetValue(object owner, object value);
    }

    public class ReflectionFieldInfoWrapper : IFieldInfo
    {
        readonly ReflectionField m_ReflectionFieldInfo;

        public string Name { get; private set; }

        public Type FieldType
        {
            get { return m_ReflectionFieldInfo.FieldType; }
        }

        public object GetValue(object owner)
        {
            return m_ReflectionFieldInfo.GetValue(owner);
        }

        public void SetValue(object owner, object value)
        {
            m_ReflectionFieldInfo.SetValue(owner, value);
        }

        public ReflectionFieldInfoWrapper(ReflectionField reflectionFieldInfo)
        {
            Name = InspectorUtility.GetCleanName(reflectionFieldInfo.Name);
            m_ReflectionFieldInfo = reflectionFieldInfo;
        }
    }
}

