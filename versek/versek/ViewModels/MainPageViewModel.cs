using System.ComponentModel;
using System.Linq;
using versek.Resources;
using Xamarin.Forms;

namespace versek.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            Check = new Command(() =>
            {
                if (string.IsNullOrWhiteSpace(VersReszlet))
                {
                    ThrowHandledError();
                }

                HideEverything();
                var versek = new VersFinder
                {
                    WithoutVowels = AreVowelsEnabled == false,
                    VersReszlet = VersReszlet
                }.Execute();

                if (versek.Count < 1)
                {
                    ThrowHandledError();
                }
                else
                {
                    var neededVers = versek.First();

                    if (versek.Count == 1 || versek.Where(v => v.IsForditas == true).Count() == versek.Count)
                    {
                        VersNev = neededVers.MuNev;
                        KoltoNev = neededVers.Kolto;

                        VersReszletState = false;
                        VersTextState = true;
                        VersText = $"A vers szövege:{System.Environment.NewLine}{System.Environment.NewLine}{neededVers.VersSzoveg.Replace(". ", $".{System.Environment.NewLine}")}";

                        if (neededVers.IsForditas)
                        {
                            AlternativCim = neededVers.AlternativMuNev;
                            AlternativeDataState = !string.IsNullOrWhiteSpace(AlternativCim);

                            if (versek.Count > 1)
                            {
                                VersReszletState = true;
                                VersTextState = false;
                                ForditasDataErrorState = true;
                                ForditasDataErrorText = "A fordítás azonban nem megállapítható ennyi információból!";
                            }
                            else
                            {
                                ForditasDataState = true;
                                ForditoNeve = neededVers.ForditoNeve;
                            }
                        }

                        BaseDataState = true;
                    }
                    else
                    {
                        BaseErrorText = "A rendelkezésre álló információból nem lehetett megállapítani a vers címét! Próbálkozz újra!";
                        BaseDataErrorState = true;

                    }
                }
            });

            Reset = new Command(() =>
            {
                HideEverything();
            });

            ToggleVowel = new Command(() =>
            {
                AreVowelsEnabled = !AreVowelsEnabled;
                var buttonBaseText = "Ékezetek:";
                ToggleButtonText = AreVowelsEnabled ? $"{buttonBaseText} BE" : $"{buttonBaseText} KI";
                ToggleButtonColorCode = AreVowelsEnabled ? "#50C878" : "#DC143C";
            });
        }

        private void ThrowHandledError()
        {
            BaseErrorText = "A megadott részletekkel nem található vers!";
            BaseDataErrorState = true;
            return;
        }

        private void HideEverything()
        {
            VersReszletState = true;
            BaseDataState = false;
            BaseDataErrorState = false;
            AlternativeDataState = false;
            ForditasDataErrorState = false;
            ForditasDataState = false;
            VersTextState = false;
        }

        //Vers részlete
        string versReszlet;
        public string VersReszlet
        {
            get => versReszlet;
            set { versReszlet = value; OnPropertyChanged(nameof(VersReszlet)); }
        }
        //


        //Vers részlete
        bool versReszletState = true;
        public bool VersReszletState
        {
            get => versReszletState;
            set { versReszletState = value; OnPropertyChanged(nameof(VersReszletState)); }
        }
        //


        //Vers neve
        string versNev;
        public string VersNev
        {
            get => versNev;
            set { versNev = value; OnPropertyChanged(nameof(VersNev)); }
        }
        //


        //Hibaüzenet, ha nincsen semmiféle adatunk
        string baseErrorText;
        public string BaseErrorText
        {
            get => baseErrorText;
            set { baseErrorText = value; OnPropertyChanged(nameof(BaseErrorText)); }
        }
        //


        //Hibaüzenet, ha nincsen semmiféle adatunk
        string forditasDataErrorText;
        public string ForditasDataErrorText
        {
            get => forditasDataErrorText;
            set { forditasDataErrorText = value; OnPropertyChanged(nameof(ForditasDataErrorText)); }
        }
        //


        //Költő neve
        string koltoNev;
        public string KoltoNev
        {
            get => koltoNev;
            set { koltoNev = value; OnPropertyChanged(nameof(KoltoNev)); }
        }
        //


        //Költő neve
        string toggleButtonText;
        public string ToggleButtonText
        {
            get => toggleButtonText;
            set { toggleButtonText = value; OnPropertyChanged(nameof(ToggleButtonText)); }
        }
        //


        //Költő neve
        string toggleButtonColorCode;
        public string ToggleButtonColorCode
        {
            get => toggleButtonColorCode;
            set { toggleButtonColorCode = value; OnPropertyChanged(nameof(ToggleButtonColorCode)); }
        }
        //


        //Költő neve
        string alternativCim;
        public string AlternativCim
        {
            get => alternativCim;
            set { alternativCim = value; OnPropertyChanged(nameof(AlternativCim)); }
        }
        //

        //Mezők megjelenésének állapota
        private string forditoNeve;
        public string ForditoNeve
        {
            get { return forditoNeve; }
            set { forditoNeve = value; OnPropertyChanged(nameof(ForditoNeve)); }
        }
        //


        //Mezők megjelenésének állapota
        private string versText;
        public string VersText
        {
            get { return versText; }
            set { versText = value; OnPropertyChanged(nameof(VersText)); }
        }
        //


        //Mezők megjelenésének állapota
        private bool versTextState;
        public bool VersTextState
        {
            get { return versTextState; }
            set { versTextState = value; OnPropertyChanged(nameof(VersTextState)); }
        }
        //


        //Mezők megjelenésének állapota
        private bool _state;
        public bool BaseDataState
        {
            get { return _state; }
            set { _state = value; OnPropertyChanged(nameof(BaseDataState)); }
        }
        //


        //Mezők megjelenésének állapota
        private bool baseDataErrorState;
        public bool BaseDataErrorState
        {
            get { return baseDataErrorState; }
            set { baseDataErrorState = value; OnPropertyChanged(nameof(BaseDataErrorState)); }
        }
        //


        //Mezők megjelenésének állapota
        private bool alternativeDataState;
        public bool AlternativeDataState
        {
            get { return alternativeDataState; }
            set { alternativeDataState = value; OnPropertyChanged(nameof(AlternativeDataState)); }
        }
        //


        //Mezők megjelenésének állapota
        private bool forditasDataErrorState;
        public bool ForditasDataErrorState
        {
            get { return forditasDataErrorState; }
            set { forditasDataErrorState = value; OnPropertyChanged(nameof(ForditasDataErrorState)); }
        }
        //


        //Mezők megjelenésének állapota
        private bool forditasDataState;
        public bool ForditasDataState
        {
            get { return forditasDataState; }
            set { forditasDataState = value; OnPropertyChanged(nameof(ForditasDataState)); }
        }
        //


        //Mezők megjelenésének állapota
        private bool areVowelsEnabled;
        public bool AreVowelsEnabled
        {
            get { return areVowelsEnabled; }
            set { areVowelsEnabled = value; OnPropertyChanged(nameof(AreVowelsEnabled)); }
        }
        //


        //Check
        public Command Check { get; set; }
        //


        //Reset
        public Command Reset { get; set; }

        public Command ToggleVowel { get; set; }
        //


        //Utility
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
