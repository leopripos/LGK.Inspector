// See LICENSE file in the root directory
//

using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class FloatDrawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(int); }
        }

        public object Draw(IMemberInfo memberInfo, object memberValue)
        {
            var value = (float)memberValue;

            if (memberInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(memberInfo.Name, value.ToString());
            }
            else
            {
                return EditorGUILayout.FloatField(memberInfo.Name, value);
            }

            return memberValue;
        }
    }
}