// See LICENSE file in the root directory
//

using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class UIntDrawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(uint); }
        }

        public object Draw(IMemberInfo memberInfo, object memberValue, string label)
        {
            var value = (uint)memberValue;

            if (memberInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(label, value.ToString());
            }
            else
            {
                return CorrectValue(EditorGUILayout.LongField(label, value));
            }

            return memberValue;
        }

        uint CorrectValue(long value)
        {
            if (value > uint.MaxValue)
                return uint.MaxValue;
            if (value < uint.MinValue)
                return uint.MinValue;

            return (uint)value;
        }
    }
}