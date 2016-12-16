namespace DKFramework
{
    public class CoreComponentPhyics : CoreComponentBase
    {
        public CoreComponentPhyics(Core core) : base(core)
        {
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            for(int i = 0; i < _core.Count; i++)
            {
                if (_core.GetElement(i).GetComponent<MovementController>() != null)
                    _core.GetElement(i).GetComponent<MovementController>().Update(deltaTime);

                if (_core.GetElement(i).GetComponent<AI>() != null)
                    _core.GetElement(i).GetComponent<AI>().Update(deltaTime);
            }      
        }
    }
}
