using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQWater.ViewModels
{
    public class InfoWindowViewModel:NotifyPropertyChanged
    {
        private string info;
        public string Info
        {
            get => this.info;
            set
            {
                if(this.info != value)
                {
                    this.info = value;
                    this.OnPropertyChanged(nameof(Info));
                }
            }
        }
    }
}
