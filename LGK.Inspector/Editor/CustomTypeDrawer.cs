// See LICENSE file in the root directory
//

using System;
using System.Collections.Generic;

namespace LGK.Inspector
{
    public abstract class CustomTypeDrawer : ITypeDrawer
    {
        protected Dictionary<Type, ITypeDrawer> typeDrawers;

        internal void SetTypeDrawers(Dictionary<Type, ITypeDrawer> drawers)
        {
            this.typeDrawers = drawers;
        }

        public abstract Type TargetType { get; }

        public abstract object Draw(IMemberInfo memberInfo, object memberValue);
    }
}