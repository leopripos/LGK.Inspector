// See LICENSE file in the root directory
//

using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace LGK.Inspector
{
    public class DefaultComponentDrawer : IComponentDrawer
    {
        readonly Dictionary<Type, ITypeDrawer> m_TypeDrawers;

        public DefaultComponentDrawer(Dictionary<Type, ITypeDrawer> typeDrawers)
        {
            m_TypeDrawers = typeDrawers;
        }

        public void Draw(IComponentInfo componentInfo)
        {
            componentInfo.ToggleView = GUILayout.Toggle(componentInfo.ToggleView, componentInfo.Name, EditorStyles.foldout);

            if (componentInfo.ToggleView)
            {
                EditorGUI.indentLevel++;
                DrawFields(componentInfo);
                DrawProperties(componentInfo);

                EditorGUI.indentLevel--;
            }
        }

        void DrawFields(IComponentInfo componentInfo)
        {
            ITypeDrawer typeDrawer;
            IFieldInfo field;

            var fields = componentInfo.Fields;
            for (int i = 0; i < fields.Length; i++)
            {
                field = fields[i];
                if (m_TypeDrawers.TryGetValue(field.FieldType, out typeDrawer))
                    typeDrawer.Draw(field, componentInfo.Value);
                else if (field.FieldType.IsEnum)
                    DrawEnum(field, componentInfo.Value);
                else
                    DrawDefault(field, componentInfo.Value);

            }
        }

        void DrawProperties(IComponentInfo componentInfo)
        {
            ITypeDrawer typeDrawer;
            IPropertyInfo property;

            var properties = componentInfo.Properties;
            for (int i = 0; i < properties.Length; i++)
            {
                property = properties[i];
                if (m_TypeDrawers.TryGetValue(property.PropertyType, out typeDrawer))
                    typeDrawer.Draw(property, componentInfo.Value);
                else if (property.PropertyType.IsEnum)
                    DrawEnum(property, componentInfo.Value);
                else
                    DrawDefault(property, componentInfo.Value);

            }
        }

        void DrawEnum(IFieldInfo fieldInfo, object owner)
        {
            var value = (Enum)fieldInfo.GetValue(owner);

            var newValue = EditorGUILayout.EnumPopup(fieldInfo.Name, value);

            if (value != newValue)
                fieldInfo.SetValue(owner, newValue);
        }

        void DrawEnum(IPropertyInfo propertyInfo, object owner)
        {
            var value = (Enum)propertyInfo.GetValue(owner);

            if (propertyInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(propertyInfo.Name, value.ToString());
            }
            else
            {
                var newValue = EditorGUILayout.EnumPopup(propertyInfo.Name, value);

                if (value != newValue)
                    propertyInfo.SetValue(owner, newValue);
            }
        }

        void DrawDefault(IFieldInfo fieldInfo, object owner)
        {
            if (fieldInfo.FieldType.IsValueType)
            {
                EditorGUILayout.LabelField(fieldInfo.Name, fieldInfo.GetValue(owner).ToString());
            }
            else
            {
                var value = fieldInfo.GetValue(owner);

                EditorGUILayout.LabelField(fieldInfo.Name, value == null ? "null (" + fieldInfo.FieldType.Name + ")" : fieldInfo.FieldType.Name);
            }
        }

        void DrawDefault(IPropertyInfo propertyInfo, object owner)
        {
            if (propertyInfo.PropertyType.IsValueType)
            {
                EditorGUILayout.LabelField(propertyInfo.Name, propertyInfo.GetValue(owner).ToString());
            }
            else
            {
                var value = propertyInfo.GetValue(owner);

                EditorGUILayout.LabelField(propertyInfo.Name, value == null ? "null (" + propertyInfo.PropertyType.Name + ")" : propertyInfo.PropertyType.Name);
            }
        }

    }

}