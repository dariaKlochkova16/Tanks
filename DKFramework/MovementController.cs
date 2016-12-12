using System;
using System.Drawing;

namespace DKFramework
{
    public class MovementController : ComponentBase
    {
        private float _maneuverTime;
        private float _pastTime = 0;

        public event EventHandler EndMovement;

        public float Velocity { get; set; } = 5;

        public bool Moving { get; private set; } = false;

        private PointF _startPosition;
        private PointF _finishPosition;

        public MovementController(GameObject linkGameObject) : base(linkGameObject)
        {
            LinkGameObject.MessageReceived += LinkGameObject_MessageReceived;
        }

        private void LinkGameObject_MessageReceived(object sender, EventArgs e)
        {
            Moving = false;
        }

        public void MakeMovement(int distance)
        {
            if (Moving)
            {
                return;
                throw new Exception("Объект уже двигается");
            }

            _pastTime = 0;
            _maneuverTime = distance / Velocity;

            _startPosition = LinkGameObject.GetComponent<Transform>().Position;

            // TODO
            int distanceX = 0, distanceY = 0;
            switch (LinkGameObject.GetComponent<Transform>().Rotaton)
            {
                case (Rotation.Down):
                    distanceX = 0;
                    distanceY = -distance;
                    break;
                case (Rotation.Left):
                    distanceX = -distance;
                    distanceY = 0;
                    break;
                case (Rotation.Right):
                    distanceX = distance;
                    distanceY = 0;
                    break;
                case (Rotation.Up):
                    distanceX = 0;
                    distanceY = distance;
                    break;
                default:
                    break;
            }
            _finishPosition = new PointF(_startPosition.X + distanceX, _startPosition.Y + distanceY);
            Moving = true;
        }

        

        public override void Update(float deltaTime)
        {
            if (!Moving)
                return;

            _pastTime += deltaTime;

            if (_pastTime >= _maneuverTime)
            {
                LinkGameObject.GetComponent<Transform>().Position = _finishPosition;
                Moving = false;
                if(EndMovement != null)
                    EndMovement(this, new EventArgs());
                return;
            }

            var positon = Mathem.Lerp(_startPosition, _finishPosition, _pastTime / _maneuverTime);

            //if (Leave(positon))
            //{
            //LinkGameObject.GetComponent<Transform>().Position = new PointF(
            //    Mathem.Clamp(0, 51, positon.X),
            //    Mathem.Clamp(-51, 0, positon.Y));
            //Moving = false;
            //if (EndMovement != null)
            //    EndMovement(this, new EventArgs());
            //}
            //else
            //{
            LinkGameObject.GetComponent<Transform>().Position = positon;
            //}
        }
     
    }
}
