using System;
using System.Security.Permissions;
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

namespace electrifier.Components.Controls.Designers
{
    /// <summary>
    /// The <see cref="ControlDesigner"/> of <see cref="NavigationViewMenuPane"/>.<br/>
    /// 
    /// <see href="https://github.com/dotnet/winforms/blob/main/src/System.Windows.Forms.Design/src/System/Windows/Forms/Design/ToolStripDesigner.cs"/><br/>
    /// <br/>
    /// Useful links:
    /// <list type="bullet">
    ///     <item>
    ///         <seealso href="https://docs.microsoft.com/en-us/dotnet/desktop/winforms/controls/serializing-collections-designerserializationvisibilityattribute?view=netframeworkdesktop-4.8">
    ///         Walkthrough: Serialize collections of standard types</seealso>
    ///     </item>
    ///     <item>
    ///         <seealso href="https://stackoverflow.com/questions/4075802/creating-a-dpi-aware-application">Creating a DPI aware application</seealso>
    ///     </item>
    /// </list>
    /// </summary>
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public class NavigationViewMenuPaneDesigner : ControlDesigner
    {
        // This is the collection of DesignerActionLists that 
        // defines the smart tags offered on the control.  
        private DesignerActionListCollection actionLists = null;

        // This method creates the DesignerActionList on demand, causing 
        // smart tags to appear on the control being designed. 
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (this.actionLists == null)
                {
                    this.actionLists = new DesignerActionListCollection
                    {
                        new AnchorActionList(this.Component)
                    };
                }

                return actionLists;
            }
        }

        

        /// <summary>
        /// The list of removed Properties from the underlying <see cref="ToolStrip"/> control.
        /// <list type="table">
        ///     <listheader>
        ///         <term>Property</term>
        ///         <description>Description</description>
        ///     </listheader>
        ///     <item>
        ///         <term>AutoSize</term>
        ///         <description>Forced to use <i>AutoSize</i>, to make <i>PaneDisplayMode</i>-Property work properly. Use MinWidth for setting minimum widths and heights.</description>
        ///     </item>
        ///     <item>
        ///         <term>Dock</term>
        ///         <description>Use <i>PaneDisplayMode</i> instead.</description>
        ///     </item>
        ///     <item>
        ///         <term>LayoutStyle</term>
        ///         <description>Forced to use <i>StackWithOverflow</i>.</description>
        ///     </item>
        /// </list>
        /// </summary>
        public static readonly string[] removedProperties = {
            "Anchor",
            "AutoSize",
            "Dock",
            "GripStyle",
            "GripMargin",
            "LayoutStyle",
            "MaximumSize",
            "MinimumSize",
            "Padding",
            "Stretch",
        };

        protected override void PreFilterProperties(IDictionary properties)
        {
            base.PreFilterProperties(properties);

            foreach (var property in removedProperties)
            {
                if (properties.Contains(property))
                    properties.Remove(property);
            }


            //properties["Visible"] = TypeDescriptor.CreateProperty(
            //    typeof(MarqueeBorderDesigner),
            //    (PropertyDescriptor)properties["Visible"],
            //    new Attribute[0]);

            //properties["Enabled"] = TypeDescriptor.CreateProperty(
            //    typeof(MarqueeBorderDesigner),
            //    (PropertyDescriptor)properties["Enabled"],
            //    new Attribute[0]);
        }

        // This class defines the smart tags that appear on the control 
        // being designed. In this case, the Anchor property appears 
        // on the smart tag and its value can be changed through a  
        // UI Type Editor created automatically by the  
        // DesignerActionService. 
        public class AnchorActionList : DesignerActionList
        {
            private readonly NavigationViewMenuPane menuPane;

            /// <summary>
            /// Fetch the <see cref="NavigationViewMenuPane"/> associated with this smart tag list.
            /// </summary>
            /// <param name="component">The <see cref="NavigationViewMenuPane"/> this designer is associated with.</param>
            /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="component"/> is null or not of type <see cref="NavigationViewMenuPane"/></exception>
            public AnchorActionList(IComponent component) : base(component)
            {
                this.menuPane = component as NavigationViewMenuPane ?? throw new ArgumentOutOfRangeException("Base control has to be an instance of NavigationViewMenuPane");
            }

            /// <summary>
            /// Get an instance of the available <see cref="DesignerActionItemCollection"/>.
            /// </summary>
            /// <returns>The <see cref="DesignerActionItemCollection"/>.</returns>
            public override DesignerActionItemCollection GetSortedActionItems()
            {
                const string designCategory = "Design";
                const string controlsCategory = "Controls";

                return new DesignerActionItemCollection
                {
                    new DesignerActionHeaderItem(designCategory),
                    new DesignerActionTextItem("Available Design Options:", designCategory),
                    new DesignerActionPropertyItem("PaneDisplayMode", displayName: "Pane Display Mode", category: designCategory),

                    new DesignerActionHeaderItem(controlsCategory),
                    new DesignerActionTextItem("Available Controls:", controlsCategory),
                    new DesignerActionMethodItem(this, memberName: nameof(DesignerActionAddHeaderItem), displayName: "Add Header Item", category: controlsCategory),
                    new DesignerActionMethodItem(this, memberName: nameof(DesignerActionAddItem), displayName: "Add Item", category: controlsCategory),
                    new DesignerActionMethodItem(this, memberName: nameof(DesignerActionAddSeperator), displayName: "Add Seperator", category: controlsCategory),

                    new DesignerActionMethodItem(this, memberName: nameof(DesignerActionAddDropDownButton), displayName: "Add DropDown", category: controlsCategory), // TODO: Test

                };
            }

            /// <summary>
            /// Target of <see cref="DesignerActionPropertyItem"/>.<br/>
            /// <br/>
            /// Used by <see cref="GetSortedActionItems"/>.
            /// </summary>
            public NavigationViewMenuPane.DisplayMode PaneDisplayMode
            {
                get => this.menuPane.PaneDisplayMode;
                set => TypeDescriptor.GetProperties(this.menuPane)["PaneDisplayMode"].SetValue(this.menuPane, value);
            }

            public void DesignerActionAddHeaderItem() => this.menuPane.Items.Add(new ToolStripLabel("New Header"));                         // TODO: Rebuild / Align PaneDisplayMode

            public void DesignerActionAddItem() => this.menuPane.Items.Add(new ToolStripButton("New Button"));                              // TODO: Rebuild / Align PaneDisplayMode

            public void DesignerActionAddSeperator() => this.menuPane.Items.Add(new ToolStripSeparator());                                  // TODO: Rebuild / Align PaneDisplayMode

            public void DesignerActionAddDropDownButton() => this.menuPane.Items.Add(new ToolStripDropDownButton("New DropDownButton"));    // TODO: Rebuild / Align PaneDisplayMode

        }
    }
}
