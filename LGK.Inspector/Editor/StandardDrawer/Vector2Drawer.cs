using UnityEngine;
using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class Vector2Drawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(Vector2); }
        }

        public object Draw(IMemberInfo memberInfo, object memberValue, string label)
        {
            var value = (Vector2)memberValue;

            if (memberInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(label, value.ToString());
            }
            else
            {
                var newValue = EditorGUILayout.Vector2Field(label, value);

                if (value.x != newValue.x || value.y != newValue.y)
                    return newValue;
            }

            return memberValue;
        }
    }
}
