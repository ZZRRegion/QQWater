using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QQWater.Windows
{
    public class BaseWindow:Window
    {
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
            base.OnMouseLeftButtonDown(e);
        }
    }
}
