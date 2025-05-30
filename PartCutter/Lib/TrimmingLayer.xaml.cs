using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PartCutter.Lib
{
    /// <summary>
    /// TrimmingLayer.xaml の相互作用ロジック
    /// </summary>
    public partial class TrimmingLayer : UserControl
    {
        public TrimmingLayer()
        {
            InitializeComponent();
        }

        enum DragLine
        {
            None,
            Top,
            Bottom,
            Left,
            Right,
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }

        private DragLine _dragLine = DragLine.None;

        private void TrimLayer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var point = e.GetPosition(TrimLayer);
            var trimming = Item.Trimming;

            if (point.X > trimming.ViewLeft && point.X < trimming.ViewRight && point.Y < trimming.ViewTop)
            {
                _dragLine = DragLine.Top;
                this.Cursor = Cursors.SizeNS;
            }
            else if (point.X > trimming.ViewLeft && point.X < trimming.ViewRight && point.Y > trimming.ViewBottom)
            {
                _dragLine = DragLine.Bottom;
                this.Cursor = Cursors.SizeNS;
            }
            else if (point.Y > trimming.ViewTop && point.Y < trimming.ViewBottom && point.X < trimming.ViewLeft)
            {
                _dragLine = DragLine.Left;
                this.Cursor = Cursors.SizeWE;
            }
            else if (point.Y > trimming.ViewTop && point.Y < trimming.ViewBottom && point.X > trimming.ViewRight)
            {
                _dragLine = DragLine.Right;
                this.Cursor = Cursors.SizeWE;
            }
            else if (point.X < trimming.ViewLeft && point.Y < trimming.ViewTop)
            {
                _dragLine = DragLine.TopLeft;
                this.Cursor = Cursors.SizeNWSE;
            }
            else if (point.X > trimming.ViewRight && point.Y < trimming.ViewTop)
            {
                _dragLine = DragLine.TopRight;
                this.Cursor = Cursors.SizeNESW;
            }
            else if (point.X < trimming.ViewLeft && point.Y > trimming.ViewBottom)
            {
                _dragLine = DragLine.BottomLeft;
                this.Cursor = Cursors.SizeNESW;
            }
            else if (point.X > trimming.ViewRight && point.Y > trimming.ViewBottom)
            {
                _dragLine = DragLine.BottomRight;
                this.Cursor = Cursors.SizeNWSE;
            }
            else
            {
                _dragLine = DragLine.None;
            }
        }

        private void TrimLayer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _dragLine = DragLine.None;
            this.Cursor = Cursors.Arrow;
        }

        private void TrimLayer_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var point = e.GetPosition(TrimLayer);
                double newLocationX = -1;
                double newLocationY = -1;
                switch (_dragLine)
                {
                    case DragLine.Top:
                        newLocationY = point.Y;
                        if (newLocationY >= Item.Trimming.ViewBottom)
                        {
                            newLocationY = Item.Trimming.ViewBottom;
                        }
                        else if (newLocationY < 1)
                        {
                            newLocationY = 0;
                        }
                        Item.Trimming.Top = (int)Math.Round(newLocationY / Item.Trimming.Scale);
                        break;
                    case DragLine.Bottom:
                        newLocationY = point.Y;
                        if (newLocationY <= Item.Trimming.ViewTop)
                        {
                            newLocationY = Item.Trimming.ViewTop;
                        }
                        else if (newLocationY > Item.MainImage.ActualHeight)
                        {
                            newLocationY = Item.MainImage.ActualHeight;
                        }
                        Item.Trimming.Bottom = (int)Math.Round(newLocationY / Item.Trimming.Scale);
                        break;
                    case DragLine.Left:
                        newLocationX = point.X;
                        if (newLocationX >= Item.Trimming.ViewRight)
                        {
                            newLocationX = Item.Trimming.ViewRight;
                        }
                        else if (newLocationX < 1)
                        {
                            newLocationX = 0;
                        }
                        Item.Trimming.Left = (int)Math.Round(newLocationX / Item.Trimming.Scale);
                        break;
                    case DragLine.Right:
                        newLocationX = point.X;
                        if (newLocationX <= Item.Trimming.ViewLeft)
                        {
                            newLocationX = Item.Trimming.ViewLeft;
                        }
                        else if (newLocationX > Item.MainImage.ActualWidth)
                        {
                            newLocationX = Item.MainImage.ActualWidth;
                        }
                        Item.Trimming.Right = (int)Math.Round(newLocationX / Item.Trimming.Scale);
                        break;
                    case DragLine.TopLeft:
                        newLocationX = point.X;
                        newLocationY = point.Y;
                        if (newLocationX >= Item.Trimming.ViewRight)
                        {
                            newLocationX = Item.Trimming.ViewRight;
                        }
                        else if (newLocationX < 1)
                        {
                            newLocationX = 0;
                        }
                        if (newLocationY >= Item.Trimming.ViewBottom)
                        {
                            newLocationY = Item.Trimming.ViewBottom;
                        }
                        else if (newLocationY < 1)
                        {
                            newLocationY = 0;
                        }
                        Item.Trimming.Left = (int)Math.Round(newLocationX / Item.Trimming.Scale);
                        Item.Trimming.Top = (int)Math.Round(newLocationY / Item.Trimming.Scale);
                        break;
                    case DragLine.TopRight:
                        newLocationX = point.X;
                        newLocationY = point.Y;
                        if (newLocationX <= Item.Trimming.ViewLeft)
                        {
                            newLocationX = Item.Trimming.ViewLeft;
                        }
                        else if (newLocationX > Item.MainImage.ActualWidth)
                        {
                            newLocationX = Item.MainImage.ActualWidth;
                        }
                        if (newLocationY >= Item.Trimming.ViewBottom)
                        {
                            newLocationY = Item.Trimming.ViewBottom;
                        }
                        else if (newLocationY < 1)
                        {
                            newLocationY = 0;
                        }
                        Item.Trimming.Right = (int)Math.Round(newLocationX / Item.Trimming.Scale);
                        Item.Trimming.Top = (int)Math.Round(newLocationY / Item.Trimming.Scale);
                        break;
                    case DragLine.BottomLeft:
                        newLocationX = point.X;
                        newLocationY = point.Y;
                        if (newLocationX >= Item.Trimming.ViewRight)
                        {
                            newLocationX = Item.Trimming.ViewRight;
                        }
                        else if (newLocationX < 1)
                        {
                            newLocationX = 0;
                        }
                        if (newLocationY <= Item.Trimming.ViewTop)
                        {
                            newLocationY = Item.Trimming.ViewTop;
                        }
                        else if (newLocationY > Item.MainImage.ActualHeight)
                        {
                            newLocationY = Item.MainImage.ActualHeight;
                        }
                        Item.Trimming.Left = (int)Math.Round(newLocationX / Item.Trimming.Scale);
                        Item.Trimming.Bottom = (int)Math.Round(newLocationY / Item.Trimming.Scale);
                        break;
                    case DragLine.BottomRight:
                        newLocationX = point.X;
                        newLocationY = point.Y;
                        if (newLocationX <= Item.Trimming.ViewLeft)
                        {
                            newLocationX = Item.Trimming.ViewLeft;
                        }
                        else if (newLocationX > Item.MainImage.ActualWidth)
                        {
                            newLocationX = Item.MainImage.ActualWidth;
                        }
                        if (newLocationY <= Item.Trimming.ViewTop)
                        {
                            newLocationY = Item.Trimming.ViewTop;
                        }
                        else if (newLocationY > Item.MainImage.ActualHeight)
                        {
                            newLocationY = Item.MainImage.ActualHeight;
                        }
                        Item.Trimming.Right = (int)Math.Round(newLocationX / Item.Trimming.Scale);
                        Item.Trimming.Bottom = (int)Math.Round(newLocationY / Item.Trimming.Scale);
                        break;

                }
            }
        }
    }
}