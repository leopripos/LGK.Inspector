// See LICENSE file in the root directory
//

using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class ByteDrawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(byte); }
        }

        public object Draw(IMemberInfo memberInfo, object memberValue, string label)
        {
            var value = (byte)memberValue;

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

        byte CorrectValue(int value)
        {
            if (value > byte.MaxValue)
                return byte.MaxValue;
            if (value < byte.MinValue)
                return byte.MinValue;

            return (byte)value;
        }
    }
}