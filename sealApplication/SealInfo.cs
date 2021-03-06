﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace sealApplication
{
    public partial class SealInfo : Form
    {

        public SealInfo()
        {
            InitializeComponent();
        }
        //全部是默认值
        private static bool IsRound = false;
        private static int pHeight = 200;
        private static int pWidth = 200;
        private static Point pBox = new Point(80, 100);//控件图像的中心点
        private static Color colorD = Color.Red;//印章的颜色
        private static int Star_W = 30;//红星的大小
        private static int tem_Line = 160;//记录圆的直径
        private static int circularity_W = 4;//设置圆画笔的粗细
        private static Font mainText = new Font("Arial", 12, FontStyle.Bold);//正文字体
        private static Font subText1 = new Font("Arial", 9, FontStyle.Bold);//符文1字体
        private static int move1 = 5;//符文1上移
        private static Font subText2 = new Font("Arial", 9, FontStyle.Bold);//符文2字体
        private static int move2 = 5;//符文2上移
        private static Rectangle rect = new Rectangle(circularity_W, circularity_W, tem_Line - circularity_W * 2, tem_Line - circularity_W * 2);//设置圆的绘制区域
        private static int _letterspace = 4;//字体间距
        private static Char_Direction _chardirect = Char_Direction.Center;//默认居中
        private static int _degree = 90;//字体圆弧所在圆
        private static int space = 16;//比外面圆圈小
        private static Rectangle NewRect = new Rectangle(new Point(rect.X + space, rect.Y + space), new Size(rect.Width - 2 * space, rect.Height - 2 * space));
        private void SealInfo_Load(object sender, EventArgs e)
        {
            try
            {
                int x = this.pictureBox1.Width / 2;
                int y = this.pictureBox1.Height / 2;
                pWidth = this.pictureBox1.Width;
                pHeight = this.pictureBox1.Height;
                pBox = new Point(x, y);
                _letterspace = (int)this.ud_word.Value;
                space = (int)this.ud_roundToWord.Value;
                if (this.cb_round2.Checked)
                {
                    IsRound = true;
                }
                this.rb_red.Checked = true;
                colorD = Color.Red;
                Star_W = (int)this.ud_ImageSize.Value;
                circularity_W = (int)this.ud_bOrW.Value;
                move1 = (int)this.ud_move1.Value;
                move2 = (int)this.ud_move2.Value;
                tem_Line = (int)this.ud_long.Value;
                rect = new Rectangle(pBox.X - tem_Line / 2, pBox.Y - tem_Line / 2, tem_Line, tem_Line);
                NewRect = new Rectangle(new Point(rect.X + space, rect.Y + space), new Size(rect.Width - 2 * space, rect.Height - 2 * space));
                //生成对应的印章图样
                this.pictureBox1.Image = CreatSeal(this.txb_mainInfo.Text, this.txb_sub1.Text, this.txb_sub2.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btn_set1_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = mainText;
            fd.ShowDialog();
            mainText = fd.Font;
            //生成对应的印章图样
            this.pictureBox1.Image = CreatSeal(this.txb_mainInfo.Text, this.txb_sub1.Text, this.txb_sub2.Text);

        }

        private void btn_set2_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = subText1;
            fd.ShowDialog();
            subText1 = fd.Font;
            //生成对应的印章图样
            this.pictureBox1.Image = CreatSeal(this.txb_mainInfo.Text, this.txb_sub1.Text, this.txb_sub2.Text);
        }

        private void btn_set3_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = subText2;
            fd.ShowDialog();
            subText2 = fd.Font;
            //生成对应的印章图样
            this.pictureBox1.Image = CreatSeal(this.txb_mainInfo.Text, this.txb_sub1.Text, this.txb_sub2.Text);
        }





        /// <summary>
        /// 创建公司公共印章得到gif图片存储地址
        /// </summary>
        /// <param name="company">公司名字</param>
        /// <param name="department">符文1</param>
        /// <param name="department2">符文2</param>
        /// <returns></returns>
        public static Bitmap CreatSeal(string company, string department, string department2)
        {
            try
            {
                string star_Str = "★";
                Bitmap bMap = new Bitmap(pWidth, pHeight);//画图初始化
                Graphics g = Graphics.FromImage(bMap);
                //Graphics g = this.panel1.CreateGraphics();//实例化Graphics类
                g.SmoothingMode = SmoothingMode.AntiAlias;//消除绘制图形的锯齿
                                                          //g.Clear(Color.White);//以白色清空panel1控件的背景
                Pen myPen = new Pen(colorD, circularity_W);//设置画笔的颜色
                g.DrawEllipse(myPen, rect); //绘制圆 

                Font star_Font = new Font("Arial", Star_W, FontStyle.Regular);//设置星号的字体样式
                SizeF star_Size = g.MeasureString(star_Str, star_Font);//对指定字符串进行测量
                                                                       //要指定的位置绘制星号
                PointF star_xy = new PointF(pBox.X - star_Size.Width / 2, pBox.Y - star_Size.Height / 2);
                g.DrawString(star_Str, star_Font, myPen.Brush, star_xy);


                //SolidBrush sb = new SolidBrush(Color.Green);
                //g.FillRectangle(sb, NewRect);


                //绘制中间文字(符文1)
                string var_txt = department;
                //string var_txt = "财务部";
                int var_len = var_txt.Length;
                Font Var_Font = subText1;//定义部门字体的字体样式
                SizeF Var_Size = g.MeasureString(var_txt, Var_Font);//对指定字符串进行测量
                                                                    //要指定的位置绘制中间文字
                PointF Var_xy = new PointF(pBox.X - Var_Size.Width / 2, pBox.Y + star_Size.Height / 2 - Var_Size.Height / 2 + move1);
                g.DrawString(var_txt, Var_Font, myPen.Brush, Var_xy);

                //（符文2）

                int var_len2 = department2.Length;
                Font Var_Font2 = subText2;//定义部门字体的字体样式
                if (!IsRound)
                {
                    SizeF Var_Size2 = g.MeasureString(department2, Var_Font2);//对指定字符串进行测量
                                                                              //要指定的位置绘制中间文字
                    PointF Var_xy2 = new PointF(pBox.X - Var_Size2.Width / 2, pBox.Y + star_Size.Height / 2 - Var_Size2.Height / 2 + move2);
                    g.DrawString(department2, Var_Font2, myPen.Brush, Var_xy2);
                }
                else
                {
                    roundText(g, department2, var_len2, Var_Font2, false);
                }

                //string text_txt = "吉林省明日科技有限公司";
                string text_txt = company;
                int text_len = text_txt.Length;//获取字符串的长度
                Font text_Font = mainText;//定义公司名字的字体的样式

                roundText(g, text_txt, text_len, text_Font, true);
                return bMap;
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 生成环绕的字体信息
        /// </summary>
        /// <param name="g">画布</param>
        /// <param name="text_txt">文本</param>
        /// <param name="text_len">文本长度</param>
        /// <param name="text_Font">文本字体</param>
        /// <param name="type">文本内容的主次</param>
        private static void roundText(Graphics g, string text_txt, int text_len, Font text_Font, bool type)
        {
            Pen myPenbush = new Pen(Color.White, circularity_W);
            float[] fCharWidth = new float[text_len];
            float fTotalWidth = ComputeStringLength(text_txt, g, fCharWidth, _letterspace, _chardirect, text_Font);
            // Compute arc's start-angle and end-angle
            double fStartAngle, fSweepAngle;
            fSweepAngle = fTotalWidth * 360 / (NewRect.Width * Math.PI);
            if (type)
            {
                fStartAngle = 270 - fSweepAngle / 2;
            }
            else
            {
                fStartAngle = 90 - fSweepAngle / 2;
            }
            // Compute every character's position and angle
            //PointF[] pntChars = new PointF[text_len];
            PointF[] pntChars = new PointF[text_len];
            double[] fCharAngle = new double[text_len];
            ComputeCharPos(fCharWidth, pntChars, fCharAngle, fStartAngle, type);
            int agle = 90;
            if (type)
            {
                agle = _degree;
            }
            else
            {
                agle = 270;
                double[] newCharAngle = new double[fCharAngle.Length];
                for (int j = 0; j < fCharAngle.Length; j++)
                {
                    newCharAngle[j] = fCharAngle[fCharAngle.Length - 1 - j];
                }
                fCharAngle = newCharAngle;
            }
            for (int i = 0; i < text_len; i++)
            {

                DrawRotatedText(g, text_txt[i].ToString(), (float)(fCharAngle[i] + agle), pntChars[i], text_Font, myPenbush);
            }
        }

        /// <summary>
        /// 计算字符串总长度和每个字符长度
        /// </summary>
        /// <param name="sText"></param>
        /// <param name="g"></param>
        /// <param name="fCharWidth"></param>
        /// <param name="fIntervalWidth"></param>
        /// <returns></returns>
        private static float ComputeStringLength(string sText, Graphics g, float[] fCharWidth, float fIntervalWidth, Char_Direction Direction, Font text_Font)
        {
            // Init字符串格式
            StringFormat sf = new StringFormat();
            sf.Trimming = StringTrimming.None;
            sf.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap
                | StringFormatFlags.LineLimit;
            // 衡量整个字符串长度
            SizeF size = g.MeasureString(sText, text_Font, (int)text_Font.Style);
            RectangleF rect = new RectangleF((pBox.X - tem_Line / 2), (pBox.Y - tem_Line / 2), size.Width, size.Height);
            // 测量每个字符大小
            CharacterRange[] crs = new CharacterRange[sText.Length];
            for (int i = 0; i < sText.Length; i++)
                crs[i] = new CharacterRange(i, 1);
            // 复位字符串格式
            sf.FormatFlags = StringFormatFlags.NoClip;
            sf.SetMeasurableCharacterRanges(crs);
            sf.Alignment = StringAlignment.Near;
            // 得到每一个字符大小
            Region[] regs = g.MeasureCharacterRanges(sText, text_Font, rect, sf);
            // Re-compute whole string length with space interval width
            float fTotalWidth = 0f;
            for (int i = 0; i < regs.Length; i++)
            {
                if (Direction == Char_Direction.Center || Direction == Char_Direction.OutSide)
                    fCharWidth[i] = regs[i].GetBounds(g).Width;
                else
                    fCharWidth[i] = regs[i].GetBounds(g).Height;
                fTotalWidth += fCharWidth[i] + fIntervalWidth;
            }
            fTotalWidth -= fIntervalWidth;//Remove the last interval width
            return fTotalWidth;
        }





        /// <summary>
        /// 求出每个字符的所在的点，以及相对于中心的角度
        ///1．  通过字符长度，求出字符所跨的弧度；
        ///2．  根据字符所跨的弧度，以及字符起始位置，算出字符的中心位置所对应的角度；
        ///3．  由于相对中心的角度已知，根据三角公式很容易算出字符所在弧上的点，如下图所示；
        ///4．  根据字符长度以及间隔距离，算出下一个字符的起始角度；
        ///5．  重复1直至整个字符串结束。
        /// </summary>
        /// <param name="CharWidth">字符的宽度</param>
        /// <param name="recChars">字符的坐标</param>
        /// <param name="CharAngle">字符角度</param>
        /// <param name="StartAngle">开始角度</param>
        private static void ComputeCharPos(float[] CharWidth, PointF[] recChars, double[] CharAngle, double StartAngle, bool type)
        {
            double fSweepAngle, fCircleLength;
            //Compute the circumference
            fCircleLength = NewRect.Width * Math.PI;

            for (int i = 0; i < CharWidth.Length; i++)
            {
                //Get char sweep angle
                fSweepAngle = CharWidth[i] * 360 / fCircleLength;

                //Set point angle
                CharAngle[i] = StartAngle + fSweepAngle / 2;

                //Get char position
                if (type)
                {
                    if (CharAngle[i] < 270f)
                        recChars[i] = new PointF(
                            NewRect.X + NewRect.Width / 2
                            - (float)(NewRect.Width / 2 *
                            Math.Sin(Math.Abs(CharAngle[i] - 270) * Math.PI / 180)),
                            NewRect.Y + NewRect.Width / 2
                            - (float)(NewRect.Width / 2 * Math.Cos(
                            Math.Abs(CharAngle[i] - 270) * Math.PI / 180)));
                    else
                        recChars[i] = new PointF(
                            NewRect.X + NewRect.Width / 2
                            + (float)(NewRect.Width / 2 *
                            Math.Sin(Math.Abs(CharAngle[i] - 270) * Math.PI / 180)),
                            NewRect.Y + NewRect.Width / 2
                            - (float)(NewRect.Width / 2 * Math.Cos(
                            Math.Abs(CharAngle[i] - 270) * Math.PI / 180)));
                }
                else
                {
                    float chary = NewRect.Y + NewRect.Width / 2 -
                        (float)(NewRect.Width / 2 * Math.Cos(Math.Abs(CharAngle[i] + 90) * Math.PI / 180));
                    recChars[i] = new PointF(
                        NewRect.X + NewRect.Width / 2
                        - (float)(NewRect.Width / 2 *
                        Math.Sin(Math.Abs(CharAngle[i] + 90) * Math.PI / 180)),
                        chary);
                }
                //Get total sweep angle with interval space
                fSweepAngle = (CharWidth[i] + _letterspace) * 360 / fCircleLength;
                StartAngle += fSweepAngle;
            }

        }
        /// <summary>
        /// 绘制每个字符
        /// </summary>
        /// <param name="g"></param>
        /// <param name="_text"></param>
        /// <param name="_angle"></param>
        /// <param name="text_Point"></param>
        /// <param name="text_Font"></param>
        /// <param name="myPen"></param>
        private static void DrawRotatedText(Graphics g, string _text, float _angle, PointF text_Point, Font text_Font, Pen myPen)
        {
            // Init format
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            // Create graphics path
            GraphicsPath gp = new GraphicsPath(System.Drawing.Drawing2D.FillMode.Winding);
            int x = (int)text_Point.X;
            int y = (int)text_Point.Y;

            // Add string
            gp.AddString(_text, text_Font.FontFamily, (int)text_Font.Style, text_Font.Size, new Point(x, y), sf);



            // Rotate string and draw it
            Matrix m = new Matrix();
            m.RotateAt(_angle, new PointF(x, y));
            g.Transform = m;
            g.DrawPath(myPen, gp);
            g.FillPath(new SolidBrush(colorD), gp);
        }

        public enum Char_Direction
        {
            Center = 0,
            OutSide = 1,
            ClockWise = 2,
            AntiClockWise = 3,
        }

        private void txb_mainInfo_TextChanged(object sender, EventArgs e)
        {
            //生成对应的印章图样
            this.pictureBox1.Image = CreatSeal(this.txb_mainInfo.Text, this.txb_sub1.Text, this.txb_sub2.Text);
        }

        private void txb_sub1_TextChanged(object sender, EventArgs e)
        {
            //生成对应的印章图样
            this.pictureBox1.Image = CreatSeal(this.txb_mainInfo.Text, this.txb_sub1.Text, this.txb_sub2.Text);
        }

        private void txb_sub2_TextChanged(object sender, EventArgs e)
        {
            //生成对应的印章图样
            this.pictureBox1.Image = CreatSeal(this.txb_mainInfo.Text, this.txb_sub1.Text, this.txb_sub2.Text);
        }

        private void ud_long_ValueChanged(object sender, EventArgs e)
        {
            tem_Line = (int)this.ud_long.Value;
            rect = new Rectangle(pBox.X - tem_Line / 2, pBox.Y - tem_Line / 2, tem_Line - 2 * circularity_W, tem_Line - 2 * circularity_W);
            NewRect = new Rectangle(new Point(rect.X + space, rect.Y + space), new Size(rect.Width - 2 * space, rect.Height - 2 * space));
            //生成对应的印章图样
            this.pictureBox1.Image = CreatSeal(this.txb_mainInfo.Text, this.txb_sub1.Text, this.txb_sub2.Text);
        }

        private void ud_bOrW_ValueChanged(object sender, EventArgs e)
        {
            circularity_W = (int)this.ud_bOrW.Value;
            //生成对应的印章图样
            this.pictureBox1.Image = CreatSeal(this.txb_mainInfo.Text, this.txb_sub1.Text, this.txb_sub2.Text);
        }

        private void rb_red_CheckedChanged(object sender, EventArgs e)
        {
            colorD = Color.Red;
            //生成对应的印章图样
            this.pictureBox1.Image = CreatSeal(this.txb_mainInfo.Text, this.txb_sub1.Text, this.txb_sub2.Text);
        }

        private void rb_blue_CheckedChanged(object sender, EventArgs e)
        {
            colorD = Color.Blue;
            //生成对应的印章图样
            this.pictureBox1.Image = CreatSeal(this.txb_mainInfo.Text, this.txb_sub1.Text, this.txb_sub2.Text);
        }

        private void rb_green_CheckedChanged(object sender, EventArgs e)
        {
            colorD = Color.Green;
            //生成对应的印章图样
            this.pictureBox1.Image = CreatSeal(this.txb_mainInfo.Text, this.txb_sub1.Text, this.txb_sub2.Text);
        }

        private void rb_purple_CheckedChanged(object sender, EventArgs e)
        {
            colorD = Color.Purple;
            //生成对应的印章图样
            this.pictureBox1.Image = CreatSeal(this.txb_mainInfo.Text, this.txb_sub1.Text, this.txb_sub2.Text);
        }

        private void ud_ImageSize_ValueChanged(object sender, EventArgs e)
        {
            Star_W = (int)this.ud_ImageSize.Value;
            //生成对应的印章图样
            this.pictureBox1.Image = CreatSeal(this.txb_mainInfo.Text, this.txb_sub1.Text, this.txb_sub2.Text);
        }

        private void cb_round2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cb_round2.Checked)
            {
                IsRound = true;
                this.ud_move2.Enabled = false;
            }
            else
            {
                IsRound = false;
                this.ud_move2.Enabled = true;
            }
            //生成对应的印章图样
            this.pictureBox1.Image = CreatSeal(this.txb_mainInfo.Text, this.txb_sub1.Text, this.txb_sub2.Text);
        }

        private void ud_word_ValueChanged(object sender, EventArgs e)
        {
            _letterspace = (int)this.ud_word.Value;
            //生成对应的印章图样
            this.pictureBox1.Image = CreatSeal(this.txb_mainInfo.Text, this.txb_sub1.Text, this.txb_sub2.Text);
        }

        private void ud_roundToWord_ValueChanged(object sender, EventArgs e)
        {
            space = (int)this.ud_roundToWord.Value;
            NewRect = new Rectangle(new Point(rect.X + space, rect.Y + space), new Size(rect.Width - 2 * space, rect.Height - 2 * space));
            //生成对应的印章图样
            this.pictureBox1.Image = CreatSeal(this.txb_mainInfo.Text, this.txb_sub1.Text, this.txb_sub2.Text);
        }

        private void ud_move1_ValueChanged(object sender, EventArgs e)
        {
            move1 = (int)this.ud_move1.Value;
            //生成对应的印章图样
            this.pictureBox1.Image = CreatSeal(this.txb_mainInfo.Text, this.txb_sub1.Text, this.txb_sub2.Text);
        }

        private void ud_move2_ValueChanged(object sender, EventArgs e)
        {
            move2 = (int)this.ud_move2.Value;
            //生成对应的印章图样
            this.pictureBox1.Image = CreatSeal(this.txb_mainInfo.Text, this.txb_sub1.Text, this.txb_sub2.Text);
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog open = new SaveFileDialog(); 
                open.ShowDialog();
                if (string.Empty!=open.FileName)
                {
                    string file = open.FileName;
                    Bitmap bmap = new Bitmap(this.pictureBox1.Image);
                    bmap.Save(file);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    //#region....网上查的生成印章的程序方法
    //public class CreatPublicSeal
    //{

    //Font Var_Font = new Font("Arial", 12, FontStyle.Bold);//定义字符串的字体样式
    //                                                      //Rectangle rect = new Rectangle(10, 10, 160, 160);//实例化Rectangle类
    //private static int tem_Line = 160;//记录圆的直径
    //private static int circularity_W = 4;//设置圆画笔的粗细
    //                                     //圆线条
    //private static Rectangle rect = new Rectangle(circularity_W, circularity_W, tem_Line - circularity_W * 2, tem_Line - circularity_W * 2);//设置圆的绘制区域
    //private static int _letterspace = 4;//字体间距
    //private static Char_Direction _chardirect = Char_Direction.Center;
    //private static int _degree = 90;
    ////字体圆弧所在圆
    //private static int space = 16;//比外面圆圈小
    //private static Rectangle NewRect = new Rectangle(new Point(rect.X + space, rect.Y + space), new Size(rect.Width - 2 * space, rect.Height - 2 * space));

    ///// <summary>
    ///// 创建公司公共印章得到gif图片存储地址
    ///// </summary>
    ///// <param name="company">公司名字</param>
    ///// <param name="department">部门名字</param>
    ///// <param name="Url">图片保存路径</param>
    ///// <returns></returns>
    //public static string CreatSeal(string company, string department, string Url)
    //{

    //    string star_Str = "★";
    //    Bitmap bMap = new Bitmap(160, 160);//画图初始化
    //    Graphics g = Graphics.FromImage(bMap);
    //    //Graphics g = this.panel1.CreateGraphics();//实例化Graphics类
    //    g.SmoothingMode = SmoothingMode.AntiAlias;//消除绘制图形的锯齿
    //    g.Clear(Color.White);//以白色清空panel1控件的背景
    //    Pen myPen = new Pen(Color.Red, circularity_W);//设置画笔的颜色
    //    g.DrawEllipse(myPen, rect); //绘制圆 

    //    Font star_Font = new Font("Arial", 30, FontStyle.Regular);//设置星号的字体样式
    //    SizeF star_Size = g.MeasureString(star_Str, star_Font);//对指定字符串进行测量
    //                                                           //要指定的位置绘制星号
    //    PointF star_xy = new PointF(tem_Line / 2 - star_Size.Width / 2, tem_Line / 2 - star_Size.Height / 2);
    //    g.DrawString(star_Str, star_Font, myPen.Brush, star_xy);

    //    //绘制中间文字
    //    string var_txt = department;
    //    //string var_txt = "财务部";
    //    int var_len = var_txt.Length;
    //    Font Var_Font = new Font("Arial", 22 - var_len * 2, FontStyle.Bold);//定义部门字体的字体样式
    //    SizeF Var_Size = g.MeasureString(var_txt, Var_Font);//对指定字符串进行测量
    //                                                        //要指定的位置绘制中间文字
    //    PointF Var_xy = new PointF(tem_Line / 2 - Var_Size.Width / 2, tem_Line / 2 + star_Size.Height / 2 - Var_Size.Height / 2 + 5);
    //    g.DrawString(var_txt, Var_Font, myPen.Brush, Var_xy);

    //    //string text_txt = "吉林省明日科技有限公司";
    //    string text_txt = company + "专用";
    //    int text_len = text_txt.Length;//获取字符串的长度
    //    Font text_Font = new Font("Arial", 25 - text_len, FontStyle.Bold);//定义公司名字的字体的样式
    //    Pen myPenbush = new Pen(Color.White, circularity_W);

    //    float[] fCharWidth = new float[text_len];
    //    float fTotalWidth = ComputeStringLength(text_txt, g, fCharWidth, _letterspace, _chardirect, text_Font);
    //    // Compute arc's start-angle and end-angle
    //    double fStartAngle, fSweepAngle;
    //    fSweepAngle = fTotalWidth * 360 / (NewRect.Width * Math.PI);
    //    fStartAngle = 270 - fSweepAngle / 2;
    //    // Compute every character's position and angle
    //    //PointF[] pntChars = new PointF[text_len];
    //    PointF[] pntChars = new PointF[text_len];
    //    double[] fCharAngle = new double[text_len];
    //    ComputeCharPos(fCharWidth, pntChars, fCharAngle, fStartAngle);
    //    for (int i = 0; i < text_len; i++)
    //    {
    //        DrawRotatedText(g, text_txt[i].ToString(), (float)(fCharAngle[i] + _degree), pntChars[i], text_Font, myPenbush);
    //    }
    //    string imageName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".gif";
    //    bMap.Save(Url + imageName);
    //    return Url + imageName;
    //}
    ///// <summary>
    ///// 计算字符串总长度和每个字符长度
    ///// </summary>
    ///// <param name="sText"></param>
    ///// <param name="g"></param>
    ///// <param name="fCharWidth"></param>
    ///// <param name="fIntervalWidth"></param>
    ///// <returns></returns>
    //private static float ComputeStringLength(string sText, Graphics g, float[] fCharWidth, float fIntervalWidth, Char_Direction Direction, Font text_Font)
    //{
    //    // Init字符串格式
    //    StringFormat sf = new StringFormat();
    //    sf.Trimming = StringTrimming.None;
    //    sf.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap
    //        | StringFormatFlags.LineLimit;
    //    // 衡量整个字符串长度
    //    SizeF size = g.MeasureString(sText, text_Font, (int)text_Font.Style);
    //    RectangleF rect = new RectangleF(0f, 0f, size.Width, size.Height);
    //    // 测量每个字符大小
    //    CharacterRange[] crs = new CharacterRange[sText.Length];
    //    for (int i = 0; i < sText.Length; i++)
    //        crs[i] = new CharacterRange(i, 1);
    //    // 复位字符串格式
    //    sf.FormatFlags = StringFormatFlags.NoClip;
    //    sf.SetMeasurableCharacterRanges(crs);
    //    sf.Alignment = StringAlignment.Near;
    //    // 得到每一个字符大小
    //    Region[] regs = g.MeasureCharacterRanges(sText, text_Font, rect, sf);
    //    // Re-compute whole string length with space interval width
    //    float fTotalWidth = 0f;
    //    for (int i = 0; i < regs.Length; i++)
    //    {
    //        if (Direction == Char_Direction.Center || Direction == Char_Direction.OutSide)
    //            fCharWidth[i] = regs[i].GetBounds(g).Width;
    //        else
    //            fCharWidth[i] = regs[i].GetBounds(g).Height;
    //        fTotalWidth += fCharWidth[i] + fIntervalWidth;
    //    }
    //    fTotalWidth -= fIntervalWidth;//Remove the last interval width
    //    return fTotalWidth;
    //}

    ///// <summary>
    ///// 求出每个字符的所在的点，以及相对于中心的角度
    /////1．  通过字符长度，求出字符所跨的弧度；
    /////2．  根据字符所跨的弧度，以及字符起始位置，算出字符的中心位置所对应的角度；
    /////3．  由于相对中心的角度已知，根据三角公式很容易算出字符所在弧上的点，如下图所示；
    /////4．  根据字符长度以及间隔距离，算出下一个字符的起始角度；
    /////5．  重复1直至整个字符串结束。
    ///// </summary>
    ///// <param name="CharWidth"></param>
    ///// <param name="recChars"></param>
    ///// <param name="CharAngle"></param>
    ///// <param name="StartAngle"></param>
    //private static void ComputeCharPos(float[] CharWidth, PointF[] recChars, double[] CharAngle, double StartAngle)
    //{
    //    double fSweepAngle, fCircleLength;
    //    //Compute the circumference
    //    fCircleLength = NewRect.Width * Math.PI;

    //    for (int i = 0; i < CharWidth.Length; i++)
    //    {
    //        //Get char sweep angle
    //        fSweepAngle = CharWidth[i] * 360 / fCircleLength;

    //        //Set point angle
    //        CharAngle[i] = StartAngle + fSweepAngle / 2;

    //        //Get char position
    //        if (CharAngle[i] < 270f)
    //            recChars[i] = new PointF(
    //                NewRect.X + NewRect.Width / 2
    //                - (float)(NewRect.Width / 2 *
    //                Math.Sin(Math.Abs(CharAngle[i] - 270) * Math.PI / 180)),
    //                NewRect.Y + NewRect.Width / 2
    //                - (float)(NewRect.Width / 2 * Math.Cos(
    //                Math.Abs(CharAngle[i] - 270) * Math.PI / 180)));
    //        else
    //            recChars[i] = new PointF(
    //                NewRect.X + NewRect.Width / 2
    //                + (float)(NewRect.Width / 2 *
    //                Math.Sin(Math.Abs(CharAngle[i] - 270) * Math.PI / 180)),
    //                NewRect.Y + NewRect.Width / 2
    //                - (float)(NewRect.Width / 2 * Math.Cos(
    //                Math.Abs(CharAngle[i] - 270) * Math.PI / 180)));

    //        //Get total sweep angle with interval space
    //        fSweepAngle = (CharWidth[i] + _letterspace) * 360 / fCircleLength;
    //        StartAngle += fSweepAngle;

    //    }
    //}
    ///// <summary>
    ///// 绘制每个字符
    ///// </summary>
    ///// <param name="g"></param>
    ///// <param name="_text"></param>
    ///// <param name="_angle"></param>
    ///// <param name="text_Point"></param>
    ///// <param name="text_Font"></param>
    ///// <param name="myPen"></param>
    //private static void DrawRotatedText(Graphics g, string _text, float _angle, PointF text_Point, Font text_Font, Pen myPen)
    //{
    //    // Init format
    //    StringFormat sf = new StringFormat();
    //    sf.Alignment = StringAlignment.Center;
    //    sf.LineAlignment = StringAlignment.Center;

    //    // Create graphics path
    //    GraphicsPath gp = new GraphicsPath(System.Drawing.Drawing2D.FillMode.Winding);
    //    int x = (int)text_Point.X;
    //    int y = (int)text_Point.Y;

    //    // Add string
    //    gp.AddString(_text, text_Font.FontFamily, (int)text_Font.Style, text_Font.Size, new Point(x, y), sf);

    //    // Rotate string and draw it
    //    Matrix m = new Matrix();
    //    m.RotateAt(_angle, new PointF(x, y));
    //    g.Transform = m;
    //    g.DrawPath(myPen, gp);
    //    g.FillPath(new SolidBrush(Color.Red), gp);
    //}

    //public enum Char_Direction
    //{
    //    Center = 0,
    //    OutSide = 1,
    //    ClockWise = 2,
    //    AntiClockWise = 3,
    //}
    //}
    //#endregion
}
