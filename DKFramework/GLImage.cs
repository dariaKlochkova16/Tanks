using System.Drawing;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using System.Collections.Generic;
using SharpGL.VertexBuffers;

namespace DKFramework
{
    struct TextureData
    {
        public Texture texture;
        public int ImageWidht;
        public int ImageHeight;
    }

    public class GLImage
    {
        int _imageWidth;
        int _imageHeight;
        private PointF _point = new PointF();
        private Rectangle _rectangleImage;

        private Texture _texture;
        private static Texture _lastBindTexture;

        private static Dictionary<string, TextureData> _uploadedTexture = new Dictionary<string, TextureData>();
        private PointF coord1, coord2, coord3, coord4;

        private VertexBuffer _vertexBuffer;
        private VertexBuffer _textureBuffer;

        public float Z { get; set; }
        public PointF PointImage
        {
            get { return _point; }
            set { _point = value; }
        }

        public Rectangle RectangleImage
        {
            get { return _rectangleImage; }
            set { _rectangleImage = value; }
        }

        public int Height
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }

        public GLImage(string nameFile, OpenGL gl, float x, float y, float z)
        {
            Width = 1;
            Height = 1;

            if (_uploadedTexture.ContainsKey(nameFile))
            {
                _texture = _uploadedTexture[nameFile].texture;
                _imageHeight = _uploadedTexture[nameFile].ImageHeight;
                _imageWidth = _uploadedTexture[nameFile].ImageWidht;
            }
            else
            {
                CreateTexture(nameFile, gl);
            }

            Constr(gl, x, y, z, new Rectangle(0, 0, (new Bitmap(nameFile)).Width, (new Bitmap(nameFile)).Height));
        }

        private void CreateTexture(string nameFile, OpenGL gl)
        {
            var image = new Bitmap(nameFile);
            _imageWidth = image.Width;
            _imageHeight = image.Height;
            _texture = new Texture();
            TextureData textureData = new TextureData();
            textureData.ImageHeight = image.Height;
            textureData.ImageWidht = image.Width;
            _texture.Create(gl, image);
            textureData.texture = _texture;
            _uploadedTexture[nameFile] = textureData;
        }

        public GLImage(Bitmap image, OpenGL gl, float x, float y, float z, Rectangle rectangleImage)
        {
            _imageWidth = image.Width;
            _imageHeight = image.Height;
            Constr(gl, x, y, z, rectangleImage);
            _texture = new Texture();
            _texture.Create(gl, image);
        }

        public GLImage(string nameFile, OpenGL gl, float x, float y, float z, Rectangle rectangleImage)
        {
            if (_uploadedTexture.ContainsKey(nameFile))
            {
                _texture = _uploadedTexture[nameFile].texture;
                _imageHeight = _uploadedTexture[nameFile].ImageHeight;
                _imageWidth = _uploadedTexture[nameFile].ImageWidht;
            }
            else
            {
                CreateTexture(nameFile, gl);
            }
            Constr(gl, x, y, z, rectangleImage);
        }

        private void Constr(OpenGL gl, float x, float y, float z, Rectangle rectangleImage)
        {
            _point.X = x;
            _point.Y = y;
            Z = z;
            _rectangleImage = rectangleImage;
            Rotate(DKFramework.Rotation.Up);

            //_vertexBuffer = new VertexBuffer();
            //_vertexBuffer.Create(gl);
            //_vertexBuffer.SetData(
            //    gl,
            //    OpenGL.GL_ARRAY_BUFFER,
            //    new float[]
            //    {
            //        0f, 0f, 0f,
            //        0f, -1f, 0f,
            //        1f, -1f, 0f,
            //        1f, 0f, 0f
            //    },
            //    false,
            //    1);

            //_textureBuffer = new VertexBuffer();
            //_textureBuffer.Create(gl);
            //_textureBuffer.SetData(
            //    gl, 
            //    OpenGL.GL_ARRAY_BUFFER,
            //    new float[]
            //    {
            //        0f, 0f, 0f,
            //        0f, -1f, 0f,
            //        1f, -1f, 0f,
            //        1f, 0f, 0f
            //    },
            //    false,
            //    1);
        }

        public void Rotate(Rotation rotate)
        {
            switch (rotate)
            {
                case DKFramework.Rotation.Up:
                    coord1 = new PointF((float)_rectangleImage.X / (float)_imageWidth, (float)_rectangleImage.Y / (float)_imageHeight);
                    coord2 = new PointF((float)_rectangleImage.X / (float)_imageWidth, (float)_rectangleImage.Bottom / (float)_imageHeight);
                    coord3 = new PointF((float)_rectangleImage.Right / (float)_imageWidth, (float)_rectangleImage.Bottom / (float)_imageHeight);
                    coord4 = new PointF((float)_rectangleImage.Right / (float)_imageWidth, (float)_rectangleImage.Y / (float)_imageHeight);
                    break;
                case DKFramework.Rotation.Left:
                    coord1 = new PointF((float)_rectangleImage.Right / (float)_imageWidth, (float)_rectangleImage.Y / (float)_imageHeight);
                    coord2 = new PointF((float)_rectangleImage.X / (float)_imageWidth, (float)_rectangleImage.Y / (float)_imageHeight);
                    coord3 = new PointF((float)_rectangleImage.X / (float)_imageWidth, (float)_rectangleImage.Bottom / (float)_imageHeight);
                    coord4 = new PointF((float)_rectangleImage.Right / (float)_imageWidth, (float)_rectangleImage.Bottom / (float)_imageHeight);
                    break;
                case DKFramework.Rotation.Right:
                    coord1 = new PointF((float)_rectangleImage.X / (float)_imageWidth, (float)_rectangleImage.Bottom / (float)_imageHeight);
                    coord2 = new PointF((float)_rectangleImage.Right / (float)_imageWidth, (float)_rectangleImage.Bottom / (float)_imageHeight);
                    coord3 = new PointF((float)_rectangleImage.Right / (float)_imageWidth, (float)_rectangleImage.Y / (float)_imageHeight);
                    coord4 = new PointF((float)_rectangleImage.X / (float)_imageWidth, (float)_rectangleImage.Y / (float)_imageHeight);
                    break;
                case DKFramework.Rotation.Down:
                    coord1 = new PointF((float)_rectangleImage.Right / (float)_imageWidth, (float)_rectangleImage.Bottom / (float)_imageHeight);
                    coord2 = new PointF((float)_rectangleImage.Right / (float)_imageWidth, (float)_rectangleImage.Y / (float)_imageHeight);
                    coord3 = new PointF((float)_rectangleImage.X / (float)_imageWidth, (float)_rectangleImage.Y / (float)_imageHeight);
                    coord4 = new PointF((float)_rectangleImage.X / (float)_imageWidth, (float)_rectangleImage.Bottom / (float)_imageHeight);
                    break;
                default:
                    coord1 = new PointF((float)_rectangleImage.X / (float)_imageWidth, (float)_rectangleImage.Y / (float)_imageHeight);
                    coord2 = new PointF((float)_rectangleImage.X / (float)_imageWidth, (float)_rectangleImage.Bottom / (float)_imageHeight);
                    coord3 = new PointF((float)_rectangleImage.Right / (float)_imageWidth, (float)_rectangleImage.Bottom / (float)_imageHeight);
                    coord4 = new PointF((float)_rectangleImage.Right / (float)_imageWidth, (float)_rectangleImage.Y / (float)_imageHeight);
                    break;
            }
        }

        public void Draw(OpenGL gl)
        {
            gl.PushMatrix();
            gl.Translate(_point.X, _point.Y, Z);

            gl.Enable(OpenGL.GL_BLEND);
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);

            if (_texture != _lastBindTexture)
            {
                _texture.Bind(gl);
                _lastBindTexture = _texture;
            }

            // _vertexBuffer.Bind(gl);
            // gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, );
            // _textureBuffer.Bind(gl);

            gl.Begin(OpenGL.GL_QUADS);

            gl.TexCoord(coord1.X, coord1.Y); gl.Vertex(0f, 0.0f, 0.0f);
            gl.TexCoord(coord2.X, coord2.Y); gl.Vertex(0.0f, -Height, 0.0f);
            gl.TexCoord(coord3.X, coord3.Y); gl.Vertex(Width, -Height, 0.0f);
            gl.TexCoord(coord4.X, coord4.Y); gl.Vertex(Width, 0.0f, 0.0f);

            gl.End();

            gl.Disable(OpenGL.GL_BLEND);

            gl.PopMatrix();
        }
    }
}
