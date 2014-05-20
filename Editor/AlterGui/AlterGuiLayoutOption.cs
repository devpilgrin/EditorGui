namespace Alter
{
    public sealed class AlterGuiLayoutOption
    {
        internal enum Type
        {
            fixedWidth,
            fixedHeight,
            minWidth,
            maxWidth,
            minHeight,
            maxHeight,
            stretchWidth,
            stretchHeight,
            alignStart,
            alignMiddle,
            alignEnd,
            alignJustify,
            equalSize,
            spacing
        }
        internal readonly Type type;
        internal readonly object value;
        internal AlterGuiLayoutOption(Type type, object value)
        {
            this.type = type;
            this.value = value;
        }
    }
}