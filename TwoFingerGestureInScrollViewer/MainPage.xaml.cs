using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.Devices.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace TwoFingerGestureInScrollViewer
{
    public sealed partial class MainPage : Page
    {
        readonly HashSet<uint> _pointers = new HashSet<uint>();

        public MainPage()
        {
            InitializeComponent();
        }

        private void Image_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Debug.WriteLine("pointer pressed");

            // Prevent DM fro taking control.
            Image.ManipulationMode &= ~ManipulationModes.System;

            if (e.Pointer.PointerDeviceType == PointerDeviceType.Touch)
            {
                _pointers.Add(e.Pointer.PointerId);
            }
        }

        private void ScrollViewer_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            // Detect when three fingers are pressed. Note you will want additional
            // logic here to decide whether a swipe has happened. During this time,
            // you don't want to re-enable DM (i.e. else if).
            if (_pointers.Count == 3)
            {
                // Only do this once...
                _pointers.Clear();

                Image.Source = new BitmapImage(new Uri("ms-appx:///Assets/wenni-zhou-780285-unsplash.jpg"));

                Debug.WriteLine("image source updated");
            }
            else if (e.Pointer.PointerDeviceType == PointerDeviceType.Touch)
            {
                // Let DM take control again. 
                Image.ManipulationMode |= ManipulationModes.System;

                // Re-enable DM.
                var enabled = TryStartDirectManipulation(e.Pointer);

                //Debug.WriteLine($"DM re-enabled?:{enabled}");
            }
        }

        private void ScrollViewer_DirectManipulationCompleted(object sender, object e)
        {
            Debug.WriteLine("DM completed");

            _pointers.Clear();
        }








        private void Image_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            // Don't use. This won't be called.
        }

        private void Image_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            //Debug.WriteLine("pointer exited");
        }

        private void Image_PointerCanceled(object sender, PointerRoutedEventArgs e)
        {
            //Debug.WriteLine("pointer canceled");
        }

        private void Image_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            //Debug.WriteLine("pointer released");
        }

        private void Image_PointerCaptureLost(object sender, PointerRoutedEventArgs e)
        {
            //Debug.WriteLine("pointer capture lost");
        }

        private void ScrollViewer_DirectManipulationStarted(object sender, object e)
        {
            Debug.WriteLine("DM started");

            //if (_isThreeFingerSwipe)
            //{
            //    Debug.WriteLine("cancel DM");
            //    ScrollViewer.CancelDirectManipulations();
            //}
        }
    }
}
