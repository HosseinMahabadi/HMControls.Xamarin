namespace HMControlsSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            SelectableEitorCommand = new Command(async() =>
            {
                await Popup.ShowMessageAsync("SelectableEditor", "Command executed...");
            });
            OnPropertyChanged(nameof(SelectableEitorCommand));
        }

        private HMPopup.Popup Popup { get; set; } = new();
        public Command SelectableEitorCommand { get; set; }
    }

}
