using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace autobug
{
    class Food : Grid
    {
        double _inicialSize, _actualSize, _growRate;
        double _superFoodSize = 50;
        int _minSize, _maxSize;
        System.Windows.Media.Brush _color;
        Vector _position;
        static int Seed = (int)DateTime.Now.Ticks;
        bool _isSuperFood;
        #region Properties
        public double ActualSize
        {
            get
            {
                return _actualSize;
            }

            set
            {
                Height = value;
                Width = value;
                _actualSize = value;
            }
        }

        public double InicialSize
        {
            get
            {
                return _inicialSize;
            }

            set
            {
                Height = value;
                Width = value;
                ActualSize = value;
                _inicialSize = value;
            }
        }

        public Vector Position
        {
            get
            {
                return _position;
            }

            set
            {
                _position = value;
                RenderTransform = new TranslateTransform(value.X, value.Y);
            }
        }

        public Brush Color
        {
            get
            {
                return _color;
            }

            set
            {
                Background = value;
                _color = value;
            }
        }

        public bool IsSuperFood
        {
            get
            {
                return _isSuperFood;
            }

            set
            {
                InicialSize = (value) ? _superFoodSize : _minSize;
                _isSuperFood = value;
            }
        }
        #endregion

        public void hatch(bool SuperFood = true)
        {
            _growRate = Measure2.RandomDouble();
            _minSize = 2;
            _maxSize = Measure2.Random(5, 10);
            ActualSize = InicialSize;
            if (SuperFood) IsSuperFood = (Measure2.Random(1000) == 1) ? true : false;

            Position = new Vector(Measure2.Random(-300, 300), Measure2.Random(-300, 300));

            Color = Brushes.Red;
        }
        public void hatch(double size, Vector position)
        {
            Position = position;
            InicialSize = size;
            Color = Brushes.Red;
        }
        public void shrink(double bite)
        {
            if (ActualSize >= bite) ActualSize -= bite;
            else hatch();
        }
        public void grow()
        {
            ActualSize += (ActualSize <= _maxSize) ? _growRate / 1000 : 0;
        }
    }
}
