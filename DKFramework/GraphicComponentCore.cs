using SharpGL;

namespace DKFramework
{
   public class GraphicComponentCore : CoreComponentBase
    {

        public GraphicComponentCore(Core core) : base(core)
        {
            _core = core;
        }

        override public void OnDraw(OpenGL gl)
        {
            for(int i = 0; i < _core.Count; i++)
                _core.GetElenent(i).renderer.Draw(gl);
        }
    }
}
