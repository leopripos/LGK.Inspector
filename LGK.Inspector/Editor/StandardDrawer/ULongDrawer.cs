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

        public void Draw(IFieldInfo fieldInfo, object owner)
        {
            var value = (ulong)fieldInfo.GetValue(owner);

            EditorGUILayout.LabelField(fieldInfo.Name, value.ToString());
        }

        public void Draw(IPropertyInfo propertyInfo, object owner)
        {
            var value = (ulong)propertyInfo.GetValue(owner);

            EditorGUILayout.LabelField(propertyInfo.Name, value.ToString());
        }
    }
}