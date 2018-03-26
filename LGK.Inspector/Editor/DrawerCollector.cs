// See LICENSE file in the root directory
//

using System;
using System.Collections.Generic;
using System.Linq;
using LGK.Inspector.StandardDrawer;

namespace LGK.Inspector
{
    public static class DrawerCollector
    {
        public static Dictionary<Type, IComponentDrawer> CollectComponentDrawers(Dictionary<Type, ITypeDrawer> typeDrawers)
        {
            var drawers = new Dictionary<Type, IComponentDrawer>()
            {

            };

            var customDrawers = CustomComponentDrawers();

            foreach (var type in customDrawers)
            {
                UnityEngine.Assertions.Assert.IsNotNull(type.GetConstructor(Type.EmptyTypes), "CustomComponentDrawer should have empty constructor.");

                var drawer = Activator.CreateInstance(type) as CustomComponentDrawer;

                drawer.SetTypeDrawers(typeDrawers);

                drawers.Add(drawer.TargetType, drawer);
            }

            return drawers;
        }

        public static Dictionary<Type, ITypeDrawer> CollectTypeDrawers()
        {
            var drawers = new Dictionary<Type, ITypeDrawer>()
            {
                { typeof(bool), new BoolTypeDrawer() },
                { typeof(byte), new ByteDrawer() },
                { typeof(sbyte), new SByteDrawer() },
                { typeof(short), new ShortDrawer() },
                { typeof(ushort),new UShortDrawer() },
                { typeof(int), new IntDrawer() },
                { typeof(uint),new UIntDrawer() },
                { typeof(long), new LongDrawer() },
                { typeof(ulong), new ULongDrawer() },
                { typeof(char), new CharDrawer() },
                { typeof(string), new StringDrawer() },
                { typeof(float), new FloatDrawer() },
                { typeof(double), new DoubleDrawer() },
                { typeof(UnityEngine.Vector2), new Vector2Drawer() },
                { typeof(UnityEngine.Vector3), new Vector3Drawer() },
            };

            var customDrawers = CustomTypeDrawer();
            foreach (var type in customDrawers)
            {
                UnityEngine.Assertions.Assert.IsNotNull(type.GetConstructor(Type.EmptyTypes), "CustomTypeDrawer should have empty constructor.");

                var drawer = Activator.CreateInstance(type) as CustomTypeDrawer;
                drawer.SetTypeDrawers(drawers);

                drawers.Add(drawer.TargetType, drawer);
            }

            return drawers;
        }


        static IEnumerable<Type> CustomTypeDrawer()
        {
            var baseType = typeof(CustomTypeDrawer);

            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(t => t.BaseType == baseType && !t.IsAbstract);
        }

        static IEnumerable<Type> CustomComponentDrawers()
        {
            var baseType = typeof(CustomComponentDrawer);

            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(t => t.BaseType == baseType && !t.IsAbstract);
        }
    }
}