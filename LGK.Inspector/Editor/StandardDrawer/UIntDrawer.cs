// See LICENSE file in the root directory
//

using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class UIntDrawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(uint); }
        }

        public void Draw(IFieldInfo fieldInfo, object owner)
        {
            var value = (uint)fieldInfo.GetValue(owner);

            var newValue = CorrectValue(EditorGUILayout.LongField(fieldInfo.Name, value));

            if (value != newValue)
                fieldInfo.SetValue(owner, newValue);
        }

        public void Draw(IPropertyInfo propertyInfo, object owner)
        {
            var value = (uint)propertyInfo.GetValue(owner);

            if (propertyInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(propertyInfo.Name, value.ToString());
            }
            else
            {
                var newValue = CorrectValue(EditorGUILayout.LongField(propertyInfo.Name, value));

                if (value != newValue)
                    propertyInfo.SetValue(owner, newValue);
            }
        }

        uint CorrectValue(long value)
        {
            if (value > uint.MaxValue)
                return uint.MaxValue;
            if (value < uint.MinValue)
                return uint.MinValue;

            return (uint)value;
        }
    }
}