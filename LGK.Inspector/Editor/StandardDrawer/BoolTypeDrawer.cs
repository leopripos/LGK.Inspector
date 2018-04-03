// See LICENSE file in the root directory
//

using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class BoolTypeDrawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(bool); }
        }

        public object Draw(IMemberInfo memberInfo, object memberValue)
        {
            var value = (bool)memberValue;

            if (memberInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(memberInfo.Name, value.ToString());
            }
            else
            {
                return EditorGUILayout.Toggle(memberInfo.Name, value);
            }

            return memberValue;
        }
    }
}