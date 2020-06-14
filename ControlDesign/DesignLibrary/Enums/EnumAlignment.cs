using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignLibrary
{
    /// <summary>
    /// 装饰器相对于控件本身的位置
    /// </summary>
    public enum EnumAlignment
    {
        [Description("左上")]
        TopLeft,
        [Description("中上")]
        TopCenter,
        [Description("右上")]
        TopRight,
        [Description("左中")]
        CenterLeft,
        [Description("中")]
        Middle,
        [Description("右中")]
        CenterRight,
        [Description("左下")]
        BottomLeft,
        [Description("中下")]
        BottomCenter,
        [Description("右下")]
        BottomRight,
    }
}
