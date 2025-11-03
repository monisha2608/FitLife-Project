using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FitLife.ViewModels
{
    // Base class for all ViewModels (handles property change notifications)
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // Updates the field and notifies the UI if the value has changed
        protected void Set<T>(ref T field, T value, [CallerMemberName] string? propName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        // Manually notifies that a property value has changed
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
