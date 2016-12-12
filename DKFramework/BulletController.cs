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
                _movementController.EndMovement += new EventHandler(EndMovement);
        }

        private void EndMovement(object sender, EventArgs e)
        {
            //Core.Instance.Remove(LinkGameObject);
        }
    }
}
