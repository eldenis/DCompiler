using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace WPF.Cliente.Util
{

    /// <summary>
    /// Attach this to a button or other controls in a DataGrid row to toggle 
    /// the visibility of the Row Detail.  The 
    /// DataGrid.RowDetailsVisibilityMode should have an initial state of 
    /// Collapsed.
    /// </summary>
    /// <remarks>Solution adapted from this article: 
    /// http://stackoverflow.com/questions/1471534/wpf-datagrid-rowdetailstemplate-visibility-bound-to-a-property</remarks>
    public class ToggleRowDetailVisibility : TriggerAction<DependencyObject>
    {
        /// <summary>
        /// Toggle the Row Detail Visibility of the row that this control is in
        /// </summary>
        /// <param name="o">the Routed event argument</param>
        protected override void Invoke(object o)
        {
            try
            {
                // the original source is what was clicked.  For example  
                // a button. 
                DependencyObject dep = this.AssociatedObject;

                // iteratively traverse the visual tree upwards looking for 
                // the clicked row. 
                while ((dep != null) && !(dep is DataGridRow))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }

                var row = dep as DataGridRow;

                // if we found the clicked row 
                if (row != null)
                {
                    // change the details visibility 
                    if (row.DetailsVisibility == Visibility.Collapsed)
                    {
                        row.DetailsVisibility = Visibility.Visible;
                    }
                    else
                    {
                        row.DetailsVisibility = Visibility.Collapsed;
                    }
                }
            }
            catch (System.Exception)
            {
            }
        }
    }
}
