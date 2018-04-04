// See LICENSE file in the root directory
//

using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class UShortDrawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(ushort); }
        }

        public object Draw(IMemberInfo memberInfo, object memberValue, string label)
        {
            var value = (ushort)memberValue;

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

        ushort CorrectValue(int value)
        {
            if (value > ushort.MaxValue)
                return ushort.MaxValue;
            if (value < ushort.MinValue)
                return ushort.MinValue;

            return (ushort)value;
        }
    }
}