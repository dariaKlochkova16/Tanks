using SharpGL;

namespace DKFramework
{
    public abstract class CoreComponentBase
    {
        protected Core _core;

        public CoreComponentBase(Core core)
        {
            _core = core;
        }

        public virtual void OnDraw(OpenGL gl) { }

        public virtual void Update(float deltaTime)
        {
        }

    }
}
