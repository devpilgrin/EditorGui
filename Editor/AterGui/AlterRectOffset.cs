using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Alter
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public sealed class AlterRectOffset
    {
        [NotRenamed]
        [NonSerialized]
        internal IntPtr m_Ptr;
        private AlterGuiStyle m_SourceStyle;
        public extern int left
        {
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            set;
        }
        public extern int right
        {
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            set;
        }
        public extern int top
        {
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            set;
        }
        public extern int bottom
        {
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            set;
        }
        public extern int horizontal
        {
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
        }
        public extern int vertical
        {
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
        }
        public AlterRectOffset()
        {
            Init();
        }
        internal AlterRectOffset(AlterGuiStyle sourceStyle, IntPtr source)
        {
            m_SourceStyle = sourceStyle;
            m_Ptr = source;
        }
        public AlterRectOffset(int left, int right, int top, int bottom)
        {
            Init();
            this.left = left;
            this.right = right;
            this.top = top;
            this.bottom = bottom;
        }
        ~AlterRectOffset()
        {
            if (m_SourceStyle == null)
            {
                Cleanup();
            }
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void Init();
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void Cleanup();
        public Rect Add(Rect rect)
        {
            return INTERNAL_CALL_Add(this, ref rect);
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern Rect INTERNAL_CALL_Add(AlterRectOffset self, ref Rect rect);
        public Rect Remove(Rect rect)
        {
            return INTERNAL_CALL_Remove(this, ref rect);
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern Rect INTERNAL_CALL_Remove(AlterRectOffset self, ref Rect rect);
        public override string ToString()
        {
            return AlterUnityString.Format("AlterRectOffset (l:{0} r:{1} t:{2} b:{3})", new object[]
            {
                left,
                right,
                top,
                bottom
            });
        }
    }
}