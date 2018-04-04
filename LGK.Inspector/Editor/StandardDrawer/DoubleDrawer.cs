// See LICENSE file in the root directory
//

using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class DoubleDrawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(int); }
        }

        public object Draw(IMemberInfo memberInfo, object memberValue, string label)
        {
            var value = (double)memberValue;

            if (memberInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(label, value.ToString());
            }
            else
            {
                return EditorGUILayout.DoubleField(label, value);
            }

            return memberInfo;
        }
    }
}