using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Alter
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class AlterGUIStyleState
    {
        [NotRenamed]
        [NonSerialized]
        internal IntPtr m_Ptr;
        private readonly AlterGuiStyle m_SourceStyle;
        [NonSerialized]
        private Texture2D m_Background;
        public Texture2D background
        {
            get
            {
                return GetBackgroundInternal();
            }
            set
            {
                SetBackgroundInternal(value);
                MBackground = value;
            }
        }
        public Color textColor
        {
            get
            {
                Color result;
                INTERNAL_get_textColor(out result);
                return result;
            }
            set
            {
                INTERNAL_set_textColor(ref value);
            }
        }

        public Texture2D MBackground
        {
            get { return m_Background; }
            set { m_Background = value; }
        }

        public AlterGUIStyleState()
        {
            Init();
        }
        internal AlterGUIStyleState(AlterGuiStyle sourceStyle, IntPtr source)
        {
            m_SourceStyle = sourceStyle;
            m_Ptr = source;
            RefreshAssetReference();
        }
        internal void RefreshAssetReference()
        {
            MBackground = GetBackgroundInternal();
        }
        ~AlterGUIStyleState()
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
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void SetBackgroundInternal(Texture2D value);
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern Texture2D GetBackgroundInternal();
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void INTERNAL_get_textColor(out Color value);
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void INTERNAL_set_textColor(ref Color value);
    }
}