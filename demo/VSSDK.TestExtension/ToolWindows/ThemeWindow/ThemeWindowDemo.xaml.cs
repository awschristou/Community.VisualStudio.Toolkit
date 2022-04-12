using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.VisualStudio.Shell;

namespace TestExtension
{
    public class SampleItem : INotifyPropertyChanged
    {
        private string _name;
        private int _age;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                RaisePropertyChanged();
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public partial class ThemeWindowDemo : UserControl, INotifyPropertyChanged
    {
        private double _progress;
        private SampleItem _sampleItem;

        public ThemeWindowDemo()
        {
            _sampleItems = new()
            {
                new() { Name = "Jim", Age = 21 },
                new() { Name = "Mads", Age = 31 },
                new() { Name = "Bob", Age = 41 },
                new() { Name = "Gary", Age = 51 },
            };

            InitializeComponent();
            UpdateProgressAsync().FireAndForget();
        }

        private async Task UpdateProgressAsync()
        {
            while (true)
            {
                await Task.Delay(250).ConfigureAwait(true);
                Progress = (Progress + 5) % 101;
            }
        }

        private ObservableCollection<SampleItem> _sampleItems;

        public ObservableCollection<SampleItem> SampleItems
        {
            get => _sampleItems;
            set
            {
                _sampleItems = value;
                RaisePropertyChanged();
            }
        }

        public SampleItem SelectedSampleItem
        {
            get => _sampleItem;
            set
            {
                _sampleItem = value;
                RaisePropertyChanged();
            }
        }

        public IEnumerable<string> ListItems { get; } = new string[] {
                "First",
                "Second",
                "Third",
                "Fourth",
                "Fifth",
                "Sixth"
        };

        public double Progress
        {
            get
            {
                return _progress;
            }
            set
            {
                _progress = value;
                RaisePropertyChanged();
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
