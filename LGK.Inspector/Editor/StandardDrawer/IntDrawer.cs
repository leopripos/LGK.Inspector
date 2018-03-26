// See LICENSE file in the root directory
//

using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class IntDrawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(int); }
        }

        public void Draw(IFieldInfo fieldInfo, object owner)
        {
            var value = (int)fieldInfo.GetValue(owner);

            var newValue = EditorGUILayout.IntField(fieldInfo.Name, value);

            if (value != newValue)
                fieldInfo.SetValue(owner, newValue);
        }

        public void Draw(IPropertyInfo propertyInfo, object owner)
        {
            var value = (int)propertyInfo.GetValue(owner);

            if (propertyInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(propertyInfo.Name, value.ToString());
            }
            else
            {
                var newValue = EditorGUILayout.IntField(propertyInfo.Name, value);

                if (value != newValue)
                    propertyInfo.SetValue(owner, newValue);
            }
        }
    }
}