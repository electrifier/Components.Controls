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
    /// <see href="https://github.com/dotnet/winforms/blob/main/src/System.Windows.Forms/src/System/Windows/Forms/ToolStrip.cs"/><br/>
    /// <see href="https://docs.microsoft.com/en-us/dotnet/desktop/winforms/controls/toolstrip-control-windows-forms?view=netframeworkdesktop-4.8"/>
    /// </summary>
    [Designer(typeof(NavigationViewMenuPaneDesigner))]
    public class NavigationViewMenuPane : ToolStrip
    {
        protected ToolStripButton tbtOpenNavigation;
        protected ToolStripButton tbtSettings;

        [Category("Navigation Options")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public NavigationViewMenuPaneItemCollection FooterMenuItems
        {
            get => this.footerMenuItems;
            protected set { this.footerMenuItems = value; this.RebuildToolStripItems(); }
        }

        protected NavigationViewMenuPaneItemCollection footerMenuItems;

        [Category("Navigation Options")]
        [DefaultValue(true)]
        public bool IsSettingsVisible
        {
            get => this.tbtSettings.Visible;
            set => this.tbtSettings.Visible = value;
        }

        [Category("Navigation Options")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public NavigationViewMenuPaneItemCollection MenuItems
        {
            get => this.menuItems;
            protected set { this.menuItems = value; this.RebuildToolStripItems(); }
        }

        protected NavigationViewMenuPaneItemCollection menuItems;

        [Category("Navigation Options")]
        [DefaultValue(DisplayMode.Left)]
        public DisplayMode PaneDisplayMode
        {
            get => this.paneDisplayMode;
            set => this.SetPaneDisplayMode(value);
        }

        protected DisplayMode paneDisplayMode = DisplayMode.Left;

        [Category("Navigation Options")]
        public bool PaneDisplayModeIsCompact { get { return ((this.paneDisplayMode == DisplayMode.LeftCompact) || (this.paneDisplayMode == DisplayMode.TopCompact)); } }

        /// <summary>
        /// <see cref="NavigationViewMenuPane"/>'s display mode.<br/>
        /// <br/>
        /// Can be one of the following values:<br/>
        /// 
        /// <list type="bullet">
        ///     <item>Left</item>
        ///     <item>Top</item>
        ///     <item>LeftCompact</item>
        ///     <item>TopCompact</item>
        /// </list>
        /// </summary>
        public enum DisplayMode
        {
            Left,
            Top,
            LeftCompact,
            TopCompact,
        }

        protected NavigationViewMenuPaneRenderer menuPaneRenderer;

        protected bool DisplayModeIsCompact => this.PaneDisplayMode == DisplayMode.LeftCompact || this.PaneDisplayMode == DisplayMode.TopCompact;

        public NavigationViewMenuPane()
        {
            InitializeComponent();

            this.menuPaneRenderer = new NavigationViewMenuPaneRenderer();       // TODO: Let the User decide which Renderer to use, via Designer. Set default to NavigationViewMenuPaneRenderer

            this.Paint += this.NavigationViewMenuPane_Paint;

            this.menuItems = new NavigationViewMenuPaneItemCollection(this, new ToolStripItem[] { this.tbtOpenNavigation });
            this.footerMenuItems = new NavigationViewMenuPaneItemCollection(this, new ToolStripItem[] { this.tbtSettings }, true);

            this.RebuildToolStripItems();
        }

        private void NavigationViewMenuPane_Paint(object sender, PaintEventArgs e)
        {
            this.Renderer = this.menuPaneRenderer;
            this.Paint -= this.NavigationViewMenuPane_Paint;
        }

        protected internal void RebuildToolStripItems()
        {
            this.Items.Clear();

            this.Items.AddRange(this.MenuItems);
            this.Items.AddRange(this.FooterMenuItems);
        }

        protected void RebuildToolStripItemsCompactMode()
        {
            var displayStyle = this.DisplayModeIsCompact ? ToolStripItemDisplayStyle.Image : ToolStripItemDisplayStyle.ImageAndText;

            foreach (ToolStripItem item in this.Items)
            {
                item.DisplayStyle = displayStyle;
            }
        }

        protected void SetPaneDisplayMode(DisplayMode displayMode)
        {
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
            //this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            //this.tbtOpenNavigation,
            //this.tbtSettings});
            this.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.StackWithOverflow;
            this.Name = "NavigationMenu";
            this.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.Size = new System.Drawing.Size(250, 450);
            // 
            // tbtOpenNavigation
            // 
            this.tbtOpenNavigation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtOpenNavigation.Image = ((System.Drawing.Image)(resources.GetObject("tbtOpenNavigation.Image")));
            this.tbtOpenNavigation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tbtOpenNavigation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtOpenNavigation.Name = "tbtOpenNavigation";
            this.tbtOpenNavigation.Size = new System.Drawing.Size(248, 28);
            // 
            // tbtSettings
            // 
            this.tbtSettings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tbtSettings.Image = ((System.Drawing.Image)(resources.GetObject("tbtSettings.Image")));
            this.tbtSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tbtSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtSettings.Name = "tbtSettings";
            this.tbtSettings.Size = new System.Drawing.Size(248, 28);
            this.tbtSettings.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        #endregion ============================================================================================================

    }
}
