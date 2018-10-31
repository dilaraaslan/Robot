using System;

namespace Robot
{
    internal class RobotikGezgin
    {
        private readonly Position _position;
        private readonly int _xLimit;
        private readonly int _yLimit;
        public RobotikGezgin()
        {
            _position = new Position();
        }
        public RobotikGezgin(int x, int y, Direction direction, int xLimit, int yLimit)
        {
            _position = new Position(x, y, direction);
            _xLimit = xLimit;
            _yLimit = yLimit;
        }

        public void Drive(string commandLetters)
        {
            MoveByCommands(commandLetters);
            CheckBorders();
        }
        //Commands 
        private void MoveByCommands(string commandLetters)
        {
            foreach (var item in commandLetters)
            {
                if (item.Equals('L'))
                {
                    TurnLeft();
                }
                else if (item.Equals('R'))
                {
                    TurnRight();
                }
                else if (item.Equals('M'))
                {
                    Move();
                }
            }
        }

        private void Move()
        {
            switch (_position.Direction)
            {
                case Direction.N:
                    _position.IncrementY();
                    break;
                case Direction.S:
                    _position.DecrementY();
                    break;
                case Direction.W:
                    _position.DecrementX();
                    break;
                case Direction.E:
                    _position.IncrementX();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CheckBorders()
        {
            if (_position.X > _xLimit) _position.X = _xLimit;
            if (_position.X < 0) _position.X = 0;
            if (_position.Y > _yLimit) _position.Y = _yLimit;
            if (_position.Y < 0) _position.Y = 0;
        }

        private void TurnLeft()
        {
            switch (_position.Direction)
            {
                case Direction.N:
                    _position.Direction = Direction.W;
                    break;
                case Direction.W:
                    _position.Direction = Direction.S;
                    break;
                case Direction.S:
                    _position.Direction = Direction.E;
                    break;
                case Direction.E:
                    _position.Direction = Direction.N;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void TurnRight()
        {
            switch (_position.Direction)
            {
                case Direction.N:
                    _position.Direction = Direction.E;
                    break;
                case Direction.W:
                    _position.Direction = Direction.N;
                    break;
                case Direction.S:
                    _position.Direction = Direction.W;
                    break;
                case Direction.E:
                    _position.Direction = Direction.S;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override string ToString()
        {
            return _position.X + " " + _position.Y + " " + _position.Direction;
        }

    }

    internal class Position
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Direction Direction { get; set; }

        public Position()
        {
            X = 0;
            Y = 0;
            Direction = Direction.N;
        }
        public Position(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }
        public void IncrementX()
        {
            X++;
        }
        public void IncrementY()
        {
            Y++;
        }
        public void DecrementX()
        {
            X--;
        }
        public void DecrementY()
        {
            Y--;
        }
    }
    // yönler
    internal enum Direction
    {
        N,
        S,
        W,
        E
    }

    internal class Program
    {
        static void Main()
        {
            //Verilerin doğru girildiği varsayıldı.
            var limits = Console.ReadLine();
            var firstPositionLine = Console.ReadLine();
            var firstCommandsLine = Console.ReadLine();
            var secondPositionLine = Console.ReadLine();
            var secondCommandsLine = Console.ReadLine();

            var xLimit = int.Parse(limits.Substring(0, 1));
            var yLimit = int.Parse(limits.Substring(2, 1));

            var aX = int.Parse(firstPositionLine.Substring(0, 1));
            var aY = int.Parse(firstPositionLine.Substring(2, 1));
            Direction dirA = (Direction)Enum.Parse(typeof(Direction), firstPositionLine.Substring(4, 1));
            RobotikGezgin robotikGezginA = new RobotikGezgin(aX, aY, dirA, xLimit, yLimit);
            robotikGezginA.Drive(firstCommandsLine);
            Console.WriteLine(robotikGezginA);
            Console.ReadKey();

            var bX = int.Parse(secondPositionLine.Substring(0, 1));
            var bY = int.Parse(secondPositionLine.Substring(2, 1));
            Direction dirB = (Direction)Enum.Parse(typeof(Direction), secondPositionLine.Substring(4, 1));
            RobotikGezgin robotikGezginB = new RobotikGezgin(bX, bY, dirB, xLimit, yLimit);
            robotikGezginB.Drive(secondCommandsLine);
            Console.WriteLine(robotikGezginB);
            Console.ReadKey();

        }
    }
}
