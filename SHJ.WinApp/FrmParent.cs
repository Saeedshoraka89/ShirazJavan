using System.ComponentModel;
using System.Runtime.InteropServices;

namespace SHJ.WinApp;

public partial class FrmParent : Form
{
    private Point _mouseLoc;

    #region Round
    private int _radius = 75;

    [DefaultValue(75)]
    private int Radius
    {
        get => _radius;
        set
        {
            _radius = value;
            RecreateRegion();
        }
    }

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect,
        int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

    private void RecreateRegion()
    {
        var bounds = ClientRectangle;
        Region = Region.FromHrgn(CreateRoundRectRgn(bounds.Left, bounds.Top, bounds.Right, bounds.Bottom, Radius,
            _radius));
        Invalidate();
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        RecreateRegion();
    }

    #endregion

    public FrmParent()
    {
        InitializeComponent();
    }

    private void FrmParent_Load(object sender, EventArgs e)
    {

    }

    private void panel1_MouseDown(object sender, MouseEventArgs e) => _mouseLoc = e.Location;

    private void panel1_MouseMove(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
            Location = new Point(Location.X + (e.Location.X - _mouseLoc.X), Location.Y + (e.Location.Y - _mouseLoc.Y));
    }

    private void CloseButton_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void BtnMax_Click(object sender, EventArgs e)
    {
        WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
    }

    private void BtnMin_Click(object sender, EventArgs e)
    {
        WindowState = FormWindowState.Minimized;
    }
}