/* 
 * ��� ����������� ������ c Gui ���������� ��������
 * c���������� GUI Unuty Engine
 * �������� ���� ����������� � ���, ��� ������ ��������� ��������� ��������
 * � ��������, � ��� ��������� ��������� �������� - ��� Unitman-�� � ������
 * �� ������.
 * ����� ����������.
 * ����, ���-�� ������������, ��� �������� ������ ����������� ���� GUI
*/

using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Alter
{
    /// <summary>
    /// ����� ��� �������� ������ ��������� ������������ ����������.
    /// �������� ��� ������� �������� ���������� ������� 3 ���� ������
    /// 1.�����(Text)
    /// 2.�����������(Image)
    /// 3.���������(Tooltip)
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class AlterGUIContent
    {
        [SerializeField]
        private bool maintain_State;
        [SerializeField]
        private string maintain_Text = string.Empty;
        [SerializeField]
        private Texture maintain_Image;
        [SerializeField]
        private string maintain_Tooltip = string.Empty;

        public static AlterGUIContent none = new AlterGUIContent(string.Empty);

        private static readonly AlterGUIContent static_State = new AlterGUIContent();
        private static readonly AlterGUIContent static_Text = new AlterGUIContent();
        private static readonly AlterGUIContent static_Image = new AlterGUIContent();
        private static readonly AlterGUIContent static_TextImage = new AlterGUIContent();
        private static readonly AlterGUIContent static_TextImageBool = new AlterGUIContent();
        
        /// <summary>
        /// ��� ��������: ������
        /// ������������ ��� ������ � 
        /// �������� ���������� ������
        /// </summary>
        public string Text
        {
            get
            {
                return maintain_Text;
            }
            set
            {
                maintain_Text = value;
            }
        }

        /// <summary>
        /// ��� ��������: ������� ����������
        /// ������������ ��� ���������� � ���������� ��������� �������
        /// </summary>
        public bool State
        {
            get
            {
                return maintain_State;
            }
            set
            {
                maintain_State = value;
            }
        }
		
        /// <summary>
        /// ��� ��������: �����������
        /// ������������ ��� ������ � 
        /// �������� ���������� �����������
        /// </summary>
        public Texture Image
        {
            get
            {
                return maintain_Image;
            }
            set
            {
                maintain_Image = value;
            }
        }

        /// <summary>
        /// ��� ��������: ������
        /// ������������ ��� ������ � 
        /// �������� ���������� ����������� ���������
        /// </summary>
        public string Tooltip
        {
            get
            {
                return maintain_Tooltip;
            }
            set
            {
                maintain_Tooltip = value;
            }
        }
		
        internal int Hash
        {
            get
            {
                var result = 0;
                if (!string.IsNullOrEmpty(maintain_Text))
                {
                    result = maintain_Text.GetHashCode() * 37;
                }
                return result;
            }
        }
		
        /// <summary>
        /// ����������� �������� �� ���������.
        /// ������ �������� ���������� (AlterGUIContent.Text = "", AlterGUIContent.Image = null, AlterGUIContent.Tooltip = "")
        /// </summary>
        public AlterGUIContent()
        {
        }
        
        /// <summary>
        /// ����������� �������� �������� ��������� �������� (State)
        /// ������ �������� ���������� (AlterGUIContent.Text = "", AlterGUIContent.Image = Texture, AlterGUIContent.Tooltip = "", AlterGUIContent.State = bool )
        /// </summary>
        /// <param name="state">��������� �������� (State) ���: bool</param>
        public AlterGUIContent(bool state)
        {
            maintain_State = state;
        }
        
        /// <summary>
        /// ����������� �������� �������� �����(Text), �����������(Image) � ��������� �������� (State).
        /// ������ �������� ���������� (AlterGUIContent.Text = string, AlterGUIContent.Image = Texture, AlterGUIContent.Tooltip = "", AlterGUIContent.State = bool)
        /// </summary>
        /// <param name="text">�����(Text) ���: string</param>
        /// <param name="image">�����������(Image) ���: Texture</param>
        /// <param name="state">��������� �������� (State) ���: bool</param>
        public AlterGUIContent(string text, Texture image, bool state)
        {
            maintain_Text = text;
            maintain_Image = image;
            maintain_State = state;
        }
        
        /// <summary>
        /// ����������� �������� �������� �����(Text), �����������(Image), ����� ���������(Tooltip) � ��������� �������� (State).
        /// ������ �������� ���������� (AlterGUIContent.Text = "string", AlterGUIContent.Image = Texture, AlterGUIContent.Tooltip = "string", AlterGUIContent.State = bool)
        /// </summary>
        /// <param name="text">�����(Text) ���: string</param>
        /// <param name="image">�����������(Image) ���: Texture</param>
        /// <param name="tooltip">����� ���������(Tooltip) ���: string</param>
        /// <param name="state">��������� �������� (State) ���: bool</param>
        public AlterGUIContent(string text, Texture image, string tooltip, bool state)
        {
            maintain_Text = text;
            maintain_Image = image;
            maintain_Tooltip = tooltip;
            maintain_State = state;
        }
        
        #region ������������ ������ - Class Designers

        /// <summary>
        /// ����������� �������� �������� �����(Text), ����� ���������(Tooltip) � ��������� �������� (State).
        /// ������ �������� ���������� (AlterGUIContent.Text = "string", AlterGUIContent.Image = null, AlterGUIContent.Tooltip = "string", AlterGUIContent.State = bool)
        /// </summary>
        /// <param name="text">�����(Text) ���: string</param>
        /// <param name="tooltip">����� ���������(Tooltip) ���: string</param>
        /// <param name="state">��������� �������� (State) ���: bool</param>
        public AlterGUIContent(string text, string tooltip, bool state)
        {
            maintain_Text = text;
            maintain_Tooltip = tooltip;
            maintain_State = state;
        }

        /// <summary>
        /// ����������� �������� �������� �����������(Image), ����� ���������(Tooltip) � ��������� �������� (State).
        /// ������ �������� ���������� (AlterGUIContent.Text = "", AlterGUIContent.Image = Texture, AlterGUIContent.Tooltip = "string", AlterGUIContent.State = bool)
        /// </summary>
        /// <param name="image">�����������(Image) ���: Texture</param>
        /// <param name="tooltip">����� ���������(Tooltip) ���: string</param>
        /// <param name="state">��������� �������� (State) ���: bool</param>
        public AlterGUIContent(Texture image, string tooltip, bool state)
        {
            maintain_Image = image;
            maintain_Tooltip = tooltip;
            maintain_State = state;
        }

        /// <summary>
        /// ����������� �������� �������� �����(Text).
        /// ������ �������� ���������� (AlterGUIContent.Text = string, AlterGUIContent.Image = null, AlterGUIContent.Tooltip = "")
        /// </summary>
        /// <param name="text">�����(Text) ���: string</param>
        public AlterGUIContent(string text)
        {
            maintain_Text = text;
        }

        /// <summary>
        /// ����������� �������� �������� �����������(Image).
        /// ������ �������� ���������� (AlterGUIContent.Text = "", AlterGUIContent.Image = Texture, AlterGUIContent.Tooltip = "")
        /// </summary>
        /// <param name="image">�����������(Image) ���: Texture</param>
        public AlterGUIContent(Texture image)
        {
            maintain_Image = image;
        }

        /// <summary>
        /// ����������� �������� �������� �����(Text) � �����������(Image).
        /// ������ �������� ���������� (AlterGUIContent.Text = string, AlterGUIContent.Image = Texture, AlterGUIContent.Tooltip = "")
        /// </summary>
        /// <param name="text">�����(Text) ���: string</param>
        /// <param name="image">�����������(Image) ���: Texture</param>
        public AlterGUIContent(string text, Texture image)
        {
            maintain_Text = text;
            maintain_Image = image;
        }

        /// <summary>
        /// ����������� �������� �������� �����(Text) � ����� ���������(Tooltip).
        /// ������ �������� ���������� (AlterGUIContent.Text = string, AlterGUIContent.Image = null, AlterGUIContent.Tooltip = string)
        /// </summary>
        /// <param name="text">�����(Text) ���: string</param>
        /// <param name="tooltip">����� ���������(Tooltip) ���: string</param>
        public AlterGUIContent(string text, string tooltip)
        {
            maintain_Text = text;
            maintain_Tooltip = tooltip;
        }

        /// <summary>
        /// ����������� �������� �������� �����������(Image) � ����� ���������(Tooltip).
        /// ������ �������� ���������� (AlterGUIContent.Text = "", AlterGUIContent.Image = Texture, AlterGUIContent.Tooltip = "string")
        /// </summary>
        /// <param name="image">�����������(Image) ���: Texture</param>
        /// <param name="tooltip">����� ���������(Tooltip) ���: string</param>
        public AlterGUIContent(Texture image, string tooltip)
        {
            maintain_Image = image;
            maintain_Tooltip = tooltip;
        }

        /// <summary>
        /// ����������� �������� �������� �����(Text), �����������(Image) � ����� ���������(Tooltip).
        /// ������ �������� ���������� (AlterGUIContent.Text = "string", AlterGUIContent.Image = Texture, AlterGUIContent.Tooltip = "string")
        /// </summary>
        /// <param name="text">�����(Text) ���: string</param>
        /// <param name="image">�����������(Image) ���: Texture</param>
        /// <param name="tooltip">����� ���������(Tooltip) ���: string</param>
        public AlterGUIContent(string text, Texture image, string tooltip)
        {
            maintain_Text = text;
            maintain_Image = image;
            maintain_Tooltip = tooltip;
        }

        public AlterGUIContent(AlterGUIContent src)
        {
            maintain_Text = src.maintain_Text;
            maintain_Image = src.maintain_Image;
            maintain_Tooltip = src.maintain_Tooltip;
            maintain_State = src.maintain_State;
        }

        #endregion


        #region ��� - Cache

        internal static AlterGUIContent Temp(string t)
        {
            static_Text.maintain_Text = t;
            return static_Text;
        }

        internal static AlterGUIContent Temp(Texture i)
        {
            static_Image.maintain_Image = i;
            return static_Image;
        }

        internal static AlterGUIContent Temp(string t, Texture i)
        {
            static_TextImage.maintain_Text = t;
            static_TextImage.maintain_Image = i;
            return static_TextImage;
        }

        internal static AlterGUIContent Temp(bool b)
        {
            static_State.maintain_State = b;
            return static_State;
        }

        internal static AlterGUIContent Temp(string t, Texture i, bool b)
        {
            static_TextImageBool.maintain_Text = t;
            static_TextImageBool.maintain_Image = i;
            static_TextImageBool.maintain_State = b;
            return static_TextImageBool;
        }
        #endregion

        /// <summary>
        /// ������� ����
        /// </summary>
        internal static void ClearStaticCache()
        {
            static_Text.maintain_Text = null;
            static_Image.maintain_Image = null;
            static_State.maintain_State = false;
            static_TextImage.maintain_Text = null;
            static_TextImage.maintain_Image = null;
            static_TextImageBool.maintain_Image = null;
            static_TextImageBool.maintain_Text = null;
            static_TextImageBool.maintain_State = false;
        }

        #region ��� - Cache
        internal static AlterGUIContent[] Temp(string[] texts)
        {
            AlterGUIContent[] array = new AlterGUIContent[texts.Length];
            for (var i = 0; i < texts.Length; i++)
            {
                array[i] = new AlterGUIContent(texts[i]);
            }
            return array;
        }
        internal static AlterGUIContent[] Temp(Texture[] images)
        {
            AlterGUIContent[] array = new AlterGUIContent[images.Length];
            for (int i = 0; i < images.Length; i++)
            {
                array[i] = new AlterGUIContent(images[i]);
            }
            return array;
        }
        internal static AlterGUIContent[] Temp(bool[] state)
        {
            AlterGUIContent[] array = new AlterGUIContent[state.Length];
            for (int i = 0; i < state.Length; i++)
            {
                array[i] = new AlterGUIContent(state[i]);
            }
            return array;
        }
        #endregion
    
    }
}