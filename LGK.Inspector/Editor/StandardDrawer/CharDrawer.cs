// See LICENSE file in the root directory
//

using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class CharDrawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(short); }
        }

        public void Draw(IFieldInfo fieldInfo, object owner)
        {
            var value = (char)fieldInfo.GetValue(owner);

            var newValue = CorrectValue(EditorGUILayout.TextField(fieldInfo.Name, value.ToString()));

            if (value != newValue)
                fieldInfo.SetValue(owner, newValue);
        }

        public void Draw(IPropertyInfo propertyInfo, object owner)
        {
            var value = (char)propertyInfo.GetValue(owner);

            if (propertyInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(propertyInfo.Name, value.ToString());
            }
            else
            {
                var newValue = CorrectValue(EditorGUILayout.TextField(propertyInfo.Name, value.ToString()));

                if (value != newValue)
                    propertyInfo.SetValue(owner, newValue);
            }
        }

        char CorrectValue(string value)
        {
            if (value.Length == 0)
                return default(char);
            else
                return value[0];
        }
    }
}