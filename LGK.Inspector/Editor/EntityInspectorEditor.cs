// See LICENSE file in the root directory
//

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LGK.Inspector
{
    [CustomEditor(typeof(EntityInspector))]
    [InitializeOnLoad]
    public class EntityInspectorEditor : Editor
    {
        static readonly IComponentDrawer m_DefaultComponentDrawer;

        static readonly Dictionary<Type, IComponentDrawer> m_ComponentDrawers;
        static readonly Dictionary<Type, ITypeDrawer> m_TypeDrawers;

        static EntityInspectorEditor()
        {
            m_TypeDrawers = DrawerCollector.CollectTypeDrawers();

            m_ComponentDrawers = DrawerCollector.CollectComponentDrawers(m_TypeDrawers);

            m_DefaultComponentDrawer = new DefaultComponentDrawer(m_TypeDrawers);

            InspectorUtility.Setup(m_TypeDrawers);
        }

        public override void OnInspectorGUI()
        {
            var entityInspector = target as EntityInspector;
            var prefixName = entityInspector.gameObject.name + " ";

            EditorGUILayout.Space();

            var items = entityInspector.Items;
            var itemCount = items.Count;
            for (int eIndex = 0; eIndex < itemCount; eIndex++)
            {
                EditorGUILayout.Space();
                DrawEntity(items[eIndex], prefixName);
            }

            EditorGUILayout.Space();
        }

        void DrawEntity(IEntityInfo entityInfo, string prefixName)
        {
            var entityName = prefixName + entityInfo.Id.ToString();
            entityInfo.ToggleView = GUILayout.Toggle(entityInfo.ToggleView, entityName, EditorStyles.toolbarButton);

            if (entityInfo.ToggleView)
            {
                IComponentDrawer componentDrawer;
                var defaultIndentLevel = EditorGUI.indentLevel + 1;

                var components = entityInfo.Components;
                for (int cIndex = 0; cIndex < components.Length; cIndex++)
                {
                    EditorGUI.indentLevel = defaultIndentLevel;

                    var componentInfo = components[cIndex];
                    if (m_ComponentDrawers.TryGetValue(componentInfo.ComponentType, out componentDrawer))
                        componentDrawer.Draw(componentInfo);
                    else
                        DrawDefault(componentInfo);
                }

                EditorGUI.indentLevel = defaultIndentLevel;
            }
        }

        void DrawDefault(IComponentInfo componentInfo)
        {
            m_DefaultComponentDrawer.Draw(componentInfo);
        }
    }
}

