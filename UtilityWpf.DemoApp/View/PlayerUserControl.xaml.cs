
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using UtilityEnum;

namespace UtilityWpf.DemoApp
{
    /// <summary>
    /// Interaction logic for PlayerUserControl.xaml
    /// </summary>
    public partial class PlayerUserControl : UserControl
    {
        public PlayerUserControl()
        {
            InitializeComponent();
            this.DataContext = new PlayerViewModel();
        }

        internal class PlayerViewModel:NPC
        {
            private ProcessState processState;

            public int Value { get; private set; }

            public UtilityEnum.ProcessState ProcessState { set { processState = value;
                    switch(value)
                    {
                        case (ProcessState.Ready):
                            break;
                        case (ProcessState.Terminated):
                            dispatcherTimer.Stop();
                            Value = 0;
                            OnPropertyChanged(nameof(Value));
                            i = 0;
                         
                            break;
                        case (ProcessState.Running):
                            dispatcherTimer.Start();
                            break;
                        case (ProcessState.Blocked):
                            dispatcherTimer.Stop();
                            break;

                    }

                    OnPropertyChanged(nameof(ProcessState)); } }


            DispatcherTimer dispatcherTimer;
            public PlayerViewModel()
            {
                 dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
          
            }
            int i = 0;
            private void dispatcherTimer_Tick(object sender, EventArgs e)
            {
                
                Value = i * 10;
                OnPropertyChanged(nameof(Value));
                i++;
                if(Value==100)
                    dispatcherTimer.Stop();
            }
        }
    }
}
