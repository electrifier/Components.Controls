using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace electrifier.Components.Controls
{
    public class NavigationViewMenuPane : ToolStrip
    {
        private ToolStripButton tbtOpenNavigation;
        private ToolStripButton tbtSettings;

        public NavigationViewMenuPane()
        {
            InitializeComponent();
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
            this.CanOverflow = false;
            this.Dock = System.Windows.Forms.DockStyle.Left;
            this.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtOpenNavigation,
            this.tbtSettings});
            this.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
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
            this.tbtOpenNavigation.Text = "toolStripButton1";
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
