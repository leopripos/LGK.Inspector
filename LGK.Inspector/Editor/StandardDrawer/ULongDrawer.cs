// See LICENSE file in the root directory
//

using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class ULongDrawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(ulong); }
        }

        public object Draw(IMemberInfo memberInfo, object memberValue)
        {
            var value = (ulong)memberValue;

            EditorGUILayout.LabelField(memberInfo.Name, value.ToString());

            return memberValue;
        }
    }
}