using UnityEngine;
using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class Vector3Drawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(Vector3); }
        }

        public void Draw(IFieldInfo fieldInfo, object owner)
        {
            var value = (Vector3)fieldInfo.GetValue(owner);

            var newValue = EditorGUILayout.Vector3Field(fieldInfo.Name, value);

            if (value.x != newValue.x || value.y != newValue.y || value.z != newValue.z)
                fieldInfo.SetValue(owner, newValue);
        }

        public void Draw(IPropertyInfo propertyInfo, object owner)
        {
            var value = (Vector3)propertyInfo.GetValue(owner);

            if (propertyInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(propertyInfo.Name, value.ToString());
            }
            else
            {
                var newValue = EditorGUILayout.Vector3Field(propertyInfo.Name, value);

                if (value.x != newValue.x || value.y != newValue.y || value.z != newValue.z)
                    propertyInfo.SetValue(owner, newValue);
            }
        }
    }
}
