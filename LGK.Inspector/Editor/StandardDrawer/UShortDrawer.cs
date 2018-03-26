// See LICENSE file in the root directory
//

using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class UShortDrawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(ushort); }
        }

        public void Draw(IFieldInfo fieldInfo, object owner)
        {
            var value = (ushort)fieldInfo.GetValue(owner);

            var newValue = CorrectValue(EditorGUILayout.IntField(fieldInfo.Name, value));

            if (value != newValue)
                fieldInfo.SetValue(owner, newValue);
        }

        public void Draw(IPropertyInfo propertyInfo, object owner)
        {
            var value = (ushort)propertyInfo.GetValue(owner);

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

        ushort CorrectValue(int value)
        {
            if (value > ushort.MaxValue)
                return ushort.MaxValue;
            if (value < ushort.MinValue)
                return ushort.MinValue;

            return (ushort)value;
        }
    }
}