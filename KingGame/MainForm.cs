using KingGame.Common;
using KingGame.Controller;
using KingGame.GUI;
using KingGame.GUI.GDI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KingGame
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitAll();
        }

        private void InitAll()
        {
            diamondMatrix = new DiamondMatrix();
            diamondMatrix.Radomize();
            InitGraphics();
            InitController();
            InitOther();
        }

        private void InitGraphics()
        {
            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            context.MaximumBuffer = this.pictureBox1.Size;
            buffGraphics = context.Allocate(this.pictureBox1.CreateGraphics(), new Rectangle(0, 0, this.pictureBox1.Width, this.pictureBox1.Height));
            guiFactory = new GuiFactoryGDI(buffGraphics);
            diamondScreen = guiFactory.CreateDiamondScreen();                                    
            diamondScreen.Load(diamondMatrix);
            textPanel = guiFactory.CreateTextPanel();
            
            
        }

        private void InitController()
        {
            connector = new Connector();
            mouseHandler = new MouseHandler();
            mouseHandler.AddObserver(connector);
            connector.AddObserver(diamondScreen);
            connector.AddObserver(textPanel);
            connector.Load(diamondMatrix);
        }

        private void InitOther()
        {
            textPanel.WriteScore(0);
            time = 60;
            textPanel.WriteTime(time);                 
        }

       

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            mouseHandler.OnClick(e.X, e.Y);                       
        }
       
        private void timer1_Tick(object sender, EventArgs e)
        {
            buffGraphics.Render();
            FirstTimeRenderTimer.Enabled = false;
        }

        private void GameOverTimer_Tick(object sender, EventArgs e)
        {
            time--;
            textPanel.WriteTime(time);
            if (time == 0)
            {
                GameOverTimer.Enabled = false;
                GameOverForm gameOverForm = new GameOverForm();
                gameOverForm.ShowDialog();
                InitAll();
                buffGraphics.Render();
                GameOverTimer.Enabled = true;
            }
        }

        private MouseHandler mouseHandler = null;        
        private BufferedGraphics buffGraphics = null;
        private IGuiFactory guiFactory = null;
        private IDiamondScreen diamondScreen = null;
        private DiamondMatrix diamondMatrix = null;
        private Connector connector = null;
        private ITextPanel textPanel = null;
        private int time;

        
    }
}
