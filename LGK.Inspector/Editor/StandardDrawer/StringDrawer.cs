// See LICENSE file in the root directory
//

using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class StringDrawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(short); }
        }

        public object Draw(IMemberInfo memberInfo, object memberValue)
        {
            var value = (string)memberValue;

            if (memberInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(memberInfo.Name, value);
            }
            else
            {
                return EditorGUILayout.TextField(memberInfo.Name, value);
            }

            return memberValue;
        }

        short CorrectValue(int value)
        {
            if (value > short.MaxValue)
                return short.MaxValue;
            if (value < short.MinValue)
                return short.MinValue;

            return (short)value;
        }
    }
}