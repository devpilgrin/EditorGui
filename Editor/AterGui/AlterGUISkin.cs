using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alter
{
    [ExecuteInEditMode]
    [Serializable]
    public class AlterGUISkin : ScriptableObject
    {
        internal delegate void SkinChangedDelegate();
        [SerializeField]
        private Font m_Font;
        [SerializeField]
        private AlterGuiStyle m_box;
        [SerializeField]
        private AlterGuiStyle m_button;
        [SerializeField]
        private AlterGuiStyle m_toggle;
        [SerializeField]
        private AlterGuiStyle m_label;
        [SerializeField]
        private AlterGuiStyle m_textField;
        [SerializeField]
        private AlterGuiStyle m_textArea;
        [SerializeField]
        private AlterGuiStyle m_window;
        [SerializeField]
        private AlterGuiStyle m_horizontalSlider;
        [SerializeField]
        private AlterGuiStyle m_horizontalSliderThumb;
        [SerializeField]
        private AlterGuiStyle m_verticalSlider;
        [SerializeField]
        private AlterGuiStyle m_verticalSliderThumb;
        [SerializeField]
        private AlterGuiStyle m_horizontalScrollbar;
        [SerializeField]
        private AlterGuiStyle m_horizontalScrollbarThumb;
        [SerializeField]
        private AlterGuiStyle m_horizontalScrollbarLeftButton;
        [SerializeField]
        private AlterGuiStyle m_horizontalScrollbarRightButton;
        [SerializeField]
        private AlterGuiStyle m_verticalScrollbar;
        [SerializeField]
        private AlterGuiStyle m_verticalScrollbarThumb;
        [SerializeField]
        private AlterGuiStyle m_verticalScrollbarUpButton;
        [SerializeField]
        private AlterGuiStyle m_verticalScrollbarDownButton;
        [SerializeField]
        private AlterGuiStyle m_ScrollView;
        [SerializeField]
        internal AlterGuiStyle[] m_CustomStyles;
        [SerializeField]
        private readonly GUISettings m_Settings = new GUISettings();
        internal static AlterGuiStyle ms_Error;
        private Dictionary<string, AlterGuiStyle> styles;
        internal static SkinChangedDelegate m_SkinChanged;
        internal static AlterGUISkin current;
        public Font font
        {
            get
            {
                return m_Font;
            }
            set
            {
                m_Font = value;
                if (current == this)
                {
                    
                    AlterGuiStyle.SetDefaultFont(m_Font);
                }
                Apply();
            }
        }
        public AlterGuiStyle box
        {
            get
            {
                return m_box;
            }
            set
            {
                m_box = value;
                Apply();
            }
        }
        public AlterGuiStyle label
        {
            get
            {
                return m_label;
            }
            set
            {
                m_label = value;
                Apply();
            }
        }
        public AlterGuiStyle textField
        {
            get
            {
                return m_textField;
            }
            set
            {
                m_textField = value;
                Apply();
            }
        }
        public AlterGuiStyle textArea
        {
            get
            {
                return m_textArea;
            }
            set
            {
                m_textArea = value;
                Apply();
            }
        }
        public AlterGuiStyle button
        {
            get
            {
                return m_button;
            }
            set
            {
                m_button = value;
                Apply();
            }
        }
        public AlterGuiStyle toggle
        {
            get
            {
                return m_toggle;
            }
            set
            {
                m_toggle = value;
                Apply();
            }
        }
        public AlterGuiStyle window
        {
            get
            {
                return m_window;
            }
            set
            {
                m_window = value;
                Apply();
            }
        }
        public AlterGuiStyle horizontalSlider
        {
            get
            {
                return m_horizontalSlider;
            }
            set
            {
                m_horizontalSlider = value;
                Apply();
            }
        }
        public AlterGuiStyle horizontalSliderThumb
        {
            get
            {
                return m_horizontalSliderThumb;
            }
            set
            {
                m_horizontalSliderThumb = value;
                Apply();
            }
        }
        public AlterGuiStyle verticalSlider
        {
            get
            {
                return m_verticalSlider;
            }
            set
            {
                m_verticalSlider = value;
                Apply();
            }
        }
        public AlterGuiStyle verticalSliderThumb
        {
            get
            {
                return m_verticalSliderThumb;
            }
            set
            {
                m_verticalSliderThumb = value;
                Apply();
            }
        }
        public AlterGuiStyle horizontalScrollbar
        {
            get
            {
                return m_horizontalScrollbar;
            }
            set
            {
                m_horizontalScrollbar = value;
                Apply();
            }
        }
        public AlterGuiStyle horizontalScrollbarThumb
        {
            get
            {
                return m_horizontalScrollbarThumb;
            }
            set
            {
                m_horizontalScrollbarThumb = value;
                Apply();
            }
        }
        public AlterGuiStyle horizontalScrollbarLeftButton
        {
            get
            {
                return m_horizontalScrollbarLeftButton;
            }
            set
            {
                m_horizontalScrollbarLeftButton = value;
                Apply();
            }
        }
        public AlterGuiStyle horizontalScrollbarRightButton
        {
            get
            {
                return m_horizontalScrollbarRightButton;
            }
            set
            {
                m_horizontalScrollbarRightButton = value;
                Apply();
            }
        }
        public AlterGuiStyle verticalScrollbar
        {
            get
            {
                return m_verticalScrollbar;
            }
            set
            {
                m_verticalScrollbar = value;
                Apply();
            }
        }
        public AlterGuiStyle verticalScrollbarThumb
        {
            get
            {
                return m_verticalScrollbarThumb;
            }
            set
            {
                m_verticalScrollbarThumb = value;
                Apply();
            }
        }
        public AlterGuiStyle verticalScrollbarUpButton
        {
            get
            {
                return m_verticalScrollbarUpButton;
            }
            set
            {
                m_verticalScrollbarUpButton = value;
                Apply();
            }
        }
        public AlterGuiStyle verticalScrollbarDownButton
        {
            get
            {
                return m_verticalScrollbarDownButton;
            }
            set
            {
                m_verticalScrollbarDownButton = value;
                Apply();
            }
        }
        public AlterGuiStyle scrollView
        {
            get
            {
                return m_ScrollView;
            }
            set
            {
                m_ScrollView = value;
                Apply();
            }
        }
        public AlterGuiStyle[] customStyles
        {
            get
            {
                return m_CustomStyles;
            }
            set
            {
                m_CustomStyles = value;
                Apply();
            }
        }
        public GUISettings settings
        {
            get
            {
                return m_Settings;
            }
        }
        internal static AlterGuiStyle error
        {
            get
            {
                if (ms_Error == null)
                {
                    ms_Error = new AlterGuiStyle();
                }
                return ms_Error;
            }
        }
        public AlterGUISkin()
        {
            m_CustomStyles = new AlterGuiStyle[1];
        }
        internal void OnEnable()
        {
            Apply();
            foreach (AlterGuiStyle gUIStyle in styles.Values)
            {
                gUIStyle.CreateObjectReferences();
            }
        }
        internal void Apply()
        {
            if (m_CustomStyles == null)
            {
                Debug.Log("custom styles is null");
            }
            BuildStyleCache();
        }
        private void BuildStyleCache()
        {
            if (m_box == null)
            {
                m_box = new AlterGuiStyle();
            }
            if (m_button == null)
            {
                m_button = new AlterGuiStyle();
            }
            if (m_toggle == null)
            {
                m_toggle = new AlterGuiStyle();
            }
            if (m_label == null)
            {
                m_label = new AlterGuiStyle();
            }
            if (m_window == null)
            {
                m_window = new AlterGuiStyle();
            }
            if (m_textField == null)
            {
                m_textField = new AlterGuiStyle();
            }
            if (m_textArea == null)
            {
                m_textArea = new AlterGuiStyle();
            }
            if (m_horizontalSlider == null)
            {
                m_horizontalSlider = new AlterGuiStyle();
            }
            if (m_horizontalSliderThumb == null)
            {
                m_horizontalSliderThumb = new AlterGuiStyle();
            }
            if (m_verticalSlider == null)
            {
                m_verticalSlider = new AlterGuiStyle();
            }
            if (m_verticalSliderThumb == null)
            {
                m_verticalSliderThumb = new AlterGuiStyle();
            }
            if (m_horizontalScrollbar == null)
            {
                m_horizontalScrollbar = new AlterGuiStyle();
            }
            if (m_horizontalScrollbarThumb == null)
            {
                m_horizontalScrollbarThumb = new AlterGuiStyle();
            }
            if (m_horizontalScrollbarLeftButton == null)
            {
                m_horizontalScrollbarLeftButton = new AlterGuiStyle();
            }
            if (m_horizontalScrollbarRightButton == null)
            {
                m_horizontalScrollbarRightButton = new AlterGuiStyle();
            }
            if (m_verticalScrollbar == null)
            {
                m_verticalScrollbar = new AlterGuiStyle();
            }
            if (m_verticalScrollbarThumb == null)
            {
                m_verticalScrollbarThumb = new AlterGuiStyle();
            }
            if (m_verticalScrollbarUpButton == null)
            {
                m_verticalScrollbarUpButton = new AlterGuiStyle();
            }
            if (m_verticalScrollbarDownButton == null)
            {
                m_verticalScrollbarDownButton = new AlterGuiStyle();
            }
            if (m_ScrollView == null)
            {
                m_ScrollView = new AlterGuiStyle();
            }
            styles = new Dictionary<string, AlterGuiStyle>(StringComparer.OrdinalIgnoreCase);
            styles["box"] = m_box;
            m_box.name = "box";
            styles["button"] = m_button;
            m_button.name = "button";
            styles["toggle"] = m_toggle;
            m_toggle.name = "toggle";
            styles["label"] = m_label;
            m_label.name = "label";
            styles["window"] = m_window;
            m_window.name = "window";
            styles["textfield"] = m_textField;
            m_textField.name = "textfield";
            styles["textarea"] = m_textArea;
            m_textArea.name = "textarea";
            styles["horizontalslider"] = m_horizontalSlider;
            m_horizontalSlider.name = "horizontalslider";
            styles["horizontalsliderthumb"] = m_horizontalSliderThumb;
            m_horizontalSliderThumb.name = "horizontalsliderthumb";
            styles["verticalslider"] = m_verticalSlider;
            m_verticalSlider.name = "verticalslider";
            styles["verticalsliderthumb"] = m_verticalSliderThumb;
            m_verticalSliderThumb.name = "verticalsliderthumb";
            styles["horizontalscrollbar"] = m_horizontalScrollbar;
            m_horizontalScrollbar.name = "horizontalscrollbar";
            styles["horizontalscrollbarthumb"] = m_horizontalScrollbarThumb;
            m_horizontalScrollbarThumb.name = "horizontalscrollbarthumb";
            styles["horizontalscrollbarleftbutton"] = m_horizontalScrollbarLeftButton;
            m_horizontalScrollbarLeftButton.name = "horizontalscrollbarleftbutton";
            styles["horizontalscrollbarrightbutton"] = m_horizontalScrollbarRightButton;
            m_horizontalScrollbarRightButton.name = "horizontalscrollbarrightbutton";
            styles["verticalscrollbar"] = m_verticalScrollbar;
            m_verticalScrollbar.name = "verticalscrollbar";
            styles["verticalscrollbarthumb"] = m_verticalScrollbarThumb;
            m_verticalScrollbarThumb.name = "verticalscrollbarthumb";
            styles["verticalscrollbarupbutton"] = m_verticalScrollbarUpButton;
            m_verticalScrollbarUpButton.name = "verticalscrollbarupbutton";
            styles["verticalscrollbardownbutton"] = m_verticalScrollbarDownButton;
            m_verticalScrollbarDownButton.name = "verticalscrollbardownbutton";
            styles["scrollview"] = m_ScrollView;
            m_ScrollView.name = "scrollview";
            if (m_CustomStyles != null)
            {
                for (int i = 0; i < m_CustomStyles.Length; i++)
                {
                    if (m_CustomStyles[i] != null)
                    {
                        styles[m_CustomStyles[i].name] = m_CustomStyles[i];
                    }
                }
            }
            error.stretchHeight = true;
            error.normal.textColor = Color.red;
        }
        public AlterGuiStyle GetStyle(string styleName)
        {
            AlterGuiStyle gUIStyle = FindStyle(styleName);
            if (gUIStyle != null)
            {
                return gUIStyle;
            }
            Debug.LogWarning(string.Concat(new object[]
            {
                "Unable to find style '",
                styleName,
                "' in skin '",
                base.name,
                "' ",
                Event.current.type
            }));
            return error;
        }
        public AlterGuiStyle FindStyle(string styleName)
        {
            if (styles == null)
            {
                BuildStyleCache();
            }
            AlterGuiStyle result;
            if (styles.TryGetValue(styleName, out result))
            {
                return result;
            }
            return null;
        }
        internal void MakeCurrent()
        {
            current = this;
            AlterGuiStyle.SetDefaultFont(font);
            if (m_SkinChanged != null)
            {
                m_SkinChanged();
            }
        }
        public IEnumerator GetEnumerator()
        {
            if (styles == null)
            {
                BuildStyleCache();
            }
            return styles.Values.GetEnumerator();
        }
    }
}