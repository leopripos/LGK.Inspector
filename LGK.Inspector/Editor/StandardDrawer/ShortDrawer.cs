// See LICENSE file in the root directory
//

using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class ShortDrawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(short); }
        }

        public object Draw(IMemberInfo memberInfo, object memberValue, string label)
        {
            var value = (short)memberValue;

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