using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptIn14Days.src;
using ScriptIn14Days.ASTrees;
using ScriptIn14Days.Environments;

namespace ScriptIn14Days
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //for invoke output
            staticForm = this;

            OriginalSize=LastSize = Size;
            controlRatio = new List<RectangleF>();
            
            for (int i = 0; i < Controls.Count; i++)
            {
                Point locationOnForm = Controls[i].FindForm().PointToClient(
    Controls[i].Parent.PointToScreen(Controls[i].Location));

                float startX = locationOnForm.X / (float)Size.Width;
                float startY = locationOnForm.Y / (float)Size.Height;
                float moveX = Controls[i].Width / (float)Size.Width;
                float moveY = Controls[i].Height / (float)Size.Height;

                controlRatio.Add(new RectangleF(startX, startY, moveX, moveY));   
            }

        }

        class RectangleF
        {
            public RectangleF(float x, float y, float width, float height)
            {
                X = x; Width = width;
                Y = y; Height = height;
            }
            public float X, Y, Width, Height;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            LineNumberReader reader = new LineNumberReader(richTextBox1.Text);

            string tmp=reader.ReadLine();
            while (tmp != "eof")
            {
                Output(reader.GetCurrentLine().ToString() + tmp);
                tmp=reader.ReadLine();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Lexer le = new Lexer(richTextBox1.Text);
            Token to=Token.EOF;
            to=le.read();
            while(to!=Token.EOF)
            {
                Output("Token: " + to.getText());
                to = le.read();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Lexer le = new Lexer(richTextBox1.Text);
            BasicParser bp = new BasicParser();

            int count = 10,i=0;
            while (le.peek(0) != Token.EOF && i++<count)
            {
                ASTree t = bp.Parse(le);
                //Console.WriteLine("########################");
                //Parser.PrintASTree(t);
                //Console.WriteLine("########################");
                Output(t == null ? "er" : t.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Lexer le = new Lexer(richTextBox1.Text);
            BasicParser bp = new BasicParser();

            //int count = 1, i = 0;
            Environments.Environment env = new BasicEnv();
            while (le.peek(0) != Token.EOF)// && i++<count)
            {
                try
                {
                    ASTree t = bp.Parse(le);
                    //Output(t.ToString());
                    //if (t == null)
                      //  throw new StoneException("Parse Error at line " + le.peek(0).getLineNumber());

                    Output(t.eval(env).ToString());
                }
                catch (StoneException ex)
                {
                    Output(ex.Message);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Output(ex.Message);
                }
                catch (Exception ex)
                {
                    Output(ex.Message);
                }
            }
        }

        public void Output(string msg)
        {
            richTextBox2.AppendText(msg + "\n");
            
            Console.WriteLine(msg);
        }

        static Form1 staticForm;
        public static void OutputStatic(string msg)
        {
            if (staticForm == null)
                return;

            staticForm.Output(msg);
        }

        Size LastSize,OriginalSize;
        List<RectangleF> controlRatio;
        private void Form1_Resize(object sender, EventArgs e)
        {
            float ratioX = Size.Width / (float)LastSize.Width;
            float ratioY = Size.Height / (float)LastSize.Height;

            for(int i=0;i<Controls.Count;i++)
            {
                Point newLocation=new Point(0,0);
                newLocation.X = (int)(Size.Width * controlRatio[i].X);
                newLocation.Y = (int)(Size.Height *controlRatio[i].Y);

                Controls[i].Location = newLocation;
                Controls[i].Width = (int)(Size.Width * (controlRatio[i].Width));
                Controls[i].Height = (int)(Size.Height * (controlRatio[i].Height));
            }
            LastSize = this.Size;
        }
    }
}
