using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace Sharpsaver.Views
{
    public partial class ScreensaverView : Window
    {
        private bool isPreviewWindow;
        private Point lastMousePosition = default;

        public ScreensaverView()
        {
            InitializeComponent();
            isPreviewWindow = false;
        }
        public ScreensaverView(IntPtr previewHandle)
        {            
            InitializeComponent();
            isPreviewWindow = true;
            Rect parentRect = new Rect();

#if NET30 || NET35
            bool bGetRect = InteropHelper.GetClientRect(previewHandle, ref parentRect);

            HwndSourceParameters sourceParams = new HwndSourceParameters("sourceParams");
            sourceParams.PositionX = 0;
            sourceParams.PositionY = 0;
            sourceParams.Height = parentRect.Height;
            sourceParams.Width = parentRect.Width;
            this.Field.Height = sourceParams.Height;
            this.Field.Width = sourceParams.Width;
            sourceParams.ParentWindow = previewHandle;
            //WS_VISIBLE = 0x10000000; WS_CHILD = 0x40000000; WS_CLIPCHILDREN = 0x02000000;
            sourceParams.WindowStyle = (int)(0x10000000L | 0x40000000L | 0x02000000L);

            //Using HwndSource instead of this.Show() to properly obtain handle of this window
            HwndSource winWPFContent = new HwndSource(sourceParams);
            winWPFContent.Disposed += new EventHandler(this.Dispose);
            winWPFContent.ContentRendered += new EventHandler(this.Window_Loaded);
            winWPFContent.RootVisual = this.Viewbox;
#else
            WindowState = WindowState.Normal;

            IntPtr windowHandle = new WindowInteropHelper(GetWindow(this)).EnsureHandle();

            // Set the preview window as the parent of this window
            InteropHelper.SetParent(windowHandle, previewHandle);

            // Make this window a tool window while preview.
            // A tool window does not appear in the taskbar or in the dialog that appears when the user presses ALT+TAB.
            // GWL_EXSTYLE = -20, WS_EX_TOOLWINDOW = 0x00000080L
            InteropHelper.SetWindowLong(windowHandle, -20, 0x00000080L);
            // Make this a child window so it will close when the parent dialog closes
            // GWL_STYLE = -16, WS_CHILD = 0x40000000
            InteropHelper.SetWindowLong(windowHandle, -16, 0x40000000L);

            // Place the window inside the parent
            InteropHelper.GetClientRect(previewHandle, ref parentRect);

            Width = parentRect.Width;
            Height = parentRect.Height;
#endif
        }

        public void Window_Loaded(object sender, EventArgs e)
        {

        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPreviewWindow) return;

            Point pos = e.GetPosition(this);

            if (lastMousePosition != default)
            {
                if ((lastMousePosition - pos).Length > 3)
                {
                    Application.Current.Shutdown();
                }
            }
            lastMousePosition = pos;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isPreviewWindow) return;

            Application.Current.Shutdown();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (isPreviewWindow) return;

            Application.Current.Shutdown();
        }
        internal void Dispose(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
