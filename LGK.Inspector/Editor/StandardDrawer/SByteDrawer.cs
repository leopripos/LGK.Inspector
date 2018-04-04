// See LICENSE file in the root directory
//

using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class SByteDrawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(sbyte); }
        }

        public object Draw(IMemberInfo memberInfo, object memberValue, string label)
        {
            var value = (sbyte)memberValue;

            if (memberInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(label, value.ToString());
            }
            else
            {
                return CorrectValue(EditorGUILayout.IntField(label, value));
            }

            return memberValue;
        }

        sbyte CorrectValue(int value)
        {
            if (value > sbyte.MaxValue)
                return sbyte.MaxValue;
            if (value < sbyte.MinValue)
                return sbyte.MinValue;

            return (sbyte)value;
        }
    }
}