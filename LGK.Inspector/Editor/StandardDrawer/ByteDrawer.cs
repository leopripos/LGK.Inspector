// See LICENSE file in the root directory
//

using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class ByteDrawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(byte); }
        }

        public void Draw(IFieldInfo fieldInfo, object owner)
        {
            var value = (byte)fieldInfo.GetValue(owner);

            var newValue = CorrectValue(EditorGUILayout.IntField(fieldInfo.Name, value));

            if (value != newValue)
                fieldInfo.SetValue(owner, newValue);
        }

        public void Draw(IPropertyInfo propertyInfo, object owner)
        {
            var value = (byte)propertyInfo.GetValue(owner);

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

        byte CorrectValue(int value)
        {
            if (value > byte.MaxValue)
                return byte.MaxValue;
            if (value < byte.MinValue)
                return byte.MinValue;

            return (byte)value;
        }
    }
}