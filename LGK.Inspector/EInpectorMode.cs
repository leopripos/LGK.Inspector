// See LICENSE file in the root directory
//

namespace LGK.Inspector
{
    public static class EInspectorMode
    {
        public const byte All = byte.MaxValue;
        public const byte Field = 1;
        public const byte Property = 1 << 1;
    }
}