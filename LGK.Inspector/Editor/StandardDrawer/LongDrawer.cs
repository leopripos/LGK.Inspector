// See LICENSE file in the root directory
//

using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class LongDrawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(long); }
        }

        public void Draw(IFieldInfo fieldInfo, object owner)
        {
            var value = (long)fieldInfo.GetValue(owner);

            var newValue = EditorGUILayout.LongField(fieldInfo.Name, value);

            if (value != newValue)
                fieldInfo.SetValue(owner, newValue);
        }

        public void Draw(IPropertyInfo propertyInfo, object owner)
        {
            var value = (long)propertyInfo.GetValue(owner);

            if (propertyInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(propertyInfo.Name, value.ToString());
            }
            else
            {
                var newValue = EditorGUILayout.LongField(propertyInfo.Name, value);

                if (value != newValue)
                    propertyInfo.SetValue(owner, newValue);
            }
        }
    }
}