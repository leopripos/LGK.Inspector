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

        public void Draw(IFieldInfo fieldInfo, object owner)
        {
            var value = (bool)fieldInfo.GetValue(owner);

            if (EditorGUILayout.Toggle(fieldInfo.Name, value) != value)
            {
                fieldInfo.SetValue(owner, !value);
            }
        }

        public void Draw(IPropertyInfo propertyInfo, object owner)
        {
            var value = (bool)propertyInfo.GetValue(owner);

            if (propertyInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(propertyInfo.Name, value.ToString());
            }
            else if (EditorGUILayout.Toggle(propertyInfo.Name, value) != value)
            {
                propertyInfo.SetValue(owner, !value);
            }
        }
    }
}