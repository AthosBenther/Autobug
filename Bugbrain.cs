using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace autobug
{
    class Bugbrain
    {
        #region Variables
        double _walkingSpeed, _chewSpeed, _xp, _level, _biteSize, _mySize, _lastReassurance, _timeStopped, _timeToEvolve;
        string _whatToDo, _whatAmIDoing;
        Vector _where, _whereAmI, _mouseRelativePosition, _targetPosition, _foodPosition;
        Vector pnt = new Vector(0, 0);
        List<Food> _Foods = new List<Food>();
        Food target;
        bool _targetIsSuperFood;
        #endregion
        #region Properties
        public string WhatToDo
        {
            get
            {
                return _whatToDo;
            }
        }
        public string WhatAmIDoing
        {
            get
            {
                return _whatAmIDoing;
            }
            set
            {
                _whatAmIDoing = value;
            }
        }

        public Vector TargetPosition
        {
            get
            {
                return _targetPosition;
            }

            set
            {
                _targetPosition = value;
            }
        }
        public Vector FoodPosition
        {
            get
            {
                return _foodPosition;
            }
            set
            {
                _foodPosition = value;
            }
        }
        public Vector WhereAmI
        {
            get
            {
                return _whereAmI;
            }

            set
            {
                _whereAmI = value;
            }
        }
        public Vector Where
        {
            get
            {
                return _where;
            }

            set
            {
                _where = value;
            }
        }
        public Vector MouseRelativePosition
        {
            get
            {
                return _mouseRelativePosition;
            }

            set
            {
                _mouseRelativePosition = value;
            }
        }

        public double WalkingSpeed
        {
            get
            {
                return _walkingSpeed;
            }

            set
            {
                _walkingSpeed = value;
            }
        }
        public double ChewSpeed
        {
            get
            {
                return _chewSpeed;
            }

            set
            {
                _chewSpeed = value;
            }
        }
        public double Xp
        {
            get
            {
                return _xp;
            }

            set
            {
                _xp = value;
            }
        }
        public double Level
        {
            get
            {
                return _level;
            }

            set
            {
                _level = value;
            }
        }
        public double BiteSize
        {
            get
            {
                return _biteSize;
            }

            set
            {
                _biteSize = value;
            }
        }
        public double MySize
        {
            get
            {
                return _mySize;
            }

            set
            {
                _mySize = value;
            }
        }
        public double LastReassurance
        {
            get
            {
                return _lastReassurance;
            }

            set
            {
                _lastReassurance = value;
            }
        }
        internal List<Food> Foods
        {
            get
            {
                return _Foods;
            }

            set
            {
                _Foods = value;
            }
        }
        #endregion

        public void think()
        {
            double distanceFromFood, distanceFromTarget;
            target = AquireTarget();
            FoodPosition = Convert2.toVector(target);
            distanceFromFood = Measure2.Distance(WhereAmI, FoodPosition);
            TargetPosition = FoodPosition;
            distanceFromTarget = distanceFromFood;
            LastReassurance++;
            switch (WhatAmIDoing)
            {
                case "WALK":
                    if (distanceFromTarget <= 0.001) _whatToDo = "STOP";
                    else if (distanceFromTarget > WalkingSpeed) _whatToDo = "WALK";
                    break;
                case "STOP":
                    if (distanceFromTarget > WalkingSpeed)
                    {
                        _whatToDo = "EVOLVE";
                    }
                    else
                    {
                        _timeStopped++;
                    }
                    break;
                case "EVOLVE":
                    if (_timeToEvolve < _timeStopped * 10)
                    {
                        MySize += 1 / _timeStopped / 10;
                        BiteSize += 1 / _timeStopped / 10000;
                        WalkingSpeed += 1 / _timeStopped / 100000;
                        _timeToEvolve++;
                        _whatToDo = "EVOLVE";
                    }
                    else
                    {
                        _timeToEvolve = 0;
                        _timeStopped = 0;
                        _whatToDo = "WALK";
                    }
                    break;
                default:
                    _whatToDo = "WALK";
                    break;
            }
        }
        private Food AquireTarget()
        {
            Food targetFood;

            if (target == null)
            {
                targetFood = DecideFood();
            }
            else if (target.IsSuperFood) targetFood = target;
            else
            {
                targetFood = DecideFood();
            }
            return targetFood;
        }
        private Food DecideFood()
        {
            List<double> foodPos = new List<double>();
            List<double> foodSize = new List<double>();
            List<Food> SuperFoods = new List<Food>();
            List<Food> FoodsToCheck = new List<Food>();
            Food targetFood, nearestFood;

            foreach (Food food in Foods)
            {
                if (food.IsSuperFood) SuperFoods.Add(food);
            }

            if (SuperFoods.Count > 0)
            {
                FoodsToCheck = SuperFoods;
                _targetIsSuperFood = true;
            }
            else
            {
                FoodsToCheck = Foods;
                _targetIsSuperFood = false;
            }

            foreach (Food food in FoodsToCheck)
            {
                foodPos.Add(Measure2.Distance(WhereAmI, Convert2.toVector(food)));
            }
            int nearestFoodID = foodPos.IndexOf(foodPos.Min());
            nearestFood = FoodsToCheck[nearestFoodID];
            targetFood = nearestFood;

            return targetFood;
        }


        public Vector move(Vector actualPosition, Vector Target, double speed)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                speed++;
            }

            double angle = Measure2.AngleBetween(actualPosition, Target);
            double distance = Measure2.Distance(actualPosition, Target);
            double X = (speed < distance) ? Math.Cos(angle) * speed : Math.Cos(angle) * distance;
            double Y = (speed < distance) ? Math.Sin(angle) * speed : Math.Sin(angle) * distance;
            return new Vector(X, Y);
        }

    }
}
