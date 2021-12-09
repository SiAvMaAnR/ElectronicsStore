using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElectronicsShop.ViewModels
{
    public class MainViewModel: BaseViewModel
    {
        private Page frameCurrentPage;
        public Page FrameCurrentPage
        {
            get { return frameCurrentPage; }
            set
            {
                frameCurrentPage = value;
                OnPropertyChanged(nameof(frameCurrentPage));
            }
        }
    
    }
}
