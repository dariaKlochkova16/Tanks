using DKFramework;
using SharpGL;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Tanks
{
    public partial class GameForm : Form
    {
        private const int _orhto = 52;

        private DateTime _pastUpdateTime;
        float deltaTimeF = 0;

        private DateTime _pastShoot;

        private int _frameCounter = 0;

        private string _nameOpenFile;

        private bool keyPress;

        TextureDescriptor[] TextureCard = new TextureDescriptor[] { };

        public GameForm()
        {
            InitializeComponent();

            InitCore();

            TextureCard = Core.Instance.GetComponent<ResoursMenager>().TextureCard;

            FPStimer.Interval = 1000;
            moveTimer.Start();
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
            Core.Instance.AddComponent<InputManager>();
            Core.Instance.AddComponent<WinManager>();
            Core.Instance.GetComponent<WinManager>().GameEnd += new EventHandler<string>(GameEnd);
        }

        private void openGLControl1_OpenGLDraw(object sender, RenderEventArgs e)
        {
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
            {
                NewGame(openFileDialog1.FileName);
            }

            CoreTimer.Start();
            _nameOpenFile = openFileDialog1.FileName;
            openGLControl1.Refresh();
        }

        private void openGLControl1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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

        private void SetRotation(Rotation rot)
        {
            Core.Instance.GetComponent<InputManager>().Rotate(rot);
            keyPress = true;
        }

        private void Shot()
        {
            TimeSpan deltaTime = DateTime.Now - _pastShoot;
            if (deltaTime.TotalMilliseconds > 1000)
            {
                Core.Instance.GetComponent<InputManager>().Shoot();
                _pastShoot = DateTime.Now;
                playSound("fire.wav");
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
            {
                Core.Instance.GetComponent<InputManager>().MakeMovement();
            }
        }
        private void GameEnd(object sender, string e)
        {
            CoreTimer.Stop();
            playSound("gameover.wav");
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            var result = MessageBox.Show(this, e + "Начать заново?", "Конец игры", buttons);

            if (result == DialogResult.Yes)
            {
                NewGame(_nameOpenFile);
                CoreTimer.Start();
            }

            if (result == DialogResult.No)
            {
                CoreTimer.Stop();
            }
        }

        private void NewGame(string nameOpenFile)
        {
            Core.Instance.ReCreate();
            InitCore();
            LevelFile.Open(nameOpenFile);
            playSound("gamestart.wav");
        }

        private void playSound(string path)
        {
            SoundPlayer player =
                new SoundPlayer();
            player.SoundLocation = path;
            player.Load();
            player.Play();
        }

    }
}
