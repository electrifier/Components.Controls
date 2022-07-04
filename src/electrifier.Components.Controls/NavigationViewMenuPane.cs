using electrifier.Components.Controls.Designers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;



namespace electrifier.Components.Controls
{
    /// <summary>
    /// NOTE: Perhaps we should use an <seealso href="https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.iextenderprovider?view=net-6.0">IExtenderProvider</seealso>
    /// for adding our functionality rather then creating a derived class?<br/><br/>
    /// <see href="https://github.com/dotnet/winforms/blob/main/src/System.Windows.Forms/src/System/Windows/Forms/ToolStrip.cs"/><br/>
    /// <see href="https://docs.microsoft.com/en-us/dotnet/desktop/winforms/controls/toolstrip-control-windows-forms?view=netframeworkdesktop-4.8"/>
    /// </summary>
    [Designer(typeof(NavigationViewMenuPaneDesigner))]
    public class NavigationViewMenuPane : ToolStrip
    {
        #region Design Time Options ===========================================================================================

        [Category("Navigation Options")]
        [DefaultValue(true)]
        public bool IsSettingsVisible
        {
            get => this.tbtSettings.Visible;
            set => this.tbtSettings.Visible = value;
        }

        [Description("The location is controlled by PaneDisplayMode property.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Point Location => base.Location;

        [Category("Navigation Options")]
        [DefaultValue(DisplayMode.Left)]
        public DisplayMode PaneDisplayMode
        {
            get => this.paneDisplayMode;
            set => this.SetPaneDisplayMode(value);
        }

        protected DisplayMode paneDisplayMode = DisplayMode.Left;

        [Category("Navigation Options")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool PaneDisplayModeIsCompact
        {
            get => ((this.PaneDisplayMode == DisplayMode.LeftCompact) || (this.PaneDisplayMode == DisplayMode.TopCompact));
            set
            {
                switch (this.PaneDisplayMode)
                {
                    case DisplayMode.Left:
                    case DisplayMode.LeftCompact:
                        this.PaneDisplayMode = (value ? DisplayMode.LeftCompact : DisplayMode.Left);
                        break;
                    case DisplayMode.Top:
                    case DisplayMode.TopCompact:
                        this.PaneDisplayMode = (value ? DisplayMode.TopCompact : DisplayMode.Top);
                        break;
                }
            }
        }

        [Description("The size is controlled by PaneDisplayMode property.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size Size => base.Size;

        #endregion ============================================================================================================

        protected ToolStripButton tbtOpenNavigation;
        protected ToolStripButton tbtSettings;
        protected NavigationViewMenuPaneRenderer menuPaneRenderer;

        public NavigationViewMenuPane()
        {
            InitializeComponent();

            this.ItemAdded += this.NavigationViewMenuPane_ItemAdded;
            this.Paint += this.NavigationViewMenuPane_Paint;
            this.TextChanged += this.NavigationViewMenuPane_TextChanged;

            this.menuPaneRenderer = new NavigationViewMenuPaneRenderer();       // TODO: Let the User decide which Renderer to use, via Designer. Set default to NavigationViewMenuPaneRenderer

            this.RebuildToolStripItems();
        }

        private void NavigationViewMenuPane_ItemAdded(object sender, ToolStripItemEventArgs args)
        {
            if (this.DesignMode)
            {
                Type itemType = args.Item.GetType();

                if (itemType != typeof(ToolStripButton) &&
                    itemType != typeof(ToolStripDropDownButton) &&
                    itemType != typeof(ToolStripLabel) &&
                    itemType != typeof(ToolStripSeparator))
                {
                    MessageBox.Show($"Please note that not all control types are fully supperted by NavigationViewMenuPane.\n\n" +
                        $"Supported types are:\n" +
                        $"\tToolStripButton\n" +
                        $"\tToolStripDropDownButton\n" +
                        $"\tToolStripLabel\n" +
                        $"\tToolStripSeparator\n\n" +
                        $"All other types can cause unexpected behaviour and rendering issues.",
                        caption: "NavigationViewMenuPane - Warning", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Warning);

                    this.ItemAdded -= this.NavigationViewMenuPane_ItemAdded;
                }
            }
        }

        private void NavigationViewMenuPane_Paint(object sender, PaintEventArgs args)
        {
            this.Renderer = this.menuPaneRenderer;
            this.Paint -= this.NavigationViewMenuPane_Paint;
        }

        private void NavigationViewMenuPane_TextChanged(object sender, EventArgs e)
        {
            this.tbtOpenNavigation.Text = this.Text;
        }

        private void TbtOpenNavigation_Click(object sender, EventArgs args)
        {
            this.PaneDisplayModeIsCompact = !this.PaneDisplayModeIsCompact;
        }

        protected internal void RebuildToolStripItems()
        {
            //this.Items.Clear();
            this.Items.Insert(0, tbtOpenNavigation); //this.Items.AddRange(this.MenuItems);
            this.Items.Add(this.tbtSettings); //this.Items.AddRange(this.FooterMenuItems);
        }

        protected void RebuildToolStripItemsCompactMode()
        {
            ToolStripItemDisplayStyle displayStyle = this.PaneDisplayModeIsCompact ? ToolStripItemDisplayStyle.Image : ToolStripItemDisplayStyle.ImageAndText;

            foreach (ToolStripItem item in this.Items)
            {
                item.DisplayStyle = displayStyle;
            }
        }

        protected void SetPaneDisplayMode(DisplayMode displayMode)
        {
            try
            {
                this.SuspendLayout();

                if (this.paneDisplayMode != displayMode)
                {
                    this.paneDisplayMode = displayMode;

                    switch (displayMode)
                    {
                        case DisplayMode.Left:
                        case DisplayMode.LeftCompact:
                            this.Dock = DockStyle.Left;
                            break;
                        case DisplayMode.Top:
                        case DisplayMode.TopCompact:
                            this.Dock = DockStyle.Top;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(displayMode));
                    }

                    this.RebuildToolStripItemsCompactMode();
                }
            }
            finally
            {
                this.ResumeLayout();
            }
        }








        #region NavigationViewMenuPane.Designer.cs ============================================================================

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NavigationViewMenuPane));
            this.tbtOpenNavigation = new System.Windows.Forms.ToolStripButton();
            this.tbtSettings = new System.Windows.Forms.ToolStripButton();
            this.SuspendLayout();
            // 
            // NavigationViewMenuPane
            // 
            this.AutoSize = true;
            this.CanOverflow = false;
            this.Dock = System.Windows.Forms.DockStyle.Left;
            this.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.StackWithOverflow;
            this.Name = "NavigationMenu";
            this.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // tbtOpenNavigation
            // 
            this.tbtOpenNavigation.Click += this.TbtOpenNavigation_Click;
            this.tbtOpenNavigation.Image = ((System.Drawing.Image)(resources.GetObject("tbtOpenNavigation.Image")));
            this.tbtOpenNavigation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tbtOpenNavigation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtOpenNavigation.Name = "tbtOpenNavigation";
            // 
            // tbtSettings
            // 
            this.tbtSettings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tbtSettings.Image = ((System.Drawing.Image)(resources.GetObject("tbtSettings.Image")));
            this.tbtSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tbtSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtSettings.Name = "tbtSettings";
            this.tbtSettings.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        #endregion ============================================================================================================

        /// <summary>
        /// <see cref="NavigationViewMenuPane"/>'s display mode.<br/>
        /// <br/>
        /// Can be one of the following values:<br/>
        /// 
        /// <list type="bullet">
        ///     <item>Left</item>
        ///     <item>LeftCompact</item>
        ///     <item>Top</item>
        ///     <item>TopCompact</item>
        /// </list>
        /// </summary>
        public enum DisplayMode
        {
            Left,
            LeftCompact,
            Top,
            TopCompact,
        }
    }
}
