using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UtilityWpf.Behavior
{
    public class BindableSelectedItemBehavior : Behavior<TreeView>
    {
        #region SelectedItem Property

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(
              nameof(SelectedItem),
              typeof(object),
              typeof(BindableSelectedItemBehavior),
              new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnSelectedItemChanged));

        static void OnSelectedItemChanged(DependencyObject sender,
          DependencyPropertyChangedEventArgs e)
        {
            var behavior = (BindableSelectedItemBehavior)sender;
            var generator = behavior.AssociatedObject.ItemContainerGenerator;
            if (generator.ContainerFromItem(e.NewValue) is TreeViewItem item)
                item.SetValue(TreeViewItem.IsSelectedProperty, true);
        }
        #endregion

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.SelectedItemChanged += OnTreeViewSelectedItemChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (this.AssociatedObject != null)
                AssociatedObject.SelectedItemChanged -= OnTreeViewSelectedItemChanged;
        }

        void OnTreeViewSelectedItemChanged(object sender,
            RoutedPropertyChangedEventArgs<object> e) =>
          SelectedItem = e.NewValue;
    }
}
