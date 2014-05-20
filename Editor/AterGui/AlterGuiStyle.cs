using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Alter
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class AlterGuiStyle
    {
        [NotRenamed]
        [NonSerialized]
        internal IntPtr m_Ptr;
        [NonSerialized]
        private AlterGUIStyleState m_Normal;
        [NonSerialized]
        private AlterGUIStyleState m_Hover;
        [NonSerialized]
        private AlterGUIStyleState m_Active;
        [NonSerialized]
        private AlterGUIStyleState m_Focused;
        [NonSerialized]
        private AlterGUIStyleState m_OnNormal;
        [NonSerialized]
        private AlterGUIStyleState m_OnHover;
        [NonSerialized]
        private AlterGUIStyleState m_OnActive;
        [NonSerialized]
        private AlterGUIStyleState m_OnFocused;
        [NonSerialized]
        private AlterRectOffset m_Border;
        [NonSerialized]
        private AlterRectOffset m_Padding;
        [NonSerialized]
        private AlterRectOffset m_Margin;
        [NonSerialized]
        private AlterRectOffset m_Overflow;
        [NonSerialized]
        private Font m_FontInternal;
        internal static bool showKeyboardFocus = true;
        private static AlterGuiStyle s_None;
        public extern string name
        {
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            set;
        }
        public AlterGUIStyleState normal
        {
            get
            {
                if (m_Normal == null)
                {
                    m_Normal = new AlterGUIStyleState(this, GetStyleStatePtr(0));
                }
                return m_Normal;
            }
            set
            {
                AssignStyleState(0, value.m_Ptr);
            }
        }
        public AlterGUIStyleState hover
        {
            get
            {
                if (m_Hover == null)
                {
                    m_Hover = new AlterGUIStyleState(this, GetStyleStatePtr(1));
                }
                return m_Hover;
            }
            set
            {
                AssignStyleState(1, value.m_Ptr);
            }
        }
        public AlterGUIStyleState active
        {
            get
            {
                if (m_Active == null)
                {
                    m_Active = new AlterGUIStyleState(this, GetStyleStatePtr(2));
                }
                return m_Active;
            }
            set
            {
                AssignStyleState(2, value.m_Ptr);
            }
        }
        public AlterGUIStyleState onNormal
        {
            get
            {
                if (m_OnNormal == null)
                {
                    m_OnNormal = new AlterGUIStyleState(this, GetStyleStatePtr(4));
                }
                return m_OnNormal;
            }
            set
            {
                AssignStyleState(4, value.m_Ptr);
            }
        }
        public AlterGUIStyleState onHover
        {
            get
            {
                if (m_OnHover == null)
                {
                    m_OnHover = new AlterGUIStyleState(this, GetStyleStatePtr(5));
                }
                return m_OnHover;
            }
            set
            {
                AssignStyleState(5, value.m_Ptr);
            }
        }
        public AlterGUIStyleState onActive
        {
            get
            {
                if (m_OnActive == null)
                {
                    m_OnActive = new AlterGUIStyleState(this, GetStyleStatePtr(6));
                }
                return m_OnActive;
            }
            set
            {
                AssignStyleState(6, value.m_Ptr);
            }
        }
        public AlterGUIStyleState focused
        {
            get
            {
                if (m_Focused == null)
                {
                    m_Focused = new AlterGUIStyleState(this, GetStyleStatePtr(3));
                }
                return m_Focused;
            }
            set
            {
                AssignStyleState(3, value.m_Ptr);
            }
        }
        public AlterGUIStyleState onFocused
        {
            get
            {
                if (m_OnFocused == null)
                {
                    m_OnFocused = new AlterGUIStyleState(this, GetStyleStatePtr(7));
                }
                return m_OnFocused;
            }
            set
            {
                AssignStyleState(7, value.m_Ptr);
            }
        }
        public AlterRectOffset border
        {
            get
            {
                if (m_Border == null)
                {
                    return m_Border = new AlterRectOffset(this, GetRectOffsetPtr(0));
                }
                return m_Border;
            }
            set
            {
                AssignRectOffset(0, value.m_Ptr);
            }
        }
        public AlterRectOffset margin
        {
            get
            {
                if (m_Margin == null)
                {
                    return m_Margin = new AlterRectOffset(this, GetRectOffsetPtr(1));
                }
                return m_Margin;
            }
            set
            {
                AssignRectOffset(1, value.m_Ptr);
            }
        }
        public AlterRectOffset padding
        {
            get
            {
                if (m_Padding == null)
                {
                    return m_Padding = new AlterRectOffset(this, GetRectOffsetPtr(2));
                }
                return m_Padding;
            }
            set
            {
                AssignRectOffset(2, value.m_Ptr);
            }
        }
        public AlterRectOffset overflow
        {
            get
            {
                if (m_Overflow == null)
                {
                    return m_Overflow = new AlterRectOffset(this, GetRectOffsetPtr(3));
                }
                return m_Overflow;
            }
            set
            {
                AssignRectOffset(3, value.m_Ptr);
            }
        }
        public extern ImagePosition imagePosition
        {
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            set;
        }
        public extern TextAnchor alignment
        {
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            set;
        }
        public extern bool wordWrap
        {
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            set;
        }
        public extern TextClipping clipping
        {
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            set;
        }
        public Vector2 contentOffset
        {
            get
            {
                Vector2 result;
                INTERNAL_get_contentOffset(out result);
                return result;
            }
            set
            {
                INTERNAL_set_contentOffset(ref value);
            }
        }
        [Obsolete("Don't use clipOffset - put things inside begingroup instead. This functionality will be removed in a later version.")]
        public Vector2 clipOffset
        {
            get
            {
                return Internal_clipOffset;
            }
            set
            {
                Internal_clipOffset = value;
            }
        }
        internal Vector2 Internal_clipOffset
        {
            get
            {
                Vector2 result;
                INTERNAL_get_Internal_clipOffset(out result);
                return result;
            }
            set
            {
                INTERNAL_set_Internal_clipOffset(ref value);
            }
        }
        public extern float fixedWidth
        {
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            set;
        }
        public extern float fixedHeight
        {
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            set;
        }
        public extern bool stretchWidth
        {
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            set;
        }
        public extern bool stretchHeight
        {
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            set;
        }
        public Font font
        {
            get
            {
                return GetFontInternal();
            }
            set
            {
                SetFontInternal(value);
                MFontInternal = value;
            }
        }
        public extern int fontSize
        {
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            set;
        }
        public extern FontStyle fontStyle
        {
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            set;
        }
        public extern bool richText
        {
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            set;
        }
        public float lineHeight
        {
            get
            {
                return Mathf.Round(Internal_GetLineHeight(m_Ptr));
            }
        }
        public static AlterGuiStyle none
        {
            get
            {
                if (s_None == null)
                {
                    s_None = new AlterGuiStyle();
                }
                return s_None;
            }
        }
        public bool isHeightDependantOnWidth
        {
            get
            {
                return fixedHeight == 0f && wordWrap && imagePosition != ImagePosition.ImageOnly;
            }
        }

        public Font MFontInternal
        {
            get { return m_FontInternal; }
            set { m_FontInternal = value; }
        }

        public AlterGuiStyle()
        {
            Init();
        }
        public AlterGuiStyle(AlterGuiStyle other)
        {
            InitCopy(other);
        }
        ~AlterGuiStyle()
        {
            Cleanup();
        }
        internal void CreateObjectReferences()
        {
            MFontInternal = GetFontInternal();
            normal.RefreshAssetReference();
            hover.RefreshAssetReference();
            active.RefreshAssetReference();
            focused.RefreshAssetReference();
            onNormal.RefreshAssetReference();
            onHover.RefreshAssetReference();
            onActive.RefreshAssetReference();
            onFocused.RefreshAssetReference();
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void Init();
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void InitCopy(AlterGuiStyle other);
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void Cleanup();
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern IntPtr GetStyleStatePtr(int idx);
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void AssignStyleState(int idx, IntPtr srcStyleState);
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern IntPtr GetRectOffsetPtr(int idx);
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void AssignRectOffset(int idx, IntPtr srcRectOffset);
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void INTERNAL_get_contentOffset(out Vector2 value);
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void INTERNAL_set_contentOffset(ref Vector2 value);
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void INTERNAL_get_Internal_clipOffset(out Vector2 value);
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void INTERNAL_set_Internal_clipOffset(ref Vector2 value);
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern float Internal_GetLineHeight(IntPtr target);
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void SetFontInternal(Font value);
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern Font GetFontInternal();
        private static void Internal_Draw(IntPtr target, Rect position, AlterGUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
        {
            Internal_DrawArguments internal_DrawArguments = default(Internal_DrawArguments);
            internal_DrawArguments.target = target;
            internal_DrawArguments.position = position;
            internal_DrawArguments.isHover = ((!isHover) ? 0 : 1);
            internal_DrawArguments.isActive = ((!isActive) ? 0 : 1);
            internal_DrawArguments.on = ((!on) ? 0 : 1);
            internal_DrawArguments.hasKeyboardFocus = ((!hasKeyboardFocus) ? 0 : 1);
            Internal_Draw(content, ref internal_DrawArguments);
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void Internal_Draw(AlterGUIContent content, ref Internal_DrawArguments arguments);
        public void Draw(Rect position, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
        {
            if (Event.current.type != EventType.Repaint)
            {
                Debug.LogError("Style.Draw may not be called if it is not a repaint event");
                return;
            }
            Internal_Draw(m_Ptr, position, AlterGUIContent.none, isHover, isActive, on, hasKeyboardFocus);
        }
        public void Draw(Rect position, string text, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
        {
            if (Event.current.type != EventType.Repaint)
            {
                Debug.LogError("Style.Draw may not be called if it is not a repaint event");
                return;
            }
            Internal_Draw(m_Ptr, position, AlterGUIContent.Temp(text), isHover, isActive, on, hasKeyboardFocus);
        }
        public void Draw(Rect position, Texture image, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
        {
            if (Event.current.type != EventType.Repaint)
            {
                Debug.LogError("Style.Draw may not be called if it is not a repaint event");
                return;
            }
            Internal_Draw(m_Ptr, position, AlterGUIContent.Temp(image), isHover, isActive, on, hasKeyboardFocus);
        }
        public void Draw(Rect position, AlterGUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
        {
            if (Event.current.type != EventType.Repaint)
            {
                Debug.LogError("Style.Draw may not be called if it is not a repaint event");
                return;
            }
            Internal_Draw(m_Ptr, position, content, isHover, isActive, on, hasKeyboardFocus);
        }
        [ExcludeFromDocs]
        public void Draw(Rect position, AlterGUIContent content, int controlID)
        {
            bool on = false;
            Draw(position, content, controlID, on);
        }
        public void Draw(Rect position, AlterGUIContent content, int controlID, [DefaultValue("false")] bool on)
        {
            if (Event.current.type != EventType.Repaint)
            {
                Debug.LogError("Style.Draw may not be called if it is not a repaint event.");
                return;
            }
            if (content != null)
            {
                Internal_Draw2(m_Ptr, position, content, controlID, on);
            }
            else
            {
                Debug.LogError("Style.Draw may not be called with AlterGUIContent that is null.");
            }
        }
        private static void Internal_Draw2(IntPtr style, Rect position, AlterGUIContent content, int controlID, bool on)
        {
            INTERNAL_CALL_Internal_Draw2(style, ref position, content, controlID, on);
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void INTERNAL_CALL_Internal_Draw2(IntPtr style, ref Rect position, AlterGUIContent content, int controlID, bool on);
        internal void DrawPrefixLabel(Rect position, AlterGUIContent content, int controlID)
        {
            if (content != null)
            {
                Internal_DrawPrefixLabel(m_Ptr, position, content, controlID, false);
            }
            else
            {
                Debug.LogError("Style.DrawPrefixLabel may not be called with AlterGUIContent that is null.");
            }
        }
        private static void Internal_DrawPrefixLabel(IntPtr style, Rect position, AlterGUIContent content, int controlID, bool on)
        {
            INTERNAL_CALL_Internal_DrawPrefixLabel(style, ref position, content, controlID, on);
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void INTERNAL_CALL_Internal_DrawPrefixLabel(IntPtr style, ref Rect position, AlterGUIContent content, int controlID, bool on);
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern float Internal_GetCursorFlashOffset();
        private static void Internal_DrawCursor(IntPtr target, Rect position, AlterGUIContent content, int pos, Color cursorColor)
        {
            INTERNAL_CALL_Internal_DrawCursor(target, ref position, content, pos, ref cursorColor);
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void INTERNAL_CALL_Internal_DrawCursor(IntPtr target, ref Rect position, AlterGUIContent content, int pos, ref Color cursorColor);
        public void DrawCursor(Rect position, AlterGUIContent content, int controlID, int Character)
        {
            Event current = Event.current;
            if (current.type == EventType.Repaint)
            {
                Color cursorColor = new Color(0f, 0f, 0f, 0f);
                float cursorFlashSpeed = GUI.skin.settings.cursorFlashSpeed;
                float num = (Time.realtimeSinceStartup - Internal_GetCursorFlashOffset()) % cursorFlashSpeed / cursorFlashSpeed;
                if (cursorFlashSpeed == 0f || num < 0.5f)
                {
                    cursorColor = GUI.skin.settings.cursorColor;
                }
                Internal_DrawCursor(m_Ptr, position, content, Character, cursorColor);
            }
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void Internal_DrawWithTextSelection(AlterGUIContent content, ref Internal_DrawWithTextSelectionArguments arguments);
        internal void DrawWithTextSelection(Rect position, AlterGUIContent content, int controlID, int firstSelectedCharacter, int lastSelectedCharacter, bool drawSelectionAsComposition)
        {
            if (Event.current.type != EventType.Repaint)
            {
                Debug.LogError("Style.Draw may not be called if it is not a repaint event");
                return;
            }
            Event current = Event.current;
            Color cursorColor = new Color(0f, 0f, 0f, 0f);
            float cursorFlashSpeed = GUI.skin.settings.cursorFlashSpeed;
            float num = (Time.realtimeSinceStartup - Internal_GetCursorFlashOffset()) % cursorFlashSpeed / cursorFlashSpeed;
            if (cursorFlashSpeed == 0f || num < 0.5f)
            {
                cursorColor = GUI.skin.settings.cursorColor;
            }
            var internal_DrawWithTextSelectionArguments = default(Internal_DrawWithTextSelectionArguments);
            internal_DrawWithTextSelectionArguments.target = m_Ptr;
            internal_DrawWithTextSelectionArguments.position = position;
            internal_DrawWithTextSelectionArguments.firstPos = firstSelectedCharacter;
            internal_DrawWithTextSelectionArguments.lastPos = lastSelectedCharacter;
            internal_DrawWithTextSelectionArguments.cursorColor = cursorColor;
            internal_DrawWithTextSelectionArguments.selectionColor = GUI.skin.settings.selectionColor;
            internal_DrawWithTextSelectionArguments.isHover = ((!position.Contains(current.mousePosition)) ? 0 : 1);
            internal_DrawWithTextSelectionArguments.isActive = ((controlID != AlterGUIUtility.hotControl) ? 0 : 1);
            internal_DrawWithTextSelectionArguments.on = 0;
            internal_DrawWithTextSelectionArguments.hasKeyboardFocus = ((controlID != AlterGUIUtility.keyboardControl || !showKeyboardFocus) ? 0 : 1);
            internal_DrawWithTextSelectionArguments.drawSelectionAsComposition = ((!drawSelectionAsComposition) ? 0 : 1);
            Internal_DrawWithTextSelection(content, ref internal_DrawWithTextSelectionArguments);
        }
        public void DrawWithTextSelection(Rect position, AlterGUIContent content, int controlID, int firstSelectedCharacter, int lastSelectedCharacter)
        {
            DrawWithTextSelection(position, content, controlID, firstSelectedCharacter, lastSelectedCharacter, false);
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void SetDefaultFont(Font font);
        public Vector2 GetCursorPixelPosition(Rect position, AlterGUIContent content, int cursorStringIndex)
        {
            Vector2 result;
            Internal_GetCursorPixelPosition(m_Ptr, position, content, cursorStringIndex, out result);
            return result;
        }
        internal static void Internal_GetCursorPixelPosition(IntPtr target, Rect position, AlterGUIContent content, int cursorStringIndex, out Vector2 ret)
        {
            INTERNAL_CALL_Internal_GetCursorPixelPosition(target, ref position, content, cursorStringIndex, out ret);
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void INTERNAL_CALL_Internal_GetCursorPixelPosition(IntPtr target, ref Rect position, AlterGUIContent content, int cursorStringIndex, out Vector2 ret);
        public int GetCursorStringIndex(Rect position, AlterGUIContent content, Vector2 cursorPixelPosition)
        {
            return Internal_GetCursorStringIndex(m_Ptr, position, content, cursorPixelPosition);
        }
        internal static int Internal_GetCursorStringIndex(IntPtr target, Rect position, AlterGUIContent content, Vector2 cursorPixelPosition)
        {
            return INTERNAL_CALL_Internal_GetCursorStringIndex(target, ref position, content, ref cursorPixelPosition);
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern int INTERNAL_CALL_Internal_GetCursorStringIndex(IntPtr target, ref Rect position, AlterGUIContent content, ref Vector2 cursorPixelPosition);
        internal int GetNumCharactersThatFitWithinWidth(string text, float width)
        {
            return Internal_GetNumCharactersThatFitWithinWidth(m_Ptr, text, width);
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern int Internal_GetNumCharactersThatFitWithinWidth(IntPtr target, string text, float width);
        public Vector2 CalcSize(AlterGUIContent content)
        {
            Vector2 result;
            Internal_CalcSize(m_Ptr, content, out result);
            return result;
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Internal_CalcSize(IntPtr target, AlterGUIContent content, out Vector2 ret);
        public Vector2 CalcScreenSize(Vector2 contentSize)
        {
            return new Vector2((fixedWidth == 0f) ? Mathf.Ceil(contentSize.x + (float)padding.left + (float)padding.right) : fixedWidth, (fixedHeight == 0f) ? Mathf.Ceil(contentSize.y + (float)padding.top + (float)padding.bottom) : fixedHeight);
        }
        public float CalcHeight(AlterGUIContent content, float width)
        {
            return Internal_CalcHeight(m_Ptr, content, width);
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern float Internal_CalcHeight(IntPtr target, AlterGUIContent content, float width);
        public void CalcMinMaxWidth(AlterGUIContent content, out float minWidth, out float maxWidth)
        {
            Internal_CalcMinMaxWidth(m_Ptr, content, out minWidth, out maxWidth);
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void Internal_CalcMinMaxWidth(IntPtr target, AlterGUIContent content, out float minWidth, out float maxWidth);
        public override string ToString()
        {
            return AlterUnityString.Format("AlterGuiStyle '{0}'", new object[]
            {
                name
            });
        }
        public static implicit operator AlterGuiStyle(string str)
        {
            if (AlterGUISkin.current == null)
            {
                Debug.LogError("Unable to use a named AlterGuiStyle without a current skin. Most likely you need to move your AlterGuiStyle initialization code to OnGUI");
                return AlterGUISkin.error;
            }
            return AlterGUISkin.current.GetStyle(str);
        }
    }
}