using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
namespace autobug
{
    class BugLabel : Label
    {
        #region Variables
        string _bugName, _bugType;
        double _ageNow, _ageMax;
        bool _isOn, _relativeAge;

        #endregion
        #region Properties
        public string BugType
        {
            get
            {
                return _bugType;
            }
            set
            {
                _bugType = value;
            }
        }
        public string BugName
        {
            get
            {
                return _bugName;
            }

            set
            {
                _bugName = value;
            }
        }

        public double AgeNow
        {
            get
            {
                return _ageNow;
            }

            set
            {
                _ageNow = value;
            }
        }

        public double AgeMax
        {
            get
            {
                return _ageMax;
            }

            set
            {
                _ageMax = value;
            }
        }

        public bool IsOn
        {
            get
            {
                return _isOn;
            }

            set
            {
                _isOn = value;
            }
        }

        public bool RelativeAge
        {
            get
            {
                return _relativeAge;
            }

            set
            {
                _relativeAge = value;
            }
        }

        public bool ShowLabel { get; internal set; }
        public bool ShowLabelPersistent { get; internal set; }
        #endregion
        #region Constructors
        #endregion
        #region Methods
        public void populate()
        {
            Background = Brushes.Transparent;
            BorderBrush = Brushes.Transparent;
            FontFamily = new System.Windows.Media.FontFamily("Consolas");
            FontSize = 10;
            Foreground = Brushes.White;
            if (ShowLabelPersistent) IsOn = true;
            else if (ShowLabel) IsOn = true;
            Content = (IsOn) ? Label() : "";
            Height = 50;
            Width = 100;
        }
        private string Label()
        {
            string FinalString;
            if (BugName.ToLower().Trim() == "morto") FinalString = BugName;
            else
            {
                FinalString = BugName.ToUpper();
                FinalString += "\n" + BugType.ToLower();
                if (RelativeAge) FinalString += "\n" + (AgeNow / AgeMax).ToString("0.00");
                else FinalString += "\n" + AgeNow.ToString() + @" / " + AgeMax.ToString();
            }
            return FinalString;
        }
        #endregion
    }
}
