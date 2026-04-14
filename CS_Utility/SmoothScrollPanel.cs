using System;
using System.Drawing;
using System.Windows.Forms;

public class SmoothScrollPanel : Panel
{
    protected override void OnMouseWheel(MouseEventArgs e)
    {
        int scrollAmount = e.Delta / 6; // adjust speed here

        this.AutoScrollPosition = new Point(
            -this.AutoScrollPosition.X,
            -(this.AutoScrollPosition.Y + scrollAmount)
        );
    }
}