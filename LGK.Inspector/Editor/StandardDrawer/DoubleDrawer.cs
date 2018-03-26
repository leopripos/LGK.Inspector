// See LICENSE file in the root directory
//

using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class DoubleDrawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(int); }
        }

        public void Draw(IFieldInfo fieldInfo, object owner)
        {
            var value = (double)fieldInfo.GetValue(owner);

            var newValue = EditorGUILayout.DoubleField(fieldInfo.Name, value);

            if (value != newValue)
                fieldInfo.SetValue(owner, newValue);
        }

        public void Draw(IPropertyInfo propertyInfo, object owner)
        {
            var value = (double)propertyInfo.GetValue(owner);

            if (propertyInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(propertyInfo.Name, value.ToString());
            }
            else
            {
                var newValue = EditorGUILayout.DoubleField(propertyInfo.Name, value);

                if (value != newValue)
                    propertyInfo.SetValue(owner, newValue);
            }
        }
    }
}