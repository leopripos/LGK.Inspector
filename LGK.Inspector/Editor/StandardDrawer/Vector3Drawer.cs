using UnityEngine;
using System;
using UnityEditor;

namespace LGK.Inspector.StandardDrawer
{
    public class Vector3Drawer : ITypeDrawer
    {
        public Type TargetType
        {
            get { return typeof(Vector3); }
        }

        public object Draw(IMemberInfo memberInfo, object memberValue)
        {
            var value = (Vector3)memberValue;

            if (memberInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(memberInfo.Name, value.ToString());
            }
            else
            {
                var newValue = EditorGUILayout.Vector3Field(memberInfo.Name, value);

                if (value.x != newValue.x || value.y != newValue.y || value.z != newValue.z)
                    return newValue;
            }

            return memberValue;
        }
    }
}
