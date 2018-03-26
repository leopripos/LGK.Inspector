// See LICENSE file in the root directory
//

using System;
using ReflectionProperty = System.Reflection.PropertyInfo;

namespace LGK.Inspector
{
    public interface IPropertyInfo
    {
        string Name { get; }

        Type PropertyType { get; }

        bool IsReadOnly { get; }

        object GetValue(object owner);

        void SetValue(object owner, object value);
    }

    public class ReflectionPropertyInfoWrapper : IPropertyInfo
    {
        readonly ReflectionProperty m_ReflectionPropertyInfo;

        public string Name
        {
            get { return m_ReflectionPropertyInfo.Name; }
        }

        public Type PropertyType
        {
            get { return m_ReflectionPropertyInfo.PropertyType; }
        }

        public bool IsReadOnly
        {
            get { return !m_ReflectionPropertyInfo.CanWrite; }
        }

        public object GetValue(object owner)
        {
            return m_ReflectionPropertyInfo.GetValue(owner, null);
        }

        public void SetValue(object owner, object value)
        {
            m_ReflectionPropertyInfo.SetValue(owner, value, null);
        }

        public ReflectionPropertyInfoWrapper(ReflectionProperty reflectionFieldInfo)
        {
            m_ReflectionPropertyInfo = reflectionFieldInfo;
        }
    }
}
