using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace electrifier.Components.Controls
{
    /// <summary>
    /// Inherited from <see cref="ToolStripItemCollection"/>, which itself inherits from
    /// <see cref="ArrangedElementCollection"/>,
    /// <see cref="IList"/>.
    /// </summary>
    [ListBindable(false)]
    //[Designer(typeof(NavigationViewMenuPaneItemCollectionDesigner))]  // => http://www.dotnetframework.org/default.aspx/DotNET/DotNET/8@0/untmp/whidbey/REDBITS/ndp/fx/src/Designer/WinForms/System/WinForms/Design/ToolStripCollectionEditor@cs/1/ToolStripCollectionEditor@cs
    public class NavigationViewMenuPaneItemCollection : ToolStripItemCollection
    {
        public NavigationViewMenuPane Owner { get; }

        public bool IsFooter { get; }

        public NavigationViewMenuPaneItemCollection(NavigationViewMenuPane navigationViewMenuPane, ToolStripItem[] toolStripItems, bool isFooter = false)
            : base(navigationViewMenuPane, toolStripItems)
        {
            this.Owner = navigationViewMenuPane ?? throw new ArgumentNullException(nameof(navigationViewMenuPane));
            this.IsFooter = isFooter;


        }

        private void UpdateItemsIsFooterProperty()
        {
            var alignment = this.IsFooter ? ToolStripItemAlignment.Right : ToolStripItemAlignment.Left;

            foreach (var item in this)
            {
                if (typeof(ToolStripButton) == item.GetType())
                {
                    ToolStripButton button = (ToolStripButton)item;
                    button.Alignment = alignment;
                }
            }

            this.Owner.RebuildToolStripItems();
        }

        public new void AddRange(System.Windows.Forms.ToolStripItem[] toolStripItems)
        {
            // TODO: Change signature to NavigationViewMenuItem, set Is Footer before adding
            // TODO: BeginUpdate on ToolStrip
            base.AddRange(toolStripItems);

            this.UpdateItemsIsFooterProperty();
        }

        public new int Add(System.Windows.Forms.ToolStripItem toolStripItem)
        {
            // TODO: Change signature to NavigationViewMenuItem, set Is Footer before adding
            // TODO: BeginUpdate on ToolStrip
            var result = base.Add(toolStripItem);

            this.UpdateItemsIsFooterProperty();

            return result;
        }
    }
}
