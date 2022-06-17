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
    /// Inherited from <see cref="ToolStripItemCollection"/>, which itself inherits
    /// <see cref="ArrangedElementCollection"/>,
    /// <see cref="IList"/>.
    /// </summary>
    [ListBindable(false)]
    public class NavigationViewMenuPaneItemCollection : ToolStripItemCollection
    {
        public NavigationViewMenuPaneItemCollection(ToolStrip toolStrip, ToolStripItem[] toolStripItems)
            : base(toolStrip, toolStripItems)
        {

        }
    }
}
