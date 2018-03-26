// See LICENSE file in the root directory
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ReflectionField = System.Reflection.FieldInfo;
using ReflectionProperty = System.Reflection.PropertyInfo;

namespace LGK.Inspector
{
    public static class InspectorUtility
    {
        static readonly IFieldInfo[] EMPTY_FIELD_INFOS = new IFieldInfo[0];
        static readonly IPropertyInfo[] EMPTY_PROPERTY_INFOS = new IPropertyInfo[0];

        static readonly Dictionary<Type, IFieldInfo[]> m_CachedFields = new Dictionary<Type, IFieldInfo[]>();
        static readonly Dictionary<Type, IPropertyInfo[]> m_CachedProperties = new Dictionary<Type, IPropertyInfo[]>();

        public static string GetCleanName(string dirtyName)
        {
            int pFrom = dirtyName.IndexOf('<');

            if (pFrom == -1)
            {
                return dirtyName;
            }

            int pTo = dirtyName.LastIndexOf('>');

            return dirtyName.Substring(pFrom + 1, pTo - pFrom - 1);
        }

        public static IEntityInfo ExtractEntityInfo(int id, object[] componensts, byte modeMask)
        {
            var entityInfo = new EntityInfo(id, componensts.Length);

            var count = componensts.Length;
            for (int i = 0; i < count; i++)
            {
                var componentValue = componensts[i];
                var componentType = componensts[i].GetType();

                IFieldInfo[] componentFields;
                if ((modeMask & EInspectorMode.Field) == EInspectorMode.Field)
                    componentFields = ExtractFields(componentType);
                else
                    componentFields = EMPTY_FIELD_INFOS;

                IPropertyInfo[] componentProperties;
                if ((modeMask & EInspectorMode.Field) == EInspectorMode.Field)
                    componentProperties = ExtractProperties(componentType);
                else
                    componentProperties = EMPTY_PROPERTY_INFOS;

                var componentInfo = new ComponentInfo(componentValue, componentType, componentFields, componentProperties);

                entityInfo.Components[i] = componentInfo;

            }

            return entityInfo;
        }

        static IFieldInfo[] ExtractFields(Type componentType)
        {
            IFieldInfo[] fields;
            if (!m_CachedFields.TryGetValue(componentType, out fields))
            {
                var reflectionFields = componentType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly);

                fields = Convert(reflectionFields);

                m_CachedFields.Add(componentType, fields);
            }

            return fields;
        }

        static IPropertyInfo[] ExtractProperties(Type componentType)
        {
            IPropertyInfo[] properoties;
            if (!m_CachedProperties.TryGetValue(componentType, out properoties))
            {
                var reflectionProperties = componentType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly).Where(p => p.CanRead).ToArray();

                properoties = Convert(reflectionProperties);

                m_CachedProperties.Add(componentType, properoties);
            }

            return properoties;
        }

        static IFieldInfo[] Convert(ReflectionField[] reflectionFieldInfos)
        {
            var fields = new IFieldInfo[reflectionFieldInfos.Length];

            for (int i = 0; i < reflectionFieldInfos.Length; i++)
            {
                fields[i] = new ReflectionFieldInfoWrapper(reflectionFieldInfos[i]);
            }

            return fields;
        }

        static IPropertyInfo[] Convert(ReflectionProperty[] reflectionPropertyInfos)
        {
            var properties = new IPropertyInfo[reflectionPropertyInfos.Length];

            for (int i = 0; i < reflectionPropertyInfos.Length; i++)
            {
                properties[i] = new ReflectionPropertyInfoWrapper(reflectionPropertyInfos[i]);
            }

            return properties;
        }
    }
}

