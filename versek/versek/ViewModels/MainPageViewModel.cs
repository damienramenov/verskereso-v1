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
                HideEverything();
                var versek = new VersFinder
                {
                    VersReszlet = VersReszlet
                }.Execute();

                if (versek.Count < 1)
                {
                    BaseErrorText = "A megadott részletekkel nem található vers!";
                    BaseDataErrorState = true;
                    return;
                }
                else
                {
                    var neededVers = versek.First();

                    if (versek.Count == 1 || versek.Where(v => v.IsForditas == true).Count() == versek.Count)
                    {
                        VersNev = neededVers.MuNev;
                        KoltoNev = neededVers.Kolto;

                        if (neededVers.IsForditas)
                        {
                            AlternativCim = neededVers.AlternativMuNev;
                            AlternativeDataState = !string.IsNullOrWhiteSpace(AlternativCim);

                            if (versek.Count > 1)
                            {
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
        }

        private void HideEverything()
        {
            BaseDataState = false;
            BaseDataErrorState = false;
            AlternativeDataState = false;
            ForditasDataErrorState = false;
            ForditasDataState = false;
        }

        //Vers részlete
        string versReszlet;
        public string VersReszlet
        {
            get => versReszlet;
            set { versReszlet = value; OnPropertyChanged(nameof(VersReszlet)); }
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


        //Check
        public Command Check { get; set; }
        //


        //Utility
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
