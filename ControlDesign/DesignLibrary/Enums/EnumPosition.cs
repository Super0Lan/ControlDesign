using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignLibrary
{
    /// <summary>
    /// InputNumber 按钮位置
    /// </summary>
    public enum EnumPosition
    {
        [Description("俩边")]
        Strech,
        [Description("左边")]
        Left,
        [Description("右边")]
        Right,
    }
}
