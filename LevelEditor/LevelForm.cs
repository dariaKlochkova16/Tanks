using System;
using System.Windows.Forms;
using SharpGL;
using DKFramework;
using System.Xml.Serialization;
using System.IO;
using System.Drawing;

namespace LevelEditor
{
    public partial class LevelForm : Form
    {
        private GameObjectGroup _gameObjectFakeGroup;
        private CollisionComponentCore collisionComponentCore;
        private const int _orhto = 52;
        private float _pixelToUnit;

        private int _brushSize = 1;

        private GLImage grid;

        private bool pressMouse = false;
        private bool leaveMouse = true;

        TextureDescriptor[] TextureCard = new TextureDescriptor[] { };

        public LevelForm()
        {
            InitializeComponent();

            XmlSerializer formatter = new XmlSerializer(typeof(TextureDescriptor[]));
            TextReader reader = new StreamReader("TextureDescriptor.xml");

            _pixelToUnit = (float)_orhto / openGLControl1.Width;
            TextureCard = (TextureDescriptor[])formatter.Deserialize(reader);
            InitCore();

            Bitmap image = new Bitmap("grid.png");
            grid = new GLImage(
                image,
                this.openGLControl1.OpenGL, 0, 0, 0,
                new Rectangle(
                    0,
                    0,
                    _orhto * image.Width / 2,
                    _orhto * image.Height / 2));
            grid.Width = _orhto;
            grid.Height = _orhto;
            InitButton();
        }

        private void InitCore()
        {
            Core.Instance.GLControl = openGLControl1.OpenGL;
            Core.Instance.AddComponent<SizeFieldComponentCore>();
            Core.Instance.GetComponent<SizeFieldComponentCore>().SizeField = new Size(_orhto, _orhto);
            Core.Instance.AddComponent<GraphicComponentCore>();
            Core.Instance.AddComponent<CollisionComponentCore>();
            Core.Instance.AddComponent<ResoursMenager>();   
            collisionComponentCore = Core.Instance.GetComponent<CollisionComponentCore>();
        }

        private void openGLControl1_OpenGLDraw(object sender, RenderEventArgs e)
        {
            //  Get the OpenGL object, for quick access.
            OpenGL gl = this.openGLControl1.OpenGL;

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Ortho(0, _orhto, -_orhto, 0, -100, 100);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            Core.Instance.Draw();

            if (_gameObjectFakeGroup != null && !leaveMouse)
            {
                _gameObjectFakeGroup.Draw(gl);
            }

            if (checkBox1.Checked)
            {
                grid.Draw(gl);
            }

            gl.Flush();
        }

        private void openGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {
            //  Get the OpenGL object, for quick access.
            OpenGL gl = this.openGLControl1.OpenGL;

            //  A bit of extra initialisation here, we have to enable textures.
            gl.Enable(OpenGL.GL_TEXTURE_2D);


            // _glImage = new GLImage("Crate.bmp", gl, 0, 0, 0);   
        }

        //Mouse events
        private void openGLControl1_MouseDown(object sender, MouseEventArgs e)
        {
            int x = RoundNumber(e.X * _pixelToUnit);
            int y = -RoundNumber(e.Y * _pixelToUnit);

            if (_gameObjectFakeGroup != null)
            {
                _gameObjectFakeGroup.Transform(x, y);

                for (int i = 0; i < _gameObjectFakeGroup.Size; i++)
                {
                    for (int j = 0; j < _gameObjectFakeGroup.Size; j++)
                    {
                        Transform transform = _gameObjectFakeGroup.GetElement(i, j).GetComponent<Transform>();
                        if (transform.Y <= 0 && transform.Y >= -_orhto && transform.X < _orhto && transform.X >= 0)
                        {
                            if (!collisionComponentCore.CrossingTest(_gameObjectFakeGroup.GetElement(i, j)))
                            {
                                Core.Instance.Add(_gameObjectFakeGroup.GetElement(i, j));
                                collisionComponentCore.Add(_gameObjectFakeGroup.GetElement(i, j),false);
                            }
                        }
                    }
                }

                if (_gameObjectFakeGroup.Name != "Base" && _gameObjectFakeGroup.Name != "Player")
                {
                    string name = _gameObjectFakeGroup.Name;
                    ButtonClick(ObjectTypeTools.Convert(name));
                    pressMouse = true;
                }
                else
                {
                    _gameObjectFakeGroup = null;
                }
            }
            else
            {
                RemoveElement(x, y);
            }

            if (!ExistElement(ObjectType.Base))
            {
                Base.Enabled = true;
            }

            if (!ExistElement(ObjectType.Player))
            {
                pointEnterPlayer.Enabled = true;
            }
           
            openGLControl1.Refresh();
        }

        private void openGLControl1_MouseUp(object sender, MouseEventArgs e)
        {
            pressMouse = false;
        }

        private void openGLControl1_MouseMove(object sender, MouseEventArgs e)
        {
            int x = RoundNumber(e.X * _pixelToUnit);
            int y = -RoundNumber(e.Y * _pixelToUnit);
            if (_gameObjectFakeGroup != null)
            {
                _gameObjectFakeGroup.Transform(x, y);
                openGLControl1.Refresh();

                if (_gameObjectFakeGroup.Name != "Enemy" && _gameObjectFakeGroup.Name != "Player" && _gameObjectFakeGroup.Name != "Base" && pressMouse)
                {
                    openGLControl1_MouseDown(sender, e);
                }
            }
            else if (pressMouse)
            {
                RemoveElement(x, y);
            }

            string strX = x.ToString();
            string strY = y.ToString();
            label2.Text = strX + ";" + strY;
        }

        private void openGLControl1_MouseLeave(object sender, EventArgs e)
        {
            if (_gameObjectFakeGroup != null)
            {
                leaveMouse = true;
            }
        }

        //TrackBar
        private void brushSize_Scroll(object sender, EventArgs e)
        {
            _brushSize = brushSize.Value;

            string name = _gameObjectFakeGroup.Name;
            _gameObjectFakeGroup = new GameObjectGroup(_brushSize, ObjectTypeTools.Convert(name));

            if (_gameObjectFakeGroup != null && _gameObjectFakeGroup.Name != "Enemy" && _gameObjectFakeGroup.Name != "Player" && _gameObjectFakeGroup.Name != "Base")
            {
                ButtonClick(ObjectTypeTools.Convert(_gameObjectFakeGroup.Name));
            }
            openGLControl1.Refresh();
        }

        //Button
        private void InitButton()
        {
            Bitmap originalBitmap = new Bitmap("TextureCard.png");
            brickWall.Image = CutImage(originalBitmap, ObjectType.BrickWall);
            concreteWall.Image = CutImage(originalBitmap, ObjectType.ConcreteWall);
            forest.Image = CutImage(originalBitmap, ObjectType.Forest);
            ice.Image = CutImage(originalBitmap, ObjectType.Ice);
            water.Image = CutImage(originalBitmap, ObjectType.Water);
            Base.Image = CutImage(originalBitmap, ObjectType.Base);
            pointEnterPlayer.Image = CutImage(originalBitmap, ObjectType.Player);
            pointEnterEnemy.Image = CutImage(originalBitmap, ObjectType.Enemy);
        }

        private Bitmap CutImage(Bitmap originalBitmap, ObjectType objectType)
        {
            var rectangle = new Rectangle();
            foreach (TextureDescriptor element in TextureCard)
            {
                if (element.Name == objectType.ToString())
                {
                    rectangle = new Rectangle(element.Point, element.Size);
                    break;
                }
            }
            Bitmap croppedImage = originalBitmap.Clone(rectangle, originalBitmap.PixelFormat);

            return croppedImage;
        }

        private void forest_Click(object sender, EventArgs e)
        {
            SelectResizeElement(ObjectType.Forest);
        }

        private void water_Click(object sender, EventArgs e)
        {
            SelectResizeElement(ObjectType.Water);
        }

        private void ice_Click(object sender, EventArgs e)
        {
            SelectResizeElement(ObjectType.Ice);
        }

        private void Base_Click(object sender, EventArgs e)
        {
            SelectUnresizeElement(ObjectType.Base);
            Base.Enabled = false;
        }

        private void concreteWall_Click(object sender, EventArgs e)
        {
            SelectResizeElement(ObjectType.ConcreteWall);
        }

        private void brickWall_Click(object sender, EventArgs e)
        {
            SelectResizeElement(ObjectType.BrickWall);
        }

        private void eraser_Click(object sender, EventArgs e)
        {
            _gameObjectFakeGroup = null;
            brushSize.Enabled = false;
        }

        private void pointEnterPlayer_Click(object sender, EventArgs e)
        {
            SelectUnresizeElement(ObjectType.Player);
            pointEnterPlayer.Enabled = false;
        }

        private void pointEnterEnemy_Click(object sender, EventArgs e)
        {
            SelectUnresizeElement(ObjectType.Enemy);
        }

        private void SelectResizeElement(ObjectType objectType)
        {
            brushSize.Enabled = true;
            ButtonClick(objectType);
            //_transformObjectFake = _transformObject;
            openGLControl1.Refresh();
        }

        private void SelectUnresizeElement(ObjectType objectType)
        {        
            _gameObjectFakeGroup = new GameObjectGroup(1, objectType);

            openGLControl1.Refresh();

            brushSize.Enabled = false;
        }

        private void ButtonClick(ObjectType objectType)
        {     
            if (objectType.ToString() != "Enemy")
            {
                _gameObjectFakeGroup = new GameObjectGroup(_brushSize, objectType);
            }
            else
            {
                _gameObjectFakeGroup = new GameObjectGroup(1, objectType);
            }
        }

        //Menu
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.InitialDirectory = "C:\\Users\\даша\\Documents\\OOP\\ЛАБЫ\\Tanks\\LevelEditor\\bin\\Debug\\";
            saveFileDialog1.Filter = "xml files (*.xml)|*.xml";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LevelFile.Save(saveFileDialog1.FileName);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Core.Instance.ReCreate();
            InitCore();

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "C:\\Users\\даша\\Documents\\OOP\\ЛАБЫ\\Tanks\\LevelEditor\\bin\\Debug\\";
            openFileDialog1.Filter = "xml files (*.xml)|*.xml";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LevelFile.Open(openFileDialog1.FileName);
            }
            openGLControl1.Refresh();
        }

          ////

        private int RoundNumber(float n)

        {
            return (int)n;
        }

        private bool ExistElement(ObjectType objectType)
        {
            for (int i = 0; i < Core.Instance.Count; i++)
            {
                if (Core.Instance.GetElement(i).Name == objectType.ToString())
                {
                    return true;
                }
            }
            return false;
        }

        private void RemoveElement(int x, int y)
        {
            for (int i = 0; i < Core.Instance.Count; i++)
            {
                float coreX = Math.Abs(Core.Instance.GetElement(i).GetComponent<Transform>().X);
                float coreY = Math.Abs(Core.Instance.GetElement(i).GetComponent<Transform>().Y);
                Size size = Core.Instance.GetElement(i).GetComponent<Transform>().Size;
                if (coreX <= Math.Abs(x)
                    && coreY <= Math.Abs(y)
                   && coreX + size.Width >= Math.Abs(x)
                   && coreY + size.Height >= Math.Abs(y))
                {
                    collisionComponentCore.Remove((int)coreX, (int)coreY, size);
                    Core.Instance.Remove(Core.Instance.GetElement(i));
                    break;
                }
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            Core.Instance.ReCreate();
            InitCore();
        }

        private void openGLControl1_MouseEnter(object sender, EventArgs e)
        {
            leaveMouse = false;
        }
    }
}


