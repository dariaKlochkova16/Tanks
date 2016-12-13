using DKFramework;
using SharpGL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tanks
{
    public partial class GameForm : Form
    {
        private GameObject _tank;
        private const int _orhto = 52;

        private DateTime _pastUpdateTime;
        float deltaTimeF = 0;

        private DateTime _pastShoot;

        private int _frameCounter = 0;

        private MovementController _tankController;

        private bool keyPress;

        TextureDescriptor[] TextureCard = new TextureDescriptor[] { };

        public GameForm()
        {
            InitializeComponent();

            InitCore();

            TextureCard = Core.Instance.GetComponent<ResoursMenager>().TextureCard;

            CreatePlayer();

            FPStimer.Interval = 1000;
            moveTimer.Start();
            CoreTimer.Start();
            FPStimer.Start();
        }

        private void InitCore()
        {
            Core.Instance.GLControl = openGLControl1.OpenGL;
            Core.Instance.AddComponent<SizeFieldComponentCore>();
            Core.Instance.GetComponent<SizeFieldComponentCore>().SizeField = new Size(_orhto, _orhto);
            Core.Instance.AddComponent<GraphicComponentCore>();
            Core.Instance.AddComponent<CoreComponentPhyics>();
            Core.Instance.AddComponent<ResoursMenager>();
            Core.Instance.AddComponent<CollisionComponentCore>();       
        }

        private void CreatePlayer()
        {
            _tank = GameObjectFactory.CreateGameObject(ObjectType.Player);
            _tank.GetComponent<Transform>().X = 10;
            _tank.GetComponent<Transform>().Y = -30;
            _tankController = _tank.GetComponent<MovementController>();

            Core.Instance.Add(_tank);

            //for(int i = 0; i < Core.Instance.Count; i++)
            //{
            //    if (Core.Instance.GetElenent(i).Name == "Player")
            //    {
            //        _tank = Core.Instance.GetElenent(i);
            //        _tankController = _tank.GetComponent<MovementController>();
            //        break;
            //    }
            //}            
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
           

            gl.Flush();

            _frameCounter++;
        }

        private void openGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {
            // Core.Instance.Clear();
            //  Get the OpenGL object, for quick access.
            OpenGL gl = this.openGLControl1.OpenGL;
            //  A bit of extra initialisation here, we have to enable textures.
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            // _glImage = new GLImage("Crate.bmp", gl, 0, 0, 0);   
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "C:\\Users\\даша\\Documents\\OOP\\ЛАБЫ\\Tanks\\LevelEditor\\bin\\Debug\\";
            openFileDialog1.Filter = "xml files (*.xml)|*.xml";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                LevelFile.Open(openFileDialog1.FileName);
           // CreatePlayer();
        }

        private void openGLControl1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (_tank != null)
                if (!_tank.GetComponent<MovementController>().Moving)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Down:
                            {
                                SetRotation(Rotation.Down);
                            }
                            break;
                        case Keys.Up:
                            {
                                SetRotation(Rotation.Up);
                            }
                            break;
                        case Keys.Left:
                            {
                                SetRotation(Rotation.Left);
                            }
                            break;
                        case Keys.Right:
                            {
                                SetRotation(Rotation.Right);
                            }
                            break;
                        case Keys.Space:
                            Shot();
                            break;
                    }
                }
        }

        private void SetRotation(Rotation rot)
        {
            if (_tank != null)
            {
                _tank.GetComponent<Transform>().Rotaton = rot;
                keyPress = true;
            }
        }

        private void Shot()
        {
            TimeSpan deltaTime = DateTime.Now - _pastShoot;
            if (deltaTime.TotalMilliseconds > 1000)
            {
                var bullet = _tank.GetComponent<ShootComponent>().Shoot();
                Core.Instance.Add(bullet);
                _pastShoot = DateTime.Now;
            }
        }

        private void CoreTimer_Tick(object sender, EventArgs e)
        {
            var nowTime = DateTime.Now;
            TimeSpan deltaTime = nowTime - _pastUpdateTime;
            deltaTimeF = (float)deltaTime.TotalSeconds;

            Core.Instance.Update(deltaTimeF);

            openGLControl1.Refresh();

            _pastUpdateTime = nowTime;
        }

        private void FPStimer_Tick(object sender, EventArgs e)
        {
            Text = "Tanks " + _frameCounter;
            _frameCounter = 0;
        }

        private void openGLControl1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    {
                        keyPress = false;
                    }
                    break;
                case Keys.Up:
                    {
                        keyPress = false;
                    }
                    break;
                case Keys.Left:
                    {
                        keyPress = false;
                    }
                    break;
                case Keys.Right:
                    {
                        keyPress = false;
                    }
                    break;
            }
        }

        private void moveTimer_Tick(object sender, EventArgs e)
        {
            if (keyPress)
                _tankController.MakeMovement(1);
        }
    }
}
