// See LICENSE file in the root directory
//

using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class ShortDrawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(short); }
        }

        public void Draw(IFieldInfo fieldInfo, object owner)
        {
            var value = (short)fieldInfo.GetValue(owner);

            var newValue = CorrectValue(EditorGUILayout.IntField(fieldInfo.Name, value));

            if (value != newValue)
                fieldInfo.SetValue(owner, newValue);
        }

        public void Draw(IPropertyInfo propertyInfo, object owner)
        {
            var value = (short)propertyInfo.GetValue(owner);

            if (propertyInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(propertyInfo.Name, value.ToString());
            }
            else
            {
                var newValue = CorrectValue(EditorGUILayout.IntField(propertyInfo.Name, value));

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