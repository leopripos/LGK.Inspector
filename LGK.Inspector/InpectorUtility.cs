// See LICENSE file in the root directory
//

using LGK.Inspector.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LGK.Inspector
{
    public static class InspectorUtility
    {
        const byte MAX_MEMBER_LEVEL = 3;

        static readonly Dictionary<Type, List<IInternalMemberInfo>> m_CachedFields = new Dictionary<Type, List<IInternalMemberInfo>>();
        static readonly Dictionary<Type, List<IInternalMemberInfo>> m_CachedProperties = new Dictionary<Type, List<IInternalMemberInfo>>();

        static Dictionary<byte, Dictionary<Type, ComponentCache>> m_CachedComponent = new Dictionary<byte, Dictionary<Type, ComponentCache>>()
        {
            { EInspectorMode.All, new Dictionary<Type, ComponentCache>() },
            { EInspectorMode.Field, new Dictionary<Type, ComponentCache>() },
            { EInspectorMode.Property, new Dictionary<Type,ComponentCache>() }
        };

        static Dictionary<Type, ITypeDrawer> m_TypeDrawers;

        public static void Setup(Dictionary<Type, ITypeDrawer> typeDrawers)
        {
            m_TypeDrawers = typeDrawers;
        }

        public static IEntityInfo ExtractEntityInfo(int id, object[] componensts, byte modeMask)
        {
            var entityInfo = new EntityInfo(id, componensts.Length);

            var count = componensts.Length;
            for (int i = 0; i < count; i++)
            {
                var componentValue = componensts[i];
                var componentType = componensts[i].GetType();

                entityInfo.Components[i] = ExtractComponentInfo(componentValue, componentType, modeMask);
            }

            return entityInfo;
        }

        static IComponentInfo ExtractComponentInfo(object componentValue, Type componentType, byte modeMask)
        {
            var cache = m_CachedComponent[modeMask];

            ComponentCache componentCache;
            if (cache.TryGetValue(componentType, out componentCache))
            {
                return new ComponentInfo(componentValue, componentType, componentCache.Members);
            }
            else {
                var memberCollector = new List<IInternalMemberInfo>(0);

                ExtractMember(componentType, modeMask, memberCollector, 0);

                cache.Add(componentType, new ComponentCache(memberCollector));

                return new ComponentInfo(componentValue, componentType, memberCollector);
            }
        }

        static void ExtractMember(Type type, byte modeMask, List<IInternalMemberInfo> memberCollector, byte level)
        {
            if ((modeMask & EInspectorMode.Field) == EInspectorMode.Field)
                ExtractFields(type, modeMask, memberCollector, level);

            if ((modeMask & EInspectorMode.Property) == EInspectorMode.Property)
                ExtractProperties(type, modeMask, memberCollector, level);
        }

        static void ExtractFields(Type type, byte modeMask, List<IInternalMemberInfo> memberCollector, byte level)
        {
            List<IInternalMemberInfo> fieldsCollector;
            if (!m_CachedFields.TryGetValue(type, out fieldsCollector))
            {
                var reflectionFields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly).Where(f => f.Name.IndexOf('<') == -1).ToArray();

                fieldsCollector = new List<IInternalMemberInfo>(reflectionFields.Length);

                m_CachedFields.Add(type, fieldsCollector);

                for (int i = 0; i < reflectionFields.Length; i++)
                {
                    var field = reflectionFields[i];
                    var fieldType = field.FieldType.IsArray ? field.FieldType.GetElementType() : field.FieldType;

                    if (fieldType.IsClass && !m_TypeDrawers.ContainsKey(fieldType) && level <= MAX_MEMBER_LEVEL)
                    {
                        var fieldInfo = new StandardFieldInfo(field, true);
                        fieldsCollector.Add(fieldInfo);

                        var lastSize = fieldsCollector.Count;
  
                        ExtractMember(fieldType, modeMask, fieldsCollector, (byte)(level + 1));

                        fieldInfo.ChildCount = (byte)(fieldsCollector.Count - lastSize);
                    }
                    else {
                        fieldsCollector.Add(new StandardFieldInfo(field, false));
                    }
                }
            }

            memberCollector.AddRange(fieldsCollector);
        }

        static void ExtractProperties(Type type, byte modeMask, List<IInternalMemberInfo> memberCollector, byte level)
        {
            List<IInternalMemberInfo> propertyCollector;
            if (!m_CachedProperties.TryGetValue(type, out propertyCollector))
            {
                var reflectionProperties = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly).Where(p => p.CanRead).ToArray();

                propertyCollector = new List<IInternalMemberInfo>(reflectionProperties.Length);

                m_CachedProperties.Add(type, propertyCollector);

                for (int i = 0; i < reflectionProperties.Length; i++)
                {
                    var property = reflectionProperties[i];
                    var propertyType = property.PropertyType.IsArray ? property.PropertyType.GetElementType() : property.PropertyType;

                    if (propertyType.IsClass && !m_TypeDrawers.ContainsKey(propertyType) && level <= MAX_MEMBER_LEVEL)
                    {
                        var propertyInfo = new StandardPropertyInfo(property, true);
                        propertyCollector.Add(propertyInfo);

                        var lastSize = propertyCollector.Count;

                        ExtractMember(propertyType, modeMask, propertyCollector, (byte)(level + 1));
                        
                        propertyInfo.ChildCount = (byte)(propertyCollector.Count - lastSize);
                    }
                    else
                    {
                        propertyCollector.Add(new StandardPropertyInfo(property, false));
                    }
                }
            }

            memberCollector.AddRange(propertyCollector);
        }

        class ComponentCache
        {
            public readonly List<IInternalMemberInfo> Members;

            public ComponentCache(List<IInternalMemberInfo> members)
            {
                Members = members;
            }
        }
    }
}

