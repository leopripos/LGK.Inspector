// See LICENSE file in the root directory
//

using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class CharDrawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(short); }
        }

        public object Draw(IMemberInfo memberInfo, object memberValue)
        {
            var value = (char)memberValue;

            if (memberInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(memberInfo.Name, value.ToString());
            }
            else
            {
                return CorrectValue(EditorGUILayout.TextField(memberInfo.Name, value.ToString()));
            }

            return memberValue;
        }

        char CorrectValue(string value)
        {
            if (value.Length == 0)
                return default(char);
            else
                return value[0];
        }
    }
}