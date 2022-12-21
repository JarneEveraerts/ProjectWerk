using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDataErrorInfo, INotifyDataErrorInfo
	{
        #region INotifyDataErrorInfo members
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        private readonly Dictionary<string, ICollection<string>> _validationErrors = new();

        public IEnumerable GetErrors(string? propertyName)
        {
            if (string.IsNullOrEmpty(propertyName)
                || !_validationErrors.ContainsKey(propertyName))
                return null;

            return _validationErrors[propertyName];
        }

        public bool HasErrors
        {
            get { return _validationErrors.Count > 0; }
        }
        #endregion

        #region validation (IDataErrorInfo)
        private DataErrorInfoAttributeValidator? _propertyValidations;
        private string? _error;

        private DataErrorInfoAttributeValidator GetValidators()
        {
            return _propertyValidations ?? (_propertyValidations
                = new DataErrorInfoAttributeValidator(this, str => Error = str));
        }

        #region IDataErrorInfo
        public string this[string propertyName] => GetValidators().Validate(propertyName);

        public string Error
        {
            get { GetValidators(); return _error; }
            set { Set(ref _error, value); }
        }
        #endregion
        #endregion

        #region Interface INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? CanExecuteChanged;

        protected void Set<T>(ref T target, T value, [CallerMemberName] string propertyName = "")
		{
			target = value;
			RaisePropertyChanged(propertyName);
		}

		protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
        #endregion
    }
}