using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sealApplication.UserControlShar
{
    [Serializable()]
    public partial class ImageComBoxUser : UserControl
    {
        public ImageComBoxUser()
        {
            InitializeComponent();
        }
        public Image selectedImage = null;
        private ImageList _smallImage = new ImageList();
        [Description("ImageList集合控件"), Browsable(true)]
        public ImageList smallImage
        {
            get
            {
                return _smallImage;
            }
            set
            {
                if (value != null)
                {
                    _smallImage = value;
                    this.panel1.Size= new Size(_smallImage.ImageSize.Width + this.btn_down.Width - 2, _smallImage.ImageSize.Height*_smallImage.Images.Count);
                    this.panel1.Location = new Point(0, _smallImage.ImageSize.Height);
                    for (int i = 0; i < _smallImage.Images.Count; i++)
                    {
                        if (i == 0)
                        {
                            this.pictureBox1.Size = _smallImage.ImageSize;
                            this.pictureBox1.Image = _smallImage.Images[i];
                            this.pictureBox1.Tag = i;
                            pictureBox1.Location = new Point(0, 0);
                        }else if (i == 1)
                        {
                            this.pictureBox2.Size = _smallImage.ImageSize;
                            this.pictureBox2.Image = _smallImage.Images[i];
                            this.pictureBox2.Tag = i;
                            pictureBox2.Location = new Point(0, _smallImage.Images[i].Height*1);
                        }
                        else if (i == 2)
                        {
                            this.pictureBox3.Size = _smallImage.ImageSize;
                            this.pictureBox3.Image = _smallImage.Images[i];
                            this.pictureBox3.Tag = i;
                            pictureBox3.Location = new Point(0, _smallImage.Images[i].Height*2);
                        }
                        else if (i == 3)
                        {
                            this.pictureBox4.Size = _smallImage.ImageSize;
                            this.pictureBox4.Image = _smallImage.Images[i];
                            this.pictureBox4.Tag = i;
                            pictureBox2.Location = new Point(0, _smallImage.Images[i].Height*3);
                        }
                        else if (i == 4)
                        {
                            this.pictureBox5.Size = _smallImage.ImageSize;
                            this.pictureBox5.Image = _smallImage.Images[i];
                            this.pictureBox5.Tag = i;
                            pictureBox5.Location = new Point(0, _smallImage.Images[i].Height*4);
                        }
                        else if (i == 5)
                        {
                            this.pictureBox6.Size = _smallImage.ImageSize;
                            this.pictureBox6.Image = _smallImage.Images[i];
                            this.pictureBox6.Tag = i;
                            pictureBox6.Location = new Point(0, _smallImage.Images[i].Height*5);
                        }
                    }
                    if (_smallImage.Images.Count == 1)
                    {
                        pictureBox2.Location = new Point(0,_smallImage.Images[0].Height);
                        pictureBox3.Location = new Point(0, _smallImage.Images[0].Height+20);
                        pictureBox4.Location = new Point(0, _smallImage.Images[0].Height+20*2);
                        pictureBox5.Location = new Point(0, _smallImage.Images[0].Height+20*3);
                        pictureBox6.Location = new Point(0, _smallImage.Images[0].Height+20*4);
                    }else if(_smallImage.Images.Count == 2)
                    {
                        pictureBox3.Location = new Point(0, _smallImage.Images[0].Height * 2);
                        pictureBox4.Location = new Point(0, _smallImage.Images[0].Height * 2 + 20 * 1);
                        pictureBox5.Location = new Point(0, _smallImage.Images[0].Height * 2 + 20 * 2);
                        pictureBox6.Location = new Point(0, _smallImage.Images[0].Height * 2 + 20 * 3);
                    }else if(_smallImage.Images.Count == 3)
                    {
                        pictureBox4.Location = new Point(0, _smallImage.Images[0].Height * 3);
                        pictureBox5.Location = new Point(0, _smallImage.Images[0].Height * 3 + 20 * 1);
                        pictureBox6.Location = new Point(0, _smallImage.Images[0].Height * 3 + 20 * 2);
                    }else if (_smallImage.Images.Count == 4)
                    {
                        pictureBox5.Location = new Point(0, _smallImage.Images[0].Height * 4);
                        pictureBox6.Location = new Point(0, _smallImage.Images[0].Height * 4 + 20);
                    }else if(_smallImage.Images.Count == 5)
                    {
                        pictureBox6.Location = new Point(0, _smallImage.Images[0].Height * 5);
                    }
                }
                else
                {
                    _smallImage = new ImageList();
                }
                this.Invalidate();
            }
        }
        private int _SelectedIndex;//DefaultValue(typeof(int),"-1")
        [Description("选择的索引号"), Browsable(false)]
        public int SelectedIndex
        {
            get
            {
                return _SelectedIndex;
            }
            set
            {
                if (_smallImage.Images.Count > 0)
                {
                    _SelectedIndex = value;
                    this.pictureComBox.Size = _smallImage.Images[_SelectedIndex].Size;
                    this.btn_down.Height = _smallImage.Images[_SelectedIndex].Height;
                    this.btn_down.Location = new Point(pictureComBox.Width - 2, pictureComBox.Location.Y);
                    this.Size = new Size(_smallImage.Images[_SelectedIndex].Width + this.btn_down.Width - 2, _smallImage.Images[_SelectedIndex].Height);
                    this.pictureComBox.Image = _smallImage.Images[_SelectedIndex];
                    selectedImage = _smallImage.Images[_SelectedIndex];
                    this.Invalidate();
                }
            }
        }

        private void btn_down_Click(object sender, EventArgs e)
        {
            this.Height = _smallImage.Images[0].Height * (_smallImage.Images.Count+1);
        }


        private void ImageComBoxUser_Load(object sender, EventArgs e)
        {
            if (_smallImage.Images.Count > 0 && _SelectedIndex >= 0)
            {
                this.pictureComBox.Size = _smallImage.Images[_SelectedIndex].Size;
                this.btn_down.Height = _smallImage.Images[_SelectedIndex].Height;
                this.btn_down.Location = new Point(pictureComBox.Width - 2, pictureComBox.Location.Y);
                this.Size = new Size(_smallImage.Images[_SelectedIndex].Width + this.btn_down.Width - 2, _smallImage.Images[_SelectedIndex].Height);
                this.pictureComBox.Image = _smallImage.Images[_SelectedIndex];
                selectedImage = _smallImage.Images[_SelectedIndex];
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox editor = (PictureBox)sender;
            SelectedIndex = Int32.Parse(editor.Tag.ToString());
            pictureComBox.Image = _smallImage.Images[SelectedIndex];
            this.pictureComBox.Size = _smallImage.Images[_SelectedIndex].Size;
            this.btn_down.Height = _smallImage.Images[_SelectedIndex].Height;
            this.btn_down.Location = new Point(pictureComBox.Width - 2, pictureComBox.Location.Y);
            this.Size = new Size(_smallImage.Images[_SelectedIndex].Width + this.btn_down.Width - 2, _smallImage.Images[_SelectedIndex].Height);
            this.pictureComBox.Image = _smallImage.Images[_SelectedIndex];
            selectedImage = _smallImage.Images[_SelectedIndex];
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            PictureBox editor = (PictureBox)sender;
            SelectedIndex = Int32.Parse(editor.Tag.ToString());
            pictureComBox.Image = _smallImage.Images[SelectedIndex];
            this.pictureComBox.Size = _smallImage.Images[_SelectedIndex].Size;
            this.btn_down.Height = _smallImage.Images[_SelectedIndex].Height;
            this.btn_down.Location = new Point(pictureComBox.Width - 2, pictureComBox.Location.Y);
            this.Size = new Size(_smallImage.Images[_SelectedIndex].Width + this.btn_down.Width - 2, _smallImage.Images[_SelectedIndex].Height);
            this.pictureComBox.Image = _smallImage.Images[_SelectedIndex];
            selectedImage = _smallImage.Images[_SelectedIndex];
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            PictureBox editor = (PictureBox)sender;
            SelectedIndex = Int32.Parse(editor.Tag.ToString());
            pictureComBox.Image = _smallImage.Images[SelectedIndex];
            this.pictureComBox.Size = _smallImage.Images[_SelectedIndex].Size;
            this.btn_down.Height = _smallImage.Images[_SelectedIndex].Height;
            this.btn_down.Location = new Point(pictureComBox.Width - 2, pictureComBox.Location.Y);
            this.Size = new Size(_smallImage.Images[_SelectedIndex].Width + this.btn_down.Width - 2, _smallImage.Images[_SelectedIndex].Height);
            this.pictureComBox.Image = _smallImage.Images[_SelectedIndex];
            selectedImage = _smallImage.Images[_SelectedIndex];
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            PictureBox editor = (PictureBox)sender;
            SelectedIndex = Int32.Parse(editor.Tag.ToString());
            pictureComBox.Image = _smallImage.Images[SelectedIndex];
            this.pictureComBox.Size = _smallImage.Images[_SelectedIndex].Size;
            this.btn_down.Height = _smallImage.Images[_SelectedIndex].Height;
            this.btn_down.Location = new Point(pictureComBox.Width - 2, pictureComBox.Location.Y);
            this.Size = new Size(_smallImage.Images[_SelectedIndex].Width + this.btn_down.Width - 2, _smallImage.Images[_SelectedIndex].Height);
            this.pictureComBox.Image = _smallImage.Images[_SelectedIndex];
            selectedImage = _smallImage.Images[_SelectedIndex];
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            PictureBox editor = (PictureBox)sender;
            SelectedIndex = Int32.Parse(editor.Tag.ToString());
            pictureComBox.Image = _smallImage.Images[SelectedIndex];
            this.pictureComBox.Size = _smallImage.Images[_SelectedIndex].Size;
            this.btn_down.Height = _smallImage.Images[_SelectedIndex].Height;
            this.btn_down.Location = new Point(pictureComBox.Width - 2, pictureComBox.Location.Y);
            this.Size = new Size(_smallImage.Images[_SelectedIndex].Width + this.btn_down.Width - 2, _smallImage.Images[_SelectedIndex].Height);
            this.pictureComBox.Image = _smallImage.Images[_SelectedIndex];
            selectedImage = _smallImage.Images[_SelectedIndex];
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            PictureBox editor = (PictureBox)sender;
            SelectedIndex = Int32.Parse(editor.Tag.ToString());
            pictureComBox.Image = _smallImage.Images[SelectedIndex];
            this.pictureComBox.Size = _smallImage.Images[_SelectedIndex].Size;
            this.btn_down.Height = _smallImage.Images[_SelectedIndex].Height;
            this.btn_down.Location = new Point(pictureComBox.Width - 2, pictureComBox.Location.Y);
            this.Size = new Size(_smallImage.Images[_SelectedIndex].Width + this.btn_down.Width - 2, _smallImage.Images[_SelectedIndex].Height);
            this.pictureComBox.Image = _smallImage.Images[_SelectedIndex];
            selectedImage = _smallImage.Images[_SelectedIndex];
        }
    }

}
