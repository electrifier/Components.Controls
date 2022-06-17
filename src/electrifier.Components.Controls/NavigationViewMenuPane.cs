// dev-branch

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
    public enum PaneDisplayMode
    {
        Left,
        Top,
        LeftCompact,
    }

    /// <summary>
    /// <see href="https://github.com/dotnet/winforms/blob/main/src/System.Windows.Forms/src/System/Windows/Forms/ToolStrip.cs"/>
    /// </summary>
    [Designer(typeof(NavigationViewMenuPaneDesigner))]
    public class NavigationViewMenuPane : ToolStrip
    {
        private PaneDisplayMode paneDisplayMode = PaneDisplayMode.Left;

        private ToolStripButton tbtOpenNavigation;
        private ToolStripButton tbtSettings;

        public bool IsInCompactMode { get { return (this.paneDisplayMode == PaneDisplayMode.LeftCompact); } }


        //[Browsable(true)]
        [DefaultValue(true)]
        //[Category("Navigation Options")]
        public bool IsSettingsVisible
        {
            get => this.tbtSettings.Visible;
            set => this.tbtSettings.Visible = value;
        }

        [DefaultValue(PaneDisplayMode.Left)]
        public PaneDisplayMode PaneDisplayMode
        {
            get => this.paneDisplayMode;
            set => this.SetPaneDisplayMode(value);
        }

        public NavigationViewMenuPane()
        {
            InitializeComponent();

            this.menuItems = new NavigationViewMenuPaneItemCollection(this, new ToolStripItem[] { this.tbtOpenNavigation });
            this.footerMenuItems = new NavigationViewMenuPaneItemCollection(this, new ToolStripItem[] { this.tbtSettings });


            this.RebuildToolStripItems();

            //this.testToolStripItems.Add(new ToolStripButton("test"));
        }

        protected void RebuildToolStripItems()
        {
            this.Items.Clear();

            this.Items.AddRange(this.MenuItems);
            this.Items.AddRange(this.FooterMenuItems);
        }

        protected void SetPaneDisplayMode(PaneDisplayMode displayMode)
        {
            if (this.paneDisplayMode != displayMode)
            {
                this.paneDisplayMode = displayMode;

                switch (displayMode)
                {
                    case PaneDisplayMode.Left:
                    case PaneDisplayMode.LeftCompact:
                        this.Dock = DockStyle.Left;
                        break;
                    case PaneDisplayMode.Top:
                        this.Dock = DockStyle.Top;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(displayMode));
                }

                this.SetCompactMode(displayMode == PaneDisplayMode.LeftCompact);
            }
        }

        protected void SetCompactMode(bool compactMode)
        {
            var displayStyle = compactMode ? ToolStripItemDisplayStyle.Image : ToolStripItemDisplayStyle.ImageAndText;

            foreach (ToolStripItem item in this.Items)
            {
                item.DisplayStyle = displayStyle;
            }
        }

        ////[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //[Browsable(true)]
        //[Category("Data")]
        ////[EditorAttribute(typeof(CollectionEditor))]
        //public ToolStripItemCollection TopItems;


        /// <summary>
        /// This works with simple designer
        /// </summary>

        //private List<ToolStripButton> testToolStripItems = new List<ToolStripButton>();
        ////[EditorBrowsable(EditorBrowsableState.Never)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //public List<ToolStripButton> TestToolStripItems
        //{
        //    get { return testToolStripItems; }
        //    set { testToolStripItems = value; }
        //}

        /// end this works
        /// 

        protected NavigationViewMenuPaneItemCollection menuItems;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public NavigationViewMenuPaneItemCollection MenuItems
        {
            get => this.menuItems;
            set => this.menuItems = value;
        }

        protected NavigationViewMenuPaneItemCollection footerMenuItems;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public NavigationViewMenuPaneItemCollection FooterMenuItems
        {
            get => this.footerMenuItems;
            set => this.footerMenuItems = value;
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
