// See LICENSE file in the root directory
//

using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class SByteDrawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(sbyte); }
        }

        public void Draw(IFieldInfo fieldInfo, object owner)
        {
            var value = (sbyte)fieldInfo.GetValue(owner);

            var newValue = CorrectValue(EditorGUILayout.IntField(fieldInfo.Name, value));

            if (value != newValue)
                fieldInfo.SetValue(owner, newValue);
        }

        public void Draw(IPropertyInfo propertyInfo, object owner)
        {
            var value = (sbyte)propertyInfo.GetValue(owner);

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

        sbyte CorrectValue(int value)
        {
            if (value > sbyte.MaxValue)
                return sbyte.MaxValue;
            if (value < sbyte.MinValue)
                return sbyte.MinValue;

            return (sbyte)value;
        }
    }
}