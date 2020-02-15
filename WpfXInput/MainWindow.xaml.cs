using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WpfXInput
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private XInputController XI;
        DispatcherTimer dt, reconnect;

        public MainWindow()
        {
            InitializeComponent();
            XI = new XInputController();
            XI.deadband = 8;
            if (XI.connected == false)
            {
                Connect.Visibility = Visibility.Collapsed;
                return;
            }
            dt = new DispatcherTimer();
            dt.Tick += new EventHandler(flushXInput);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1000/144);
            dt.Start();
            reconnect = new DispatcherTimer();
            reconnect.Interval = new TimeSpan(0, 0, 1);
            reconnect.Tick += new EventHandler(Xreconnect);
        }
        
        private void Xreconnect(object sender, EventArgs e)
        {
            if (XI.Reconnect())
            {
                Connect.Visibility = Visibility.Visible;
                reconnect.Stop();
                dt.Start();
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (XI != null)
            XI.Reconnect((SharpDX.XInput.UserIndex)((System.Windows.Controls.Primitives.Selector)sender).SelectedItem);
        }

        private void flushXInput(object sender, EventArgs e)
        {
            XI.Update();

            if (XI.connected == false)
            {
                Connect.Visibility = Visibility.Collapsed;
                dt.Stop();
                reconnect.Start();
                return;
            }
            pointl.ThumbPoint = XI.leftThumb;
            pointl.ButtonDown = XI.ButtonLeftThumb;
            pointr.ThumbPoint = XI.rightThumb;
            pointr.ButtonDown = XI.ButtonRightThumb;
            ButtonA_Back.Fill.Opacity = XI.ButtonA ? 0.6 : 0;
            ButtonB_Back.Fill.Opacity = XI.ButtonB ? 0.6 : 0;
            ButtonX_Back.Fill.Opacity = XI.ButtonX ? 0.6 : 0;
            ButtonY_Back.Fill.Opacity = XI.ButtonY ? 0.6 : 0;
            ButtonUP_Back.Visibility = XI.ButtonDPadUp ? Visibility.Visible : Visibility.Collapsed;
            ButtonDown_Back.Visibility = XI.ButtonDPadDown ? Visibility.Visible : Visibility.Collapsed;
            ButtonLeft_Back.Visibility = XI.ButtonDPadLeft ? Visibility.Visible : Visibility.Collapsed;
            ButtonRight_Back.Visibility = XI.ButtonDPadRight ? Visibility.Visible : Visibility.Collapsed;
            ButtonLB_Back.Visibility = XI.ButtonLeftShoulder ? Visibility.Visible : Visibility.Collapsed;
            ButtonRB_Back.Visibility = XI.ButtonRightShoulder ? Visibility.Visible : Visibility.Collapsed;
            ButtonStart_Back.Visibility = XI.ButtonStart ? Visibility.Visible : Visibility.Collapsed;
            ButtonBack_Back.Visibility = XI.ButtonBack ? Visibility.Visible : Visibility.Collapsed;
            LT_Trigger.Offset = 1 - ((XI.leftTrigger + 4) / 263);
            RT_Trigger.Offset = 1 - ((XI.rightTrigger + 4) / 263);
            //pointl.Margin = new Thickness(XI.leftThumb.X + Pgl.ActualWidth / 2, XI.leftThumb.Y + Pgl.ActualHeight / 2, 0, 0);
            //pointr.Margin = new Thickness(XI.rightThumb.X + Pgr.ActualWidth / 2, XI.rightThumb.Y + Pgr.ActualHeight / 2, 0, 0);
        }
    }
}
