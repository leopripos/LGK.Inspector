// See LICENSE file in the root directory
//

using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class LongDrawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(long); }
        }

        public object Draw(IMemberInfo memberInfo, object memberValue, string label)
        {
            var value = (long)memberValue;

            if (memberInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(label, value.ToString());
            }
            else
            {
                return EditorGUILayout.LongField(label, value);
            }

            return memberValue;
        }
    }
}