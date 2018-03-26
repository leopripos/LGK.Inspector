// See LICENSE file in the root directory
//

using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class StringDrawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(short); }
        }

        public void Draw(IFieldInfo fieldInfo, object owner)
        {
            var value = (string)fieldInfo.GetValue(owner);

            var newValue = EditorGUILayout.TextField(fieldInfo.Name, value);

            if (value != newValue)
                fieldInfo.SetValue(owner, newValue);
        }

        public void Draw(IPropertyInfo propertyInfo, object owner)
        {
            var value = (string)propertyInfo.GetValue(owner);

            if (propertyInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(propertyInfo.Name, value);
            }
            else
            {
                var newValue = EditorGUILayout.TextField(propertyInfo.Name, value);

                if (value != newValue)
                    propertyInfo.SetValue(owner, newValue);
            }
        }

        short CorrectValue(int value)
        {
            if (value > short.MaxValue)
                return short.MaxValue;
            if (value < short.MinValue)
                return short.MinValue;

            return (short)value;
        }
    }
}