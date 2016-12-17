using System.Drawing;
using SharpGL;

namespace DKFramework
{
    public class Render : ComponentBase
    {
        private Rectangle _rectangleImage;
        private GLImage _glImage;

        public string TextureFileName { get; set; }

        public Bitmap Image { get; set; }
        
        public Rectangle RectangleImage
        {
            get { return _rectangleImage; }
            set { _rectangleImage = value; }
        }

        public Render(GameObject linkGameObject) : base(linkGameObject) { }

        public void Draw(OpenGL gl)
        {
            Transform transform = LinkGameObject.GetComponent<Transform>();

            if (_glImage == null)
            {
                _glImage = new GLImage(TextureFileName, gl, transform.X, transform.Y, transform.Z, _rectangleImage);
                _glImage.Width = transform.Size.Width;
                _glImage.Height = transform.Size.Height;
            }
            else
            {
                PointF point = new PointF(transform.X, transform.Y);
                _glImage.PointImage = point;
                _glImage.Z = transform.Z;
                _glImage.RectangleImage = _rectangleImage;
            }
            
            _glImage.Width = transform.Size.Width;
            _glImage.Height = transform.Size.Height;
            _glImage.Rotate(transform.Rotaton);
            _glImage.Draw(gl);
        }
    }
}
