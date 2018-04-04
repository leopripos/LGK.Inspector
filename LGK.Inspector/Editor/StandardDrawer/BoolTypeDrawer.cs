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

        public object Draw(IMemberInfo memberInfo, object memberValue, string label)
        {
            var value = (bool)memberValue;

            if (memberInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(label, value.ToString());
            }
            else
            {
                return EditorGUILayout.Toggle(label, value);
            }

            return memberValue;
        }
    }
}