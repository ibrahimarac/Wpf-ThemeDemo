using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Ibrahim.Wpf.CustomControls.Controls
{
    public class CustomBaseForm : Window
    {
        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(CustomBaseForm), new PropertyMetadata(null));


        public CustomBaseForm()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.AllowsTransparency = true;
            this.WindowStyle = WindowStyle.None;

            //Formu sol tıklayarak taşıyabilmek için
            this.MouseLeftButtonDown += SchedulerBaseForm_MouseLeftButtonDown;

            //Kapat butonuna tıklandığında Close Command'ı tetiklenecek
            this.CommandBindings.Add(new System.Windows.Input.CommandBinding(ApplicationCommands.Close, FormClosed));
        }

        public virtual void FormClosed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void SchedulerBaseForm_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
