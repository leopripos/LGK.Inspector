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

        public object Draw(IMemberInfo memberInfo, object memberValue)
        {
            var value = (Vector2)memberValue;

            if (memberInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(memberInfo.Name, value.ToString());
            }
            else
            {
                var newValue = EditorGUILayout.Vector2Field(memberInfo.Name, value);

                if (value.x != newValue.x || value.y != newValue.y)
                    return newValue;
            }

            return memberValue;
        }
    }
}
