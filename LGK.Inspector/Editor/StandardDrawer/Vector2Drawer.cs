using UnityEngine;
using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class Vector2Drawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(Vector2); }
        }

        public void Draw(IFieldInfo fieldInfo, object owner)
        {
            var value = (Vector2)fieldInfo.GetValue(owner);
            var newValue = EditorGUILayout.Vector2Field(fieldInfo.Name, value);

            if (value.x != newValue.x || value.y != newValue.y)
                fieldInfo.SetValue(owner, newValue);
        }

        public void Draw(IPropertyInfo propertyInfo, object owner)
        {
            var value = (Vector2)propertyInfo.GetValue(owner);

            if (propertyInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(propertyInfo.Name, value.ToString());
            }
            else
            {
                var newValue = EditorGUILayout.Vector2Field(propertyInfo.Name, value);

                if (value.x != newValue.x || value.y != newValue.y)
                    propertyInfo.SetValue(owner, newValue);
            }
        }
    }
}
