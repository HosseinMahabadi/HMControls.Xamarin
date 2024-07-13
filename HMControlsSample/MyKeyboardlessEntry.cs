using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMControls;

namespace HMControlsSample;

public class MyKeyboardlessEntry : KeyboardlessEntry
{
    public override void ActionOnFocused()
    {
        Text += " Focused ";
    }
}
