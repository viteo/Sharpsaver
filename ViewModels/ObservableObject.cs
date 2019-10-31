using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sharpsaver.ViewModels
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(
#if !NET30 && !NET35
            [CallerMemberName] 
#endif
        string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
