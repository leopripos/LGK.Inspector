// See LICENSE file in the root directory
//

using LGK.Inspector.Internal;
using System;
using ReflectionProperty = System.Reflection.PropertyInfo;

namespace LGK.Inspector
{
    public class StandardPropertyInfo : IInternalMemberInfo
    {
        readonly ReflectionProperty m_ReflectionPropertyInfo;

        public string Name
        {
            get;
            private set;
        }

        public Type TargetType
        {
            get { return m_ReflectionPropertyInfo.PropertyType; }
        }

        public bool IsReadOnly
        {
            get { return !m_ReflectionPropertyInfo.CanWrite; }
        }

        public bool IsContainer
        {
            get;
            private set;
        }

        public byte ChildCount
        {
            get;
            internal set;
        }

        public object GetValue(object owner)
        {
            return m_ReflectionPropertyInfo.GetValue(owner, null);
        }

        public void SetValue(object owner, object value)
        {
            m_ReflectionPropertyInfo.SetValue(owner, value, null);
        }

        public StandardPropertyInfo(ReflectionProperty reflectionFieldInfo, bool isContainer)
        {
            m_ReflectionPropertyInfo = reflectionFieldInfo;

            if (reflectionFieldInfo.PropertyType.IsArray)
            {
                Name = m_ReflectionPropertyInfo.Name + " []";
            }
            else {
                Name = m_ReflectionPropertyInfo.Name;
            }

            IsContainer = isContainer;
        }
    }
}
