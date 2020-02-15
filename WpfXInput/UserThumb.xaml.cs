using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfXInput
{
    /// <summary>
    /// UserThumb.xaml 的交互逻辑
    /// </summary>
    public partial class UserThumb : UserControl
    {
        private Point TP;
        private double ST, PZ;
        private BackStyle BS;

        public UserThumb()
        {
            InitializeComponent();
            TP = new Point(0, 0);
            SizeChanged += new SizeChangedEventHandler(OnSizeChange);
        }
        
        private void OnSizeChange(object sender, SizeChangedEventArgs e)
        {
            Back.Margin = new Thickness(
                ActualWidth / 4 - StrokeThickness,
                ActualHeight / 4 - StrokeThickness,
                ActualWidth / 4 - StrokeThickness,
                ActualHeight / 4 - StrokeThickness
            );
            Thumb.Width = ActualWidth / 2;
            Thumb.Height = ActualHeight / 2;
            //point.Width = ActualWidth / 50;
            //point.Height = ActualHeight / 50;
        }

        public enum BackStyle {
            Hidden = 0,
            Rectangle = 1,
            Ellipse = 2
        }

        public double StrokeThickness
        {
            get { return ST; }
            set
            {
                ST = value;
                Back.StrokeThickness = value;
            }
        }

        public double PointSize
        {
            get { return PZ; }
            set
            {
                PZ = value;
                point.Width = value;
                point.Height = value;
            }
        }

        public bool ButtonDown
        {
            set { down.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
            get { return down.Visibility == Visibility.Visible; }
        }

        public BackStyle UseBackStyle
        {
            get { return BS; }
            set
            {
                BS = value;
                switch (value)
                {
                    case BackStyle.Hidden:
                        Back.Visibility = Visibility.Collapsed;
                        BackR.Visibility = Visibility.Collapsed;
                        break;
                    case BackStyle.Rectangle:
                        Back.Visibility = Visibility.Visible;
                        BackR.Visibility = Visibility.Collapsed;
                        break;
                    case BackStyle.Ellipse:
                        Back.Visibility = Visibility.Collapsed;
                        BackR.Visibility = Visibility.Visible;
                        break;
                    default:
                        Back.Visibility = Visibility.Visible;
                        BackR.Visibility = Visibility.Collapsed;
                        BS = BackStyle.Rectangle;
                        break;
                }
            }
        }

        public Point ThumbPoint
        {
            get { return TP; }
            set
            {
                TP = value;
                Thumb.Margin = new Thickness(
                    (ActualWidth / 4) * (value.X / 100 + 1), 
                    (ActualHeight / 4) * (value.Y / 100 + 1), 
                    0, 
                    0
                );
            }
        }
    }
}
