using Plasmoid.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace electrifier.Components.Controls
{
    /// <summary>
    /// <seealso href="https://www.codeproject.com/articles/33411/vtextender-toolstrip-extender-renderer-and-customi"></seealso>
    /// <seealso cref="ControlPaint"/>
    /// </summary>
    public class NavigationViewMenuPaneRenderer : ToolStripRenderer
    {
        private Color buttonSelectedBackground = Color.FromArgb(234, 234, 234);
        private Color buttonSelectedBorder = Color.FromArgb(205, 205, 205);    //ControlPaint.Dark(this.buttonSelectedBackground);

        private Color buttonPressedBackground = Color.FromArgb(237, 237, 237);
        private Color buttonPressedBorder = Color.FromArgb(205, 205, 205);    //ControlPaint.Dark(this.buttonSelectedBackground);


        private Color buttonCheckedBackground = Color.FromArgb(117, 133, 200);
        private Color buttonCheckedIndicator = Color.FromArgb(74, 86, 155);
        private Color buttonCheckedBorder = Color.FromArgb(59, 66, 100);

        public Size ButtonCheckedIndicatorSize = new Size(3, 16);

        public int CornerRadius { get; set; } = 5;

        ///// <summary>
        ///// The <see cref="NavigationViewMenuPane"/> that is the owner if this Instance.
        ///// </summary>
        //public NavigationViewMenuPane Owner { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigationViewMenuPane"></param>
        //public NavigationViewMenuPaneRenderer(NavigationViewMenuPane navigationViewMenuPane)
        //{
        //    this.Owner = navigationViewMenuPane;
        //}
        public NavigationViewMenuPaneRenderer()
        {
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs args)
        {
            base.OnRenderMenuItemBackground(args);

            if (args.Item.Selected)
            {
                if (args.Item.Pressed)
                    DrawBackground(this.buttonPressedBackground, this.buttonPressedBorder);
                else
                    DrawBackground(this.buttonSelectedBackground, this.buttonSelectedBorder);
            }

            void DrawBackground(Color backgroundColor, Color borderColor)
            {
                using (Brush brush = new SolidBrush(backgroundColor))
                    args.Graphics.FillRoundedRectangle(brush, args.Item.ContentRectangle, this.CornerRadius);

                using (Pen pen = new Pen(borderColor, 1))
                    args.Graphics.DrawRoundedRectangle(pen, args.Item.ContentRectangle, this.CornerRadius);
            }
        }

        //protected override void InitializeItem(ToolStripItem item)
        //{
        //    base.InitializeItem(item);

        //    item.AutoSize = false;
        //    item.Height = 36;     // The default Winui3-Size is 40x36 Pixel. The "Checked"-Bar on the left side measueres 3px.
        //}

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs args)
        {
            base.OnRenderArrow(args);

            this.DrawDropDownArrow(args.Graphics, args.ArrowRectangle, args.ArrowColor);
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs args)
        {
            base.OnRenderButtonBackground(args);

            if (args.Item.Selected)
            {
                if (args.Item.Pressed)
                    DrawBackground(this.buttonPressedBackground, this.buttonPressedBorder);
                else
                    DrawBackground(this.buttonSelectedBackground, this.buttonSelectedBorder);
            }

            if ((args.Item is ToolStripButton button) && button.Checked)
                this.DrawButtonCheckedIndicator(args.Graphics, args.Item.ContentRectangle, this.buttonCheckedIndicator);

            return;

            // TODO: Move into own helper function
            void DrawBackground(Color backgroundColor, Color borderColor)
            {
                using (Brush brush = new SolidBrush(backgroundColor))
                    args.Graphics.FillRoundedRectangle(brush, args.Item.ContentRectangle, this.CornerRadius);

                using (Pen pen = new Pen(borderColor, 1))
                    args.Graphics.DrawRoundedRectangle(pen, args.Item.ContentRectangle, this.CornerRadius);
            }
        }

        protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs args)
        {
            base.OnRenderDropDownButtonBackground(args);

            var contentArea = args.Item.ContentRectangle;

            if (args.Item is ToolStripDropDownButton dropdown)
            {
                if (dropdown.ShowDropDownArrow)
                    contentArea.Width += 10;       // TODO: HardCoded value for DropDownArrow-Width
            }
            else
                throw new ArgumentOutOfRangeException(nameof(args.Item));

            if (args.Item.Selected)
            {
                // TODO: Move into own helper function, see OnRenderButtonBackground
                using (Brush brush = new SolidBrush(this.buttonSelectedBackground))
                    args.Graphics.FillRoundedRectangle(brush, contentArea, this.CornerRadius);

                using (Pen pen = new Pen(this.buttonSelectedBorder))
                    args.Graphics.DrawRoundedRectangle(pen, contentArea, this.CornerRadius);
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs args)
        {
            base.OnRenderSeparator(args);

            using (Brush brush = new SolidBrush(this.buttonSelectedBackground))
                args.Graphics.FillRectangle(brush, args.Item.ContentRectangle);
        }

        #region Drawing Helper functions ======================================================================================

        protected virtual void DrawButtonCheckedIndicator(Graphics graphics, Rectangle bounds, Color color)
        {
            Rectangle indicatorBounds = new Rectangle(
                bounds.Left + 2,
                bounds.Top + ((bounds.Height - this.ButtonCheckedIndicatorSize.Height) / 2),
                this.ButtonCheckedIndicatorSize.Width,
                this.ButtonCheckedIndicatorSize.Height + 1);

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddRectangle(indicatorBounds);
                path.CloseFigure();

                using (Brush brush = new SolidBrush((Color)color))
                    graphics.FillPath(brush, path);
            }
        }

        protected virtual void DrawDropDownArrow(Graphics graphics, Rectangle bounds, Color color)
        {
            int top = ((bounds.Height - 8) / 2) + 2;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddLine(new Point(bounds.X, top), new Point(bounds.X + 4, top));
                path.AddLine(new Point(bounds.X, top), new Point(bounds.X + 2, top + 2));
                path.AddLine(new Point(bounds.X + 2, top + 2), new Point(bounds.X + 4, top));
                path.CloseFigure();

                using (Pen pen = new Pen(color, 0.5f))
                    graphics.DrawPath(pen, path);

                using (Brush brush = new SolidBrush(color))
                    graphics.FillPath(brush, path);
            }
        }

        #endregion ============================================================================================================

    }
}
