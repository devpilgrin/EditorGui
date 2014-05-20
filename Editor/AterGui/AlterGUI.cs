/*
 * Окончательное название, для сокращения записей AlternativeGUI преобразован в AlterGUI
 */


using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEngine;
using UnityEngineInternal;


namespace Alter
{
    /// <summary>
    /// Класс альтернативного GUI
    /// Данный класс содержит базовые элементы управления
    /// </summary>
    public class AlterGUI : GUI
    {
        private Rect s_ToolTipRect;
        internal static void BeginWindows(int skinMode, int editorWindowInstanceID)
        {
            //AlternativeGUILayout
            var topLevel = AlterGuiLayoutUtility.current.topLevel;
            var layoutGroups = AlterGuiLayoutUtility.current.layoutGroups;
            var windows = AlterGuiLayoutUtility.current.windows;
            var matrix = GUI.matrix;
            //GUI.Internal_BeginWindows();
            GUI.matrix = matrix;
            AlterGuiLayoutUtility.current.topLevel = topLevel;
            AlterGuiLayoutUtility.current.layoutGroups = layoutGroups;
            AlterGuiLayoutUtility.current.windows = windows;
        }
        
    }


    internal class AlterGuiLayoutEntry
    {
        public float minWidth;
        public float maxWidth;
        public float minHeight;
        public float maxHeight;
        public Rect rect = new Rect(0f, 0f, 0f, 0f);
        public int stretchWidth;
        public int stretchHeight;
        private AlterGuiStyle m_Style = AlterGuiStyle.none;
        internal static Rect kDummyRect = new Rect(0f, 0f, 1f, 1f);
        protected static int indent = 0;
        public AlterGuiStyle style
        {
            get
            {
                return m_Style;
            }
            set
            {
                m_Style = value;
                ApplyStyleSettings(value);
            }
        }
        public virtual AlterRectOffset margin
        {
            get
            {
                return style.margin;
            }
        }
        public AlterGuiLayoutEntry(float _minWidth, float _maxWidth, float _minHeight, float _maxHeight, AlterGuiStyle _style)
        {
            minWidth = _minWidth;
            maxWidth = _maxWidth;
            minHeight = _minHeight;
            maxHeight = _maxHeight;
            if (_style == null)
            {
                _style = AlterGuiStyle.none;
            }
            style = _style;
        }
        public AlterGuiLayoutEntry(float _minWidth, float _maxWidth, float _minHeight, float _maxHeight, AlterGuiStyle _style, AlterGuiLayoutOption[] options)
        {
            minWidth = _minWidth;
            maxWidth = _maxWidth;
            minHeight = _minHeight;
            maxHeight = _maxHeight;
            style = _style;
            ApplyOptions(options);
        }
        public virtual void CalcWidth()
        {
        }
        public virtual void CalcHeight()
        {
        }
        public virtual void SetHorizontal(float x, float width)
        {
            rect.x = x;
            rect.width = width;
        }
        public virtual void SetVertical(float y, float height)
        {
            rect.y = y;
            rect.height = height;
        }
        protected virtual void ApplyStyleSettings(AlterGuiStyle style)
        {
            stretchWidth = ((style.fixedWidth != 0f || !style.stretchWidth) ? 0 : 1);
            stretchHeight = ((style.fixedHeight != 0f || !style.stretchHeight) ? 0 : 1);
            m_Style = style;
        }
        public virtual void ApplyOptions(AlterGuiLayoutOption[] options)
        {
            if (options == null)
            {
                return;
            }
            for (var i = 0; i < options.Length; i++)
            {
                var gUILayoutOption = options[i];
                switch (gUILayoutOption.type)
                {
                    case AlterGuiLayoutOption.Type.fixedWidth:
                        minWidth = (maxWidth = (float)gUILayoutOption.value);
                        stretchWidth = 0;
                        break;
                    case AlterGuiLayoutOption.Type.fixedHeight:
                        minHeight = (maxHeight = (float)gUILayoutOption.value);
                        stretchHeight = 0;
                        break;
                    case AlterGuiLayoutOption.Type.minWidth:
                        minWidth = (float)gUILayoutOption.value;
                        if (maxWidth < minWidth)
                        {
                            maxWidth = minWidth;
                        }
                        break;
                    case AlterGuiLayoutOption.Type.maxWidth:
                        maxWidth = (float)gUILayoutOption.value;
                        if (minWidth > maxWidth)
                        {
                            minWidth = maxWidth;
                        }
                        stretchWidth = 0;
                        break;
                    case AlterGuiLayoutOption.Type.minHeight:
                        minHeight = (float)gUILayoutOption.value;
                        if (maxHeight < minHeight)
                        {
                            maxHeight = minHeight;
                        }
                        break;
                    case AlterGuiLayoutOption.Type.maxHeight:
                        maxHeight = (float)gUILayoutOption.value;
                        if (minHeight > maxHeight)
                        {
                            minHeight = maxHeight;
                        }
                        stretchHeight = 0;
                        break;
                    case AlterGuiLayoutOption.Type.stretchWidth:
                        stretchWidth = (int)gUILayoutOption.value;
                        break;
                    case AlterGuiLayoutOption.Type.stretchHeight:
                        stretchHeight = (int)gUILayoutOption.value;
                        break;
                }
            }
            if (maxWidth != 0f && maxWidth < minWidth)
            {
                maxWidth = minWidth;
            }
            if (maxHeight != 0f && maxHeight < minHeight)
            {
                maxHeight = minHeight;
            }
        }
        public override string ToString()
        {
            var text = string.Empty;
            for (var i = 0; i < indent; i++)
            {
                text += " ";
            }
            return string.Concat(new object[]
			{
				text,
				AlterUnityString.Format("{1}-{0} (x:{2}-{3}, y:{4}-{5})", new object[]
				{
					(style == null) ? "NULL" : style.name,
					base.GetType(),
					rect.x,
					rect.xMax,
					rect.y,
					rect.yMax
				}),
				"   -   W: ",
				minWidth,
				"-",
				maxWidth,
				(stretchWidth == 0) ? string.Empty : "+",
				", H: ",
				minHeight,
				"-",
				maxHeight,
				(stretchHeight == 0) ? string.Empty : "+"
			});
        }
    }



    internal class AlterGuiLayoutGroup : AlterGuiLayoutEntry
    {
        public List<AlterGuiLayoutEntry> entries = new List<AlterGuiLayoutEntry>();
        public bool isVertical = true;
        public bool resetCoords=false;
        public float spacing;
        public bool sameSize = true;
        public bool isWindow = false;
        public int windowID = -1;
        private int cursor;
        protected int stretchableCountX = 100;
        protected int stretchableCountY = 100;
        protected bool userSpecifiedWidth;
        protected bool userSpecifiedHeight;
        protected float childMinWidth = 100f;
        protected float childMaxWidth = 100f;
        protected float childMinHeight = 100f;
        protected float childMaxHeight = 100f;
        private AlterRectOffset m_Margin = new AlterRectOffset();
        public override AlterRectOffset margin
        {
            get
            {
                return m_Margin;
            }
        }
        public AlterGuiLayoutGroup()
            : base(0f, 0f, 0f, 0f, AlterGuiStyle.none)
        {
        }
        public AlterGuiLayoutGroup(AlterGuiStyle _style, AlterGuiLayoutOption[] options)
            : base(0f, 0f, 0f, 0f, _style)
        {
            if (options != null)
            {
                ApplyOptions(options);
            }
            m_Margin.left = _style.margin.left;
            m_Margin.right = _style.margin.right;
            m_Margin.top = _style.margin.top;
            m_Margin.bottom = _style.margin.bottom;
        }
        public override void ApplyOptions(AlterGuiLayoutOption[] options)
        {
            if (options == null)
            {
                return;
            }
            base.ApplyOptions(options);
            for (var i = 0; i < options.Length; i++)
            {
                var gUILayoutOption = options[i];
                switch (gUILayoutOption.type)
                {
                    case AlterGuiLayoutOption.Type.fixedWidth:
                    case AlterGuiLayoutOption.Type.minWidth:
                    case AlterGuiLayoutOption.Type.maxWidth:
                        userSpecifiedHeight = true;
                        break;
                    case AlterGuiLayoutOption.Type.fixedHeight:
                    case AlterGuiLayoutOption.Type.minHeight:
                    case AlterGuiLayoutOption.Type.maxHeight:
                        userSpecifiedWidth = true;
                        break;
                    case AlterGuiLayoutOption.Type.spacing:
                        spacing = (int)gUILayoutOption.value;
                        break;
                }
            }
        }
        protected override void ApplyStyleSettings(AlterGuiStyle style)
        {
            base.ApplyStyleSettings(style);
            var margin = style.margin;
            m_Margin.left = margin.left;
            m_Margin.right = margin.right;
            m_Margin.top = margin.top;
            m_Margin.bottom = margin.bottom;
        }
        public void ResetCursor()
        {
            cursor = 0;
        }
        public Rect PeekNext()
        {
            if (cursor < entries.Count)
            {
                var gUILayoutEntry = entries[cursor];
                return gUILayoutEntry.rect;
            }
            throw new ArgumentException(string.Concat(new object[]
			{
				"Getting control ",
				cursor,
				"'s position in a group with only ",
				entries.Count,
				" controls when doing ",
				Event.current.rawType,
				"\nAborting"
			}));
        }
        public AlterGuiLayoutEntry GetNext()
        {
            if (cursor < entries.Count)
            {
                var result = entries[cursor];
                cursor++;
                return result;
            }
            throw new ArgumentException(string.Concat(new object[]
			{
				"Getting control ",
				cursor,
				"'s position in a group with only ",
				entries.Count,
				" controls when doing ",
				Event.current.rawType,
				"\nAborting"
			}));
        }
        public Rect GetLast()
        {
            if (cursor == 0)
            {
                Debug.LogError("You cannot call GetLast immediately after beginning a group.");
                return kDummyRect;
            }
            if (cursor <= entries.Count)
            {
                var gUILayoutEntry = entries[cursor - 1];
                return gUILayoutEntry.rect;
            }
            Debug.LogError(string.Concat(new object[]
			{
				"Getting control ",
				cursor,
				"'s position in a group with only ",
				entries.Count,
				" controls when doing ",
				Event.current.type
			}));
            return kDummyRect;
        }
        public void Add(AlterGuiLayoutEntry e)
        {
            entries.Add(e);
        }
        public override void CalcWidth()
        {
            if (entries.Count == 0)
            {
                maxWidth = (minWidth = base.style.padding.horizontal);
                return;
            }
            childMinWidth = 0f;
            childMaxWidth = 0f;
            var num = 0;
            var num2 = 0;
            stretchableCountX = 0;
            var flag = true;
            if (isVertical)
            {
                foreach (var current in entries)
                {
                    current.CalcWidth();
                    var margin = current.margin;
                    if (current.style != AlterGuiLayoutUtility.spaceStyle)
                    {
                        if (!flag)
                        {
                            num = Mathf.Min(margin.left, num);
                            num2 = Mathf.Min(margin.right, num2);
                        }
                        else
                        {
                            num = margin.left;
                            num2 = margin.right;
                            flag = false;
                        }
                        childMinWidth = Mathf.Max(current.minWidth + margin.horizontal, childMinWidth);
                        childMaxWidth = Mathf.Max(current.maxWidth + margin.horizontal, childMaxWidth);
                    }
                    stretchableCountX += current.stretchWidth;
                }
                childMinWidth -= num + num2;
                childMaxWidth -= num + num2;
            }
            else
            {
                var num3 = 0;
                foreach (var current2 in entries)
                {
                    current2.CalcWidth();
                    var margin2 = current2.margin;
                    if (current2.style != AlterGuiLayoutUtility.spaceStyle)
                    {
                        int num4;
                        if (!flag)
                        {
                            num4 = ((num3 <= margin2.left) ? margin2.left : num3);
                        }
                        else
                        {
                            num4 = 0;
                            flag = false;
                        }
                        childMinWidth += current2.minWidth + spacing + num4;
                        childMaxWidth += current2.maxWidth + spacing + num4;
                        num3 = margin2.right;
                        stretchableCountX += current2.stretchWidth;
                    }
                    else
                    {
                        childMinWidth += current2.minWidth;
                        childMaxWidth += current2.maxWidth;
                        stretchableCountX += current2.stretchWidth;
                    }
                }
                childMinWidth -= spacing;
                childMaxWidth -= spacing;
                if (entries.Count != 0)
                {
                    num = entries[0].margin.left;
                    num2 = num3;
                }
                else
                {
                    num2 = (num = 0);
                }
            }
            float num5;
            float num6;
            if (style != AlterGuiStyle.none || userSpecifiedWidth)
            {
                num5 = Mathf.Max(style.padding.left, num);
                num6 = Mathf.Max(style.padding.right, num2);
            }
            else
            {
                m_Margin.left = num;
                m_Margin.right = num2;
                num6 = (num5 = 0f);
            }
            minWidth = Mathf.Max(minWidth, childMinWidth + num5 + num6);
            if (maxWidth == 0f)
            {
                stretchWidth += stretchableCountX + ((!style.stretchWidth) ? 0 : 1);
                maxWidth = childMaxWidth + num5 + num6;
            }
            else
            {
                stretchWidth = 0;
            }
            maxWidth = Mathf.Max(maxWidth, minWidth);
            if (style.fixedWidth != 0f)
            {
                maxWidth = (minWidth = style.fixedWidth);
                stretchWidth = 0;
            }
        }
        public override void SetHorizontal(float x, float width)
        {
            base.SetHorizontal(x, width);
            if (resetCoords)
            {
                x = 0f;
            }
            var padding = style.padding;
            if (isVertical)
            {
                if (base.style != AlterGuiStyle.none)
                {
                    foreach (var current in entries)
                    {
                        float num = Mathf.Max(current.margin.left, padding.left);
                        var x2 = x + num;
                        var num2 = width - Mathf.Max(current.margin.right, padding.right) - num;
                        if (current.stretchWidth != 0)
                        {
                            current.SetHorizontal(x2, num2);
                        }
                        else
                        {
                            current.SetHorizontal(x2, Mathf.Clamp(num2, current.minWidth, current.maxWidth));
                        }
                    }
                }
                else
                {
                    var num3 = x - margin.left;
                    var num4 = width + margin.horizontal;
                    foreach (var current2 in entries)
                    {
                        if (current2.stretchWidth != 0)
                        {
                            current2.SetHorizontal(num3 + current2.margin.left, num4 - current2.margin.horizontal);
                        }
                        else
                        {
                            current2.SetHorizontal(num3 + current2.margin.left, Mathf.Clamp(num4 - current2.margin.horizontal, current2.minWidth, current2.maxWidth));
                        }
                    }
                }
            }
            else
            {
                if (base.style != AlterGuiStyle.none)
                {
                    float num5 = padding.left;
                    float num6 = padding.right;
                    if (entries.Count != 0)
                    {
                        num5 = Mathf.Max(num5, entries[0].margin.left);
                        num6 = Mathf.Max(num6, entries[entries.Count - 1].margin.right);
                    }
                    x += num5;
                    width -= num6 + num5;
                }
                var num7 = width - spacing * (entries.Count - 1);
                var t = 0f;
                if (childMinWidth != childMaxWidth)
                {
                    t = Mathf.Clamp((num7 - childMinWidth) / (childMaxWidth - childMinWidth), 0f, 1f);
                }
                var num8 = 0f;
                if (num7 > childMaxWidth && stretchableCountX > 0)
                {
                    num8 = (num7 - childMaxWidth) / stretchableCountX;
                }
                var num9 = 0;
                var flag = true;
                foreach (var current3 in entries)
                {
                    var num10 = Mathf.Lerp(current3.minWidth, current3.maxWidth, t);
                    num10 += num8 * current3.stretchWidth;
                    if (current3.style != AlterGuiLayoutUtility.spaceStyle)
                    {
                        var num11 = current3.margin.left;
                        if (flag)
                        {
                            num11 = 0;
                            flag = false;
                        }
                        var num12 = (num9 <= num11) ? num11 : num9;
                        x += num12;
                        num9 = current3.margin.right;
                    }
                    current3.SetHorizontal(Mathf.Round(x), Mathf.Round(num10));
                    x += num10 + spacing;
                }
            }
        }
        public override void CalcHeight()
        {
            if (entries.Count == 0)
            {
                maxHeight = (minHeight = base.style.padding.vertical);
                return;
            }
            childMinHeight = (childMaxHeight = 0f);
            var num = 0;
            var num2 = 0;
            stretchableCountY = 0;
            if (isVertical)
            {
                var num3 = 0;
                var flag = true;
                foreach (var current in entries)
                {
                    current.CalcHeight();
                    var margin = current.margin;
                    if (current.style != AlterGuiLayoutUtility.spaceStyle)
                    {
                        int num4;
                        if (!flag)
                        {
                            num4 = Mathf.Max(num3, margin.top);
                        }
                        else
                        {
                            num4 = 0;
                            flag = false;
                        }
                        childMinHeight += current.minHeight + spacing + num4;
                        childMaxHeight += current.maxHeight + spacing + num4;
                        num3 = margin.bottom;
                        stretchableCountY += current.stretchHeight;
                    }
                    else
                    {
                        childMinHeight += current.minHeight;
                        childMaxHeight += current.maxHeight;
                        stretchableCountY += current.stretchHeight;
                    }
                }
                childMinHeight -= spacing;
                childMaxHeight -= spacing;
                if (entries.Count != 0)
                {
                    num = entries[0].margin.top;
                    num2 = num3;
                }
                else
                {
                    num = (num2 = 0);
                }
            }
            else
            {
                var flag2 = true;
                foreach (var current2 in entries)
                {
                    current2.CalcHeight();
                    var margin2 = current2.margin;
                    if (current2.style != AlterGuiLayoutUtility.spaceStyle)
                    {
                        if (!flag2)
                        {
                            num = Mathf.Min(margin2.top, num);
                            num2 = Mathf.Min(margin2.bottom, num2);
                        }
                        else
                        {
                            num = margin2.top;
                            num2 = margin2.bottom;
                            flag2 = false;
                        }
                        childMinHeight = Mathf.Max(current2.minHeight, childMinHeight);
                        childMaxHeight = Mathf.Max(current2.maxHeight, childMaxHeight);
                    }
                    stretchableCountY += current2.stretchHeight;
                }
            }
            float num5;
            float num6;
            if (base.style != AlterGuiStyle.none || userSpecifiedHeight)
            {
                num5 = Mathf.Max(base.style.padding.top, num);
                num6 = Mathf.Max(base.style.padding.bottom, num2);
            }
            else
            {
                m_Margin.top = num;
                m_Margin.bottom = num2;
                num6 = (num5 = 0f);
            }
            minHeight = Mathf.Max(minHeight, childMinHeight + num5 + num6);
            if (maxHeight == 0f)
            {
                stretchHeight += stretchableCountY + ((!base.style.stretchHeight) ? 0 : 1);
                maxHeight = childMaxHeight + num5 + num6;
            }
            else
            {
                stretchHeight = 0;
            }
            maxHeight = Mathf.Max(maxHeight, minHeight);
            if (base.style.fixedHeight != 0f)
            {
                maxHeight = (minHeight = base.style.fixedHeight);
                stretchHeight = 0;
            }
        }



        public override void SetVertical(float y, float height)
        {
            base.SetVertical(y, height);
            if (entries.Count == 0)
            {
                return;
            }
            var padding = base.style.padding;
            if (resetCoords)
            {
                y = 0f;
            }
            if (isVertical)
            {
                if (base.style != AlterGuiStyle.none)
                {
                    float num = padding.top;
                    float num2 = padding.bottom;
                    if (entries.Count != 0)
                    {
                        num = Mathf.Max(num, entries[0].margin.top);
                        num2 = Mathf.Max(num2, entries[entries.Count - 1].margin.bottom);
                    }
                    y += num;
                    height -= num2 + num;
                }
                var num3 = height - spacing * (entries.Count - 1);
                var t = 0f;
                if (childMinHeight != childMaxHeight)
                {
                    t = Mathf.Clamp((num3 - childMinHeight) / (childMaxHeight - childMinHeight), 0f, 1f);
                }
                var num4 = 0f;
                if (num3 > childMaxHeight && stretchableCountY > 0)
                {
                    num4 = (num3 - childMaxHeight) / stretchableCountY;
                }
                var num5 = 0;
                var flag = true;
                foreach (var current in entries)
                {
                    var num6 = Mathf.Lerp(current.minHeight, current.maxHeight, t);
                    num6 += num4 * current.stretchHeight;
                    if (current.style != AlterGuiLayoutUtility.spaceStyle)
                    {
                        var num7 = current.margin.top;
                        if (flag)
                        {
                            num7 = 0;
                            flag = false;
                        }
                        var num8 = (num5 <= num7) ? num7 : num5;
                        y += num8;
                        num5 = current.margin.bottom;
                    }
                    current.SetVertical(Mathf.Round(y), Mathf.Round(num6));
                    y += num6 + spacing;
                }
            }
            else
            {
                if (base.style != AlterGuiStyle.none)
                {
                    foreach (var current2 in entries)
                    {
                        float num9 = Mathf.Max(current2.margin.top, padding.top);
                        var y2 = y + num9;
                        var num10 = height - Mathf.Max(current2.margin.bottom, padding.bottom) - num9;
                        if (current2.stretchHeight != 0)
                        {
                            current2.SetVertical(y2, num10);
                        }
                        else
                        {
                            current2.SetVertical(y2, Mathf.Clamp(num10, current2.minHeight, current2.maxHeight));
                        }
                    }
                }
                else
                {
                    var num11 = y - margin.top;
                    var num12 = height + margin.vertical;
                    foreach (var current3 in entries)
                    {
                        if (current3.stretchHeight != 0)
                        {
                            current3.SetVertical(num11 + current3.margin.top, num12 - current3.margin.vertical);
                        }
                        else
                        {
                            current3.SetVertical(num11 + current3.margin.top, Mathf.Clamp(num12 - current3.margin.vertical, current3.minHeight, current3.maxHeight));
                        }
                    }
                }
            }
        }
        public override string ToString()
        {
            var text = string.Empty;
            var text2 = string.Empty;
            for (var i = 0; i < indent; i++)
            {
                text2 += " ";
            }
            var text3 = text;
            text = string.Concat(new object[]
			{
				text3,
				base.ToString(),
				" Margins: ",
				childMinHeight,
				" {\n"
			});
            indent += 4;
            foreach (var current in entries)
            {
                text = text + current + "\n";
            }
            text = text + text2 + "}";
            indent -= 4;
            return text;
        }
    }


    public class AlterGuiLayoutUtility
    {
        internal sealed class AltrrLayoutCache
        {
            internal AlterGuiLayoutGroup topLevel = new AlterGuiLayoutGroup();
            internal GenericStack layoutGroups = new GenericStack();
            internal AlterGuiLayoutGroup windows = new AlterGuiLayoutGroup();
            internal AltrrLayoutCache()
            {
                layoutGroups.Push(topLevel);
            }
            internal AltrrLayoutCache(AltrrLayoutCache other)
            {
                topLevel = other.topLevel;
                layoutGroups = other.layoutGroups;
                windows = other.windows;
            }
        }
        private static Dictionary<int, AltrrLayoutCache> storedLayouts = new Dictionary<int, AltrrLayoutCache>();
        private static Dictionary<int, AltrrLayoutCache> storedWindows = new Dictionary<int, AltrrLayoutCache>();
        internal static AltrrLayoutCache current = new AltrrLayoutCache();
        private static Rect kDummyRect = new Rect(0f, 0f, 1f, 1f);
        private static AlterGuiStyle s_SpaceStyle;
        internal static AlterGuiLayoutGroup topLevel
        {
            get
            {
                return current.topLevel;
            }
        }
        internal static AlterGuiStyle spaceStyle
        {
            get
            {
                if (s_SpaceStyle == null)
                {
                    s_SpaceStyle = new AlterGuiStyle();
                }
                s_SpaceStyle.stretchWidth = false;
                return s_SpaceStyle;
            }
        }
        internal static AltrrLayoutCache SelectIDList(int instanceID, bool isWindow)
        {
            var dictionary = (!isWindow) ? storedLayouts : storedWindows;
            AltrrLayoutCache layoutCache;
            if (!dictionary.TryGetValue(instanceID, out layoutCache))
            {
                layoutCache = new AltrrLayoutCache();
                dictionary[instanceID] = layoutCache;
            }
            current.topLevel = layoutCache.topLevel;
            current.layoutGroups = layoutCache.layoutGroups;
            current.windows = layoutCache.windows;
            return layoutCache;
        }
        internal static void Begin(int instanceID)
        {
            var layoutCache = SelectIDList(instanceID, false);
            if (Event.current.type == EventType.Layout)
            {
                current.topLevel = (layoutCache.topLevel = new AlterGuiLayoutGroup());
                current.layoutGroups.Clear();
                current.layoutGroups.Push(current.topLevel);
                current.windows = (layoutCache.windows = new AlterGuiLayoutGroup());
            }
            else
            {
                current.topLevel = layoutCache.topLevel;
                current.layoutGroups = layoutCache.layoutGroups;
                current.windows = layoutCache.windows;
            }
        }
        internal static void BeginWindow(int windowID, AlterGuiStyle style, AlterGuiLayoutOption[] options)
        {
            var layoutCache = SelectIDList(windowID, true);
            if (Event.current.type == EventType.Layout)
            {
                current.topLevel = (layoutCache.topLevel = new AlterGuiLayoutGroup());
                current.topLevel.style = style;
                current.topLevel.windowID = windowID;
                if (options != null)
                {
                    current.topLevel.ApplyOptions(options);
                }
                current.layoutGroups.Clear();
                current.layoutGroups.Push(current.topLevel);
                current.windows = (layoutCache.windows = new AlterGuiLayoutGroup());
            }
            else
            {
                current.topLevel = layoutCache.topLevel;
                current.layoutGroups = layoutCache.layoutGroups;
                current.windows = layoutCache.windows;
            }
        }
        public static void BeginGroup(string GroupName)
        {
        }
        public static void EndGroup(string groupName)
        {
        }
        internal static void Layout()
        {
            if (current.topLevel.windowID == -1)
            {
                current.topLevel.CalcWidth();
                current.topLevel.SetHorizontal(0f, Mathf.Min(Screen.width, current.topLevel.maxWidth));
                current.topLevel.CalcHeight();
                current.topLevel.SetVertical(0f, Mathf.Min(Screen.height, current.topLevel.maxHeight));
                LayoutFreeGroup(current.windows);
            }
            else
            {
                LayoutSingleGroup(current.topLevel);
                LayoutFreeGroup(current.windows);
            }
        }
        internal static void LayoutFromEditorWindow()
        {
            current.topLevel.CalcWidth();
            current.topLevel.SetHorizontal(0f, Screen.width);
            current.topLevel.CalcHeight();
            current.topLevel.SetVertical(0f, Screen.height);
            LayoutFreeGroup(current.windows);
        }
        internal static float LayoutFromInspector(float width)
        {
            if (current.topLevel != null && current.topLevel.windowID == -1)
            {
                current.topLevel.CalcWidth();
                current.topLevel.SetHorizontal(0f, width);
                current.topLevel.CalcHeight();
                current.topLevel.SetVertical(0f, Mathf.Min(Screen.height, current.topLevel.maxHeight));
                var minHeight = current.topLevel.minHeight;
                LayoutFreeGroup(current.windows);
                return minHeight;
            }
            if (current.topLevel != null)
            {
                LayoutSingleGroup(current.topLevel);
            }
            return 0f;
        }
        internal static void LayoutFreeGroup(AlterGuiLayoutGroup toplevel)
        {
            using (var enumerator = toplevel.entries.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var i = (AlterGuiLayoutGroup) enumerator.Current;
                    LayoutSingleGroup(i);
                }
            }
            toplevel.ResetCursor();
        }
        private static void LayoutSingleGroup(AlterGuiLayoutGroup i)
        {
            if (!i.isWindow)
            {
                var minWidth = i.minWidth;
                var maxWidth = i.maxWidth;
                i.CalcWidth();
                i.SetHorizontal(i.rect.x, Mathf.Clamp(i.maxWidth, minWidth, maxWidth));
                var minHeight = i.minHeight;
                var maxHeight = i.maxHeight;
                i.CalcHeight();
                i.SetVertical(i.rect.y, Mathf.Clamp(i.maxHeight, minHeight, maxHeight));
            }
            else
            {
                i.CalcWidth();
                var rect = Internal_GetWindowRect(i.windowID);
                i.SetHorizontal(rect.x, Mathf.Clamp(rect.width, i.minWidth, i.maxWidth));
                i.CalcHeight();
                i.SetVertical(rect.y, Mathf.Clamp(rect.height, i.minHeight, i.maxHeight));
                Internal_MoveWindow(i.windowID, i.rect);
            }
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern Rect Internal_GetWindowRect(int windowID);
        private static void Internal_MoveWindow(int windowID, Rect r)
        {
            INTERNAL_CALL_Internal_MoveWindow(windowID, ref r);
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void INTERNAL_CALL_Internal_MoveWindow(int windowID, ref Rect r);
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern Rect GetWindowsBounds();
        [SecuritySafeCritical]
        private static AlterGuiLayoutGroup CreateGUILayoutGroupInstanceOfType(Type LayoutType)
        {
            if (!typeof(AlterGuiLayoutGroup).IsAssignableFrom(LayoutType))
            {
                throw new ArgumentException("LayoutType needs to be of type GUILayoutGroup");
            }
            return (AlterGuiLayoutGroup)Activator.CreateInstance(LayoutType);
        }
        internal static AlterGuiLayoutGroup BeginLayoutGroup(AlterGuiStyle style, AlterGuiLayoutOption[] options, Type LayoutType)
        {
            var type = Event.current.type;
            AlterGuiLayoutGroup gUILayoutGroup;
            if (type != EventType.Layout && type != EventType.Used)
            {
                gUILayoutGroup = (current.topLevel.GetNext() as AlterGuiLayoutGroup);
                if (gUILayoutGroup == null)
                {
                    throw new ArgumentException("GUILayout: Mismatched LayoutGroup." + Event.current.type);
                }
                gUILayoutGroup.ResetCursor();
            }
            else
            {
                gUILayoutGroup = CreateGUILayoutGroupInstanceOfType(LayoutType);
                gUILayoutGroup.style = style;
                if (options != null)
                {
                    gUILayoutGroup.ApplyOptions(options);
                }
                current.topLevel.Add(gUILayoutGroup);
            }
            current.layoutGroups.Push(gUILayoutGroup);
            current.topLevel = gUILayoutGroup;
            return gUILayoutGroup;
        }
        internal static void EndLayoutGroup()
        {
            current.layoutGroups.Pop();
            current.topLevel = (AlterGuiLayoutGroup)current.layoutGroups.Peek();
        }
        internal static AlterGuiLayoutGroup BeginLayoutArea(AlterGuiStyle style, Type LayoutType)
        {
            var type = Event.current.type;
            AlterGuiLayoutGroup gUILayoutGroup;
            if (type != EventType.Layout && type != EventType.Used)
            {
                gUILayoutGroup = (current.windows.GetNext() as AlterGuiLayoutGroup);
                if (gUILayoutGroup == null)
                {
                    throw new ArgumentException("GUILayout: Mismatched LayoutGroup." + Event.current.type);
                }
                gUILayoutGroup.ResetCursor();
            }
            else
            {
                gUILayoutGroup = CreateGUILayoutGroupInstanceOfType(LayoutType);
                gUILayoutGroup.style = style;
                current.windows.Add(gUILayoutGroup);
            }
            current.layoutGroups.Push(gUILayoutGroup);
            current.topLevel = gUILayoutGroup;
            return gUILayoutGroup;
        }
        internal static AlterGuiLayoutGroup DoBeginLayoutArea(AlterGuiStyle style, Type LayoutType)
        {
            return BeginLayoutArea(style, LayoutType);
        }
        public static Rect GetRect(AlterGUIContent content, AlterGuiStyle style)
        {
            return DoGetRect(content, style, null);
        }
        public static Rect GetRect(AlterGUIContent content, AlterGuiStyle style, params AlterGuiLayoutOption[] options)
        {
            return DoGetRect(content, style, options);
        }
        private static Rect DoGetRect(AlterGUIContent content, AlterGuiStyle style, AlterGuiLayoutOption[] options)
        {
           AlterGUIUtility.CheckOnGUI();
            
            var type = Event.current.type;
            if (type == EventType.Layout)
            {
                if (style.isHeightDependantOnWidth)
                {
                    current.topLevel.Add(new AlterGuiWordWrapSizer(style, content, options));
                }
                else
                {
                    var vector = style.CalcSize(content);
                    current.topLevel.Add(new AlterGuiLayoutEntry(vector.x, vector.x, vector.y, vector.y, style, options));
                }
                return kDummyRect;
            }
            if (type != EventType.Used)
            {
                return current.topLevel.GetNext().rect;
            }
            return kDummyRect;
        }
        public static Rect GetRect(float width, float height)
        {
            return DoGetRect(width, width, height, height, AlterGuiStyle.none, null);
        }
        public static Rect GetRect(float width, float height, AlterGuiStyle style)
        {
            return DoGetRect(width, width, height, height, style, null);
        }
        public static Rect GetRect(float width, float height, params AlterGuiLayoutOption[] options)
        {
            return DoGetRect(width, width, height, height, AlterGuiStyle.none, options);
        }
        public static Rect GetRect(float width, float height, AlterGuiStyle style, params AlterGuiLayoutOption[] options)
        {
            return DoGetRect(width, width, height, height, style, options);
        }
        public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight)
        {
            return DoGetRect(minWidth, maxWidth, minHeight, maxHeight, AlterGuiStyle.none, null);
        }
        public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, AlterGuiStyle style)
        {
            return DoGetRect(minWidth, maxWidth, minHeight, maxHeight, style, null);
        }
        public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, params AlterGuiLayoutOption[] options)
        {
            return DoGetRect(minWidth, maxWidth, minHeight, maxHeight, AlterGuiStyle.none, options);
        }
        public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, AlterGuiStyle style, params AlterGuiLayoutOption[] options)
        {
            return DoGetRect(minWidth, maxWidth, minHeight, maxHeight, style, options);
        }
        private static Rect DoGetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, AlterGuiStyle style, AlterGuiLayoutOption[] options)
        {
            var type = Event.current.type;
            if (type == EventType.Layout)
            {
                current.topLevel.Add(new AlterGuiLayoutEntry(minWidth, maxWidth, minHeight, maxHeight, style, options));
                return kDummyRect;
            }
            if (type != EventType.Used)
            {
                return current.topLevel.GetNext().rect;
            }
            return kDummyRect;
        }
        public static Rect GetLastRect()
        {
            var type = Event.current.type;
            if (type == EventType.Layout)
            {
                return kDummyRect;
            }
            if (type != EventType.Used)
            {
                return current.topLevel.GetLast();
            }
            return kDummyRect;
        }
        public static Rect GetAspectRect(float aspect)
        {
            return DoGetAspectRect(aspect, AlterGuiStyle.none, null);
        }
        public static Rect GetAspectRect(float aspect, AlterGuiStyle style)
        {
            return DoGetAspectRect(aspect, style, null);
        }
        public static Rect GetAspectRect(float aspect, params AlterGuiLayoutOption[] options)
        {
            return DoGetAspectRect(aspect, AlterGuiStyle.none, options);
        }
        public static Rect GetAspectRect(float aspect, AlterGuiStyle style, params AlterGuiLayoutOption[] options)
        {
            return DoGetAspectRect(aspect, AlterGuiStyle.none, options);
        }
        private static Rect DoGetAspectRect(float aspect, AlterGuiStyle style, AlterGuiLayoutOption[] options)
        {
            var type = Event.current.type;
            if (type == EventType.Layout)
            {
                current.topLevel.Add(new GUIAspectSizer(aspect, options));
                return kDummyRect;
            }
            if (type != EventType.Used)
            {
                return current.topLevel.GetNext().rect;
            }
            return kDummyRect;
        }
    }



    internal sealed class GUIAspectSizer : AlterGuiLayoutEntry
    {
        private readonly float aspect;
        public GUIAspectSizer(float aspect, AlterGuiLayoutOption[] options): base(0f, 0f, 0f, 0f, AlterGuiStyle.none)
        {
            this.aspect = aspect;
            ApplyOptions(options);
        }
        public override void CalcHeight()
        {
            minHeight = (maxHeight = rect.width / aspect);
        }
    }



    internal sealed class GUIClip
    {
        public static extern bool enabled
        {
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
        }
        public static extern Rect topmostRect
        {
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
        }
        public static extern Rect visibleRect
        {
            [WrapperlessIcall]
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
        }
        internal static void Push(Rect screenRect, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)
        {
            INTERNAL_CALL_Push(ref screenRect, ref scrollOffset, ref renderOffset, resetOffset);
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void INTERNAL_CALL_Push(ref Rect screenRect, ref Vector2 scrollOffset, ref Vector2 renderOffset, bool resetOffset);
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Pop();
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern Rect GetTopRect();
        public static Vector2 Unclip(Vector2 pos)
        {
            Unclip_Vector2(ref pos);
            return pos;
        }
        private static void Unclip_Vector2(ref Vector2 pos)
        {
            INTERNAL_CALL_Unclip_Vector2(ref pos);
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void INTERNAL_CALL_Unclip_Vector2(ref Vector2 pos);
        public static Rect Unclip(Rect rect)
        {
            Unclip_Rect(ref rect);
            return rect;
        }
        private static void Unclip_Rect(ref Rect rect)
        {
            INTERNAL_CALL_Unclip_Rect(ref rect);
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void INTERNAL_CALL_Unclip_Rect(ref Rect rect);
        public static Vector2 Clip(Vector2 absolutePos)
        {
            Clip_Vector2(ref absolutePos);
            return absolutePos;
        }
        private static void Clip_Vector2(ref Vector2 absolutePos)
        {
            INTERNAL_CALL_Clip_Vector2(ref absolutePos);
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void INTERNAL_CALL_Clip_Vector2(ref Vector2 absolutePos);
        public static Rect Clip(Rect absoluteRect)
        {
            Internal_Clip_Rect(ref absoluteRect);
            return absoluteRect;
        }
        private static void Internal_Clip_Rect(ref Rect absoluteRect)
        {
            INTERNAL_CALL_Internal_Clip_Rect(ref absoluteRect);
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void INTERNAL_CALL_Internal_Clip_Rect(ref Rect absoluteRect);
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern void Reapply();
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal static extern Matrix4x4 GetMatrix();
        internal static void SetMatrix(Matrix4x4 m)
        {
            INTERNAL_CALL_SetMatrix(ref m);
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void INTERNAL_CALL_SetMatrix(ref Matrix4x4 m);
        public static Vector2 GetAbsoluteMousePosition()
        {
            Vector2 result;
            Internal_GetAbsoluteMousePosition(out result);
            return result;
        }
        [WrapperlessIcall]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void Internal_GetAbsoluteMousePosition(out Vector2 output);
    }
}


