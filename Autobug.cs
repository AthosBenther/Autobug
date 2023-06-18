using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace autobug
{
    class Autobug : Grid
    {
        #region Variables
        double _size, _speed, _biteSize, _age, _lifespan;
        double _sizeMultiplier, _speedMultiplier, _biteSizeMultiplier, _lifespanMultiplier;
        Bugbrain brain = new Bugbrain();
        Vector _actualPosition;
        public TranslateTransform pos = new TranslateTransform();
        RotateTransform rot = new RotateTransform();
        TransformGroup TransGroup = new TransformGroup();
        int _id;
        bool _amHatched = false;
        public bool _amDying = false;
        public bool _showLabel = false;
        string _type;
        public bool _showLabelPersistent = false;
        private int _mouseDown;
        #endregion
        #region Properties
        public double BiteSize
        {
            get
            {
                return _biteSize;
            }

            set
            {
                _biteSize = value * _biteSizeMultiplier;
            }
        }
        public double Size
        {
            get
            {
                return _size;
            }

            set
            {
                _size = value;
                grow(value);
            }
        }
        public double Speed
        {
            get
            {
                return _speed;
            }

            set
            {
                _speed = value * _speedMultiplier;
            }
        }
        public double Age
        {
            get
            {
                return _age;
            }

            set
            {
                _age = value;
            }
        }
        public double LifeSpan
        {
            get
            {
                return _lifespan;
            }

            set
            {
                _lifespan = value * _lifespanMultiplier;
            }
        }

        public Vector ActualPosition
        {
            get
            {
                return _actualPosition;
            }

            set
            {
                _actualPosition = value;
            }
        }

        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        public string Type
        {
            get
            {
                return _type;
            }

            set
            {
                _type = value;
            }
        }
        #endregion
        #region Names

        #endregion
        #region Methods
        public void hatch()
        {
            _sizeMultiplier = _speedMultiplier = _biteSizeMultiplier = _lifespanMultiplier = 1;
            _size = 2;
            ActualPosition = new Vector(Measure2.Random(-300, 300), Measure2.Random(-300, 300));
            _speed = 0.1;
            _biteSize = 0.05;

            hatch(_size, ActualPosition, _speed, _biteSize, 2000);
        }
        public void hatch(double size, Vector Position, double speed, double biteSize, double lifeSpan)
        {
            GenType();
            Background = System.Windows.Media.Brushes.White;
            LifeSpan = lifeSpan;
            Size = size;
            ActualPosition = Position;
            Speed = speed;
            brain.WhatAmIDoing = "STOP";
            BiteSize = biteSize;
            _amHatched = true;

            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;


            pos.X = ActualPosition.X;
            pos.Y = ActualPosition.Y;

            RenderTransform = pos;


            brain.WhereAmI = ActualPosition;


            reassureCapabilities();
        }

        private void grow()
        {
            Height++;
            Width++;
        }
        private void grow(double size)
        {
            Height = size;
            Width = size;
        }

        public void walk(Vector Target)
        {
            var newPosition = brain.move(ActualPosition, Target, Speed);
            pos.X -= newPosition.X;
            pos.Y -= newPosition.Y;

            RenderTransform = pos;

            ActualPosition = Convert2.toVector(this);
        }
        public void act(List<Food> Foods)
        {
            if (_amHatched)
            {
                brain.WhereAmI = ActualPosition;
                brain.Foods = Foods;
                if (!_amDying)
                {
                    brain.think();
                    switch (brain.WhatToDo)
                    {
                        case "WALK":
                            walk(brain.TargetPosition);
                            brain.WhatAmIDoing = "WALK";

                            break;
                        case "STOP":
                            brain.WhatAmIDoing = "STOP";

                            break;
                        case "EVOLVE":
                            brain.WhatAmIDoing = "EVOLVE";
                            Speed = brain.WalkingSpeed;
                            BiteSize = brain.BiteSize;
                            Size = brain.MySize;
                            _lifespan += 2;

                            break;
                        default:
                            break;
                    }
                    _age++;
                    if (Age > LifeSpan) _amDying = true;
                }
                else
                {
                    Die();
                }
            }
        }

        private void reassureCapabilities()
        {
            brain.WalkingSpeed = Speed;
            brain.MySize = Size;
            brain.WhereAmI = ActualPosition;
            brain.BiteSize = BiteSize;
            brain.LastReassurance = 0;
        }
        private void Die()
        {
            Size -= (Size / Age < Size) ? Size / Age : Size;
            Age -= 5;

            if (Age <= 0)
            {
                _amDying = false;
                hatch();
            }
        }

        private void GenType()
        {
            double multiplyer = 2;
            switch (Measure2.Random(5))
            {
                case 0:
                    Type = "GLUTÃO";
                    _biteSizeMultiplier = multiplyer;
                    break;
                case 1:
                    Type = "APRESSADINHO";
                    _speedMultiplier = multiplyer;
                    break;
                case 2:
                    Type = "ZÉ";
                    _biteSizeMultiplier = 1;
                    break;
                case 3:
                    Type = "MATUSALÉM";
                    _lifespanMultiplier = multiplyer;
                    break;
                case 4:
                    Type = "EINSTEIN";
                    LifeSpan = 1;
                    goto case 5;
                case 5:
                    Type = "ZÉ";
                    break;
                default:
                    break;
            }
        }
        #endregion
        #region Overrides
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            _showLabel = true;
        }
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            _showLabel = false;
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            _showLabelPersistent = !_showLabelPersistent;
        }
        #endregion
    }
}
