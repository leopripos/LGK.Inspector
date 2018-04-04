// See LICENSE file in the root directory
//

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LGK.Inspector
{
    public abstract class CustomComponentDrawer : IComponentDrawer
    {
        protected Dictionary<Type, ITypeDrawer> typeDrawers;

        internal void SetTypeDrawers(Dictionary<Type, ITypeDrawer> drawers)
        {
            this.typeDrawers = drawers;
        }

        public abstract Type TargetType { get; }

        void IComponentDrawer.Draw(IComponentInfo componentInfo)
        {
            componentInfo.ToggleView = EditorGUILayout.Foldout(componentInfo.ToggleView, componentInfo.Name, EditorStyles.foldout);

            if (componentInfo.ToggleView)
            {
                EditorGUI.indentLevel++;

                Draw(componentInfo);

                EditorGUI.indentLevel--;
            }
        }

        public abstract void Draw(IComponentInfo componentInfo);
    }
}