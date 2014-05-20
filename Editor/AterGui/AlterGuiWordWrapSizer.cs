namespace Alter
{
    internal sealed class AlterGuiWordWrapSizer : AlterGuiLayoutEntry    {
        private readonly AlterGUIContent content;
        private readonly float forcedMinHeight;
        private readonly float forcedMaxHeight;
        public AlterGuiWordWrapSizer(AlterGuiStyle _style, AlterGUIContent _content, AlterGuiLayoutOption[] options)
            : base(0f, 0f, 0f, 0f, _style)
        {
            content = new AlterGUIContent(_content);
            base.ApplyOptions(options);
            forcedMinHeight = minHeight;
            forcedMaxHeight = maxHeight;
        }
        public override void CalcWidth()
        {
            float minWidth = 0;
            float maxWidth = 0;
            if (minWidth == 0f || maxWidth == 0f)
            {
                style.CalcMinMaxWidth(content, out minWidth, out maxWidth);
                
                if (this.minWidth == 0f)
                {
                    this.minWidth = minWidth;
                }
                if (this.maxWidth == 0f)
                {
                    this.maxWidth = maxWidth;
                }
            }
        }

        public override void CalcHeight()
        {
            if (forcedMinHeight == 0f || forcedMaxHeight == 0f)
            {
                float num = style.CalcHeight(content, rect.width);
                if (forcedMinHeight == 0f)
                {
                    minHeight = num;
                }
                else
                {
                    minHeight = forcedMinHeight;
                }
                if (forcedMaxHeight == 0f)
                {
                    maxHeight = num;
                }
                else
                {
                    maxHeight = forcedMaxHeight;
                }
            }
        }
    }
}