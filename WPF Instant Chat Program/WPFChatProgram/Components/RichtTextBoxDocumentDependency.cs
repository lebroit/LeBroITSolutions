using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WPFChatProgram.Components
{
    public class RichTextBoxWithDocumentDependency : RichTextBox
    {
        public static readonly DependencyProperty DocumentProperty = 
            DependencyProperty.Register("Document", typeof(FlowDocument),
            typeof(RichTextBoxWithDocumentDependency), new FrameworkPropertyMetadata(
                null, new PropertyChangedCallback(OnDocumentChanged)));

        public new FlowDocument Document
        {
            get
            {
                return (FlowDocument)GetValue(DocumentProperty);
            }

            set
            {
                SetValue(DocumentProperty, value);
            }
        }

        public static void OnDocumentChanged(DependencyObject obj,
            DependencyPropertyChangedEventArgs args)
        {
            var rtb = (RichTextBox)obj;
            rtb.Document = (FlowDocument)args.NewValue;
        }

    }
}
