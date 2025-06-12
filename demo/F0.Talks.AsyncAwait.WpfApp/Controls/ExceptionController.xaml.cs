using F0.Talks.AsyncAwait.Services;
using System.Windows;
using System.Windows.Controls;

namespace F0.Talks.AsyncAwait.WpfApp.Controls;

public partial class ExceptionController : UserControl
{
    public ExceptionController()
    {
        InitializeComponent();
    }

    private void OnThrowSynchronously(object sender, RoutedEventArgs e)
    {
        ExceptionService.ThrowImmediately();
    }

    private async void OnThrowAsynchronously(object sender, RoutedEventArgs e)
    {
        await ExceptionService.ThrowAsync();
    }
}
