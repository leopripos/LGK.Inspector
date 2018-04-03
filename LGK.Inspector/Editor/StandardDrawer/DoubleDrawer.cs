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

        public object Draw(IMemberInfo memberInfo, object memberValue)
        {
            var value = (double)memberValue;

            if (memberInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(memberInfo.Name, value.ToString());
            }
            else
            {
                return EditorGUILayout.DoubleField(memberInfo.Name, value);
            }

            return memberInfo;
        }
    }
}