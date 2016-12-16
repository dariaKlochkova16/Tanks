using System;

namespace DKFramework
{
    public class BulletController : ComponentBase
    {
        MovementController _movementController;

        public BulletController(GameObject linkGameObject) : base(linkGameObject)
        {
            _movementController = LinkGameObject.GetComponent<MovementController>();
            if (LinkGameObject.GetComponent<MovementController>() != null)
                _movementController.EndMovement += new EventHandler<MessageBase>(EndMovement);
        }

        private void EndMovement(object sender, MessageBase message)
        {
            MessageCollision messageCollision = (MessageCollision)message;
            if (messageCollision.GameObject != null && messageCollision.GameObject.GetComponent<DamagedReceiver>() != null)
                messageCollision.GameObject.GetComponent<DamagedReceiver>().DamageCaused(1);
            Core.Instance.Remove(LinkGameObject);
        }
    }
}
