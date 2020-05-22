using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IntratimeClient.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public void Set<T>(ref T currentValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (currentValue == null
              || !currentValue.Equals(newValue))
            {
                currentValue = newValue;
                RaisePropertyChanged(propertyName);
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}