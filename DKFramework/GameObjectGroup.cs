using SharpGL;
using System.Drawing;

namespace DKFramework
{
    public class GameObjectGroup
    {
        private GameObject[,] _object;
        private int _brushSize;

        public GameObjectGroup(int brushSize, ObjectType objectType)
        {
            Name = objectType.ToString();
            _brushSize = brushSize;
            _object = new GameObject[brushSize, brushSize];
            for (int i = 0; i < brushSize; i++)
            {
                for (int j = 0; j < brushSize; j++)
                {
                    _object[i, j] = GameObjectFactory.CreateGameObject(objectType);
                }
            }
        }

        public void Transform(int x, int y)
        {
            for (int i = 0; i < _brushSize; i++)
            {
                for (int j = 0; j < _brushSize; j++)
                {
                    _object[i, j].GetComponent<Transform>().X = x + i;
                    _object[i, j].GetComponent<Transform>().Y = y + j;
                }
            }
        }

        public GameObject GetElement(int i, int j)
        {
            return _object[i, j];
        }

        public int Size
        {
            get { return _brushSize; }
            set { _brushSize = value; }
        }

        public string Name
        {
            get;
            set;
        }

        public void SetRectangle(Rectangle rectangle)
        {

            for (int i = 0; i < _brushSize; i++)
            {
                for (int j = 0; j < _brushSize; j++)
                {
                    _object[i, j].GetComponent<Render>().RectangleImage = rectangle;
                }
            }
        }

        public void Draw(OpenGL gl)
        {
            for (int i = 0; i < _brushSize; i++)
            {
                for (int j = 0; j < _brushSize; j++)
                {
                    _object[i,j].GetComponent<Render>().Draw(gl);

                }
            }
        }

    }
}
