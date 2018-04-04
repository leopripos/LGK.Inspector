// See LICENSE file in the root directory
//

using LGK.Inspector.Internal;
using System;
using System.Collections.Generic;
using UnityEditor;

namespace LGK.Inspector
{
    public class DefaultComponentDrawer : IComponentDrawer
    {
        readonly Dictionary<Type, ITypeDrawer> m_TypeDrawers;
        static Dictionary<Type, IDictionary<int, bool>> m_ComponentToggles = new Dictionary<Type, IDictionary<int, bool>>();

        public DefaultComponentDrawer(Dictionary<Type, ITypeDrawer> typeDrawers)
        {
            m_TypeDrawers = typeDrawers;
        }

        public void Draw(IComponentInfo componentInfo)
        {
            componentInfo.ToggleView = EditorGUILayout.Foldout(componentInfo.ToggleView, componentInfo.Name, EditorStyles.foldout);

            if (componentInfo.ToggleView)
            {
                EditorGUI.indentLevel++;

                var componentToggle = ResolveComponentToggle(componentInfo);
                
                DrawMember(componentInfo.Members, componentInfo.Value, componentToggle);

                EditorGUI.indentLevel--;
            }
        }

        IDictionary<int, bool> ResolveComponentToggle(IComponentInfo componentInfo)
        {
            IDictionary<int, bool> componentToggle;

            if (!m_ComponentToggles.TryGetValue(componentInfo.ComponentType, out componentToggle))
            {
                componentToggle = new Dictionary<int, bool>();
                var members = componentInfo.Members;
                for (int i = 0; i < members.Count; i++)
                {
                    var member = members[i];

                    if (member.IsContainer || member.TargetType.IsArray)
                    {
                        componentToggle.Add(i, false);
                    }
                }

                m_ComponentToggles.Add(componentInfo.ComponentType, componentToggle);
            }

            return componentToggle;
        }

        void DrawMember(IList<IInternalMemberInfo> members, object owner, IDictionary<int, bool> componentToggle)
        {
            int memberId = 0;
            while (memberId < members.Count)
            {
                var member = members[memberId];

                if (member.IsContainer)
                {
                    componentToggle[memberId] = EditorGUILayout.Foldout(componentToggle[memberId], member.Name);

                    var value = member.GetValue(owner);
                    if (value != null && componentToggle[memberId])
                        DrawChildMember(members, value, ref memberId, member.ChildCount, componentToggle);
                    else
                        memberId += member.ChildCount; // Skiping
                }
                else {
                    DrawMember(members[memberId], owner, memberId, componentToggle);
                }

                memberId++;
            }
        }

        void DrawChildMember(IList<IInternalMemberInfo> members, object owner, ref int memberId, int count, IDictionary<int, bool> componentToggle)
        {
            EditorGUI.indentLevel++;

            var lastMemberId = memberId + count;
            while (memberId < lastMemberId)
            {
                memberId++;

                var member = members[memberId];

                if (member.IsContainer)
                {
                    componentToggle[memberId] = EditorGUILayout.Foldout(componentToggle[memberId], member.Name);

                    var value = member.GetValue(owner);
                    if (value != null && componentToggle[memberId])
                        DrawChildMember(members, value, ref memberId, member.ChildCount, componentToggle);
                    else
                        memberId += member.ChildCount; // Skiping
                }
                else
                {
                    DrawMember(members[memberId], owner, memberId, componentToggle);
                }
            }

            EditorGUI.indentLevel--;
        }

        void DrawMember(IInternalMemberInfo memberInfo, object owner, int memberId, IDictionary<int, bool> componentToggle)
        {
            if (memberInfo.TargetType.IsArray)
                DrawArray(memberInfo, owner, memberId, componentToggle);
            else if (memberInfo.TargetType.IsEnum)
                DrawEnum(memberInfo, owner);
            else
            {
                DrawType(memberInfo, owner, memberInfo.Name);
            }
        }

        void DrawArray(IInternalMemberInfo memberInfo, object owner, int memberId, IDictionary<int, bool> componentToggle)
        {
            componentToggle[memberId] = EditorGUILayout.Foldout(componentToggle[memberId], memberInfo.Name);

            var value = (Array)memberInfo.GetValue(owner);

            if (value != null && componentToggle[memberId])
            {
                ITypeDrawer typeDrawer;

                EditorGUI.indentLevel++;

                if (m_TypeDrawers.TryGetValue(memberInfo.TargetType.GetElementType(), out typeDrawer))
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        var itemValue = value.GetValue(i);
                        var newItemValue = typeDrawer.Draw(memberInfo, itemValue, "[" + i.ToString() + "]");

                        if (newItemValue != value)
                            value.SetValue(newItemValue, i);
                    }
                }
                else
                {
                    var array = (Array)memberInfo.GetValue(owner);
                    for (int i = 0; i < array.Length; i++)
                    {
                        DrawDefault(memberInfo, array.GetValue(i), "[" + i.ToString() + "]");
                    }
                }

                EditorGUI.indentLevel--;
            }
        }

        void DrawEnum(IInternalMemberInfo memberInfo, object owner)
        {
            var value = (Enum)memberInfo.GetValue(owner);

            if (memberInfo.IsReadOnly)
            {
                EditorGUILayout.LabelField(memberInfo.Name, value.ToString());
            }
            else
            {
                var newValue = EditorGUILayout.EnumPopup(memberInfo.Name, value);

                if (value != newValue)
                    memberInfo.SetValue(owner, newValue);
            }
        }

        void DrawType(IInternalMemberInfo memberInfo, object owner, string label)
        {
            var value = memberInfo.GetValue(owner);

            ITypeDrawer typeDrawer;
            if (m_TypeDrawers.TryGetValue(memberInfo.TargetType, out typeDrawer))
            {
                var newValue = typeDrawer.Draw(memberInfo, value, label);

                if (newValue != value)
                    memberInfo.SetValue(owner, newValue);
            }
            else
                DrawDefault(memberInfo, value, label);
        }

        void DrawDefault(IInternalMemberInfo memberInfo, object memberValue, string label)
        {
            if (memberInfo.TargetType.IsValueType)
            {
                EditorGUILayout.LabelField(label, memberValue.ToString());
            }
            else
            {
                EditorGUILayout.LabelField(label, memberValue == null ? "null (" + memberInfo.TargetType.Name + ")" : memberInfo.TargetType.Name);
            }
        }
    }
}