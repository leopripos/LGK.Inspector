// See LICENSE file in the root directory
//

using LGK.Inspector.Internal;
using System;
using ReflectionField = System.Reflection.FieldInfo;

namespace LGK.Inspector
{
    public class StandardFieldInfo : IInternalMemberInfo
    {
        readonly ReflectionField m_ReflectionFieldInfo;

        public string Name {
            get;
            private set;
        }

        public Type TargetType
        {
            get { return m_ReflectionFieldInfo.FieldType; }
        }

        public bool IsReadOnly
        {
            get { return false; }
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
            return m_ReflectionFieldInfo.GetValue(owner);
        }

        public void SetValue(object owner, object value)
        {
            m_ReflectionFieldInfo.SetValue(owner, value);
        }

        public StandardFieldInfo(ReflectionField reflectionFieldInfo, bool isContainer)
        {
            if (reflectionFieldInfo.FieldType.IsArray)
            {
                Name = reflectionFieldInfo.Name + " []";
            }
            else {
                Name = reflectionFieldInfo.Name;
            }

            m_ReflectionFieldInfo = reflectionFieldInfo;
            
            IsContainer = isContainer;
        }
    }
}

