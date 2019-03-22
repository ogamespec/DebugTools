using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace DebugTools
{
    public enum DataMode
    {
        NoData,
        Byte,
        Bytes2,
        Bytes4,
        Bytes8,
        Float,
        Double,
    }

    public enum DataDisplayMode
    {
        Hex,
        Signed,
        Unsigned,
    }

    public enum TextMode
    {
        NoText,
        Ansi,
        Unicode,
    }

    public class MemoryViewControl : Control
    {
        public MemoryViewControl()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            Cursor = Cursors.IBeam;
        }

        private BufferedGraphics gfx = null;            // для двойной буферизации при отрисовке
        private BufferedGraphicsContext context;

        // Свойства

        public int Columns { get; set; } = -1;
        public DataMode DataMode { get; set; }
        public DataDisplayMode DataDisplayMode { get; set; }
        public TextMode TextMode { get; set; }
        public UInt64 Address { get; set; }
        public Color AddressColor { get; set; }
        public Color DataColor { get; set; }
        public bool BigEndian { get; set; } = false;
        public Color ScrollbarColor { get; set; }
        public Color ScrollbarArrowColor { get; set; }
        public Color ScrollbarHoverArrowColor { get; set; }

        public byte[] Data { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (gfx == null)
            {
                ReallocateGraphics();
            }

            Draw(gfx.Graphics);

            gfx.Render(e.Graphics);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (gfx != null)
            {
                gfx.Dispose();
                ReallocateGraphics();
            }

            Invalidate();
            base.OnSizeChanged(e);
        }

        private void ReallocateGraphics()
        {
            context = BufferedGraphicsManager.Current;
            context.MaximumBuffer = new Size(Width + 1, Height + 1);

            gfx = context.Allocate(CreateGraphics(),
                 new Rectangle(0, 0, Width, Height));
        }

        private void Draw(Graphics gr)
        {
            gr.Clear(BackColor);

            if (Data == null)
                return;

            int columns = GetColumns();

            int bytesLeft = Data.Length - (int)ScrollingOffset;
            UInt64 offset = ScrollingOffset;
            int y = 0;
            int lineHeight = GetLineHeight(gr);
            int valueSize = GetDataValueSize();

            while (bytesLeft > valueSize)
            {
                int delta = Math.Min(columns, bytesLeft / valueSize);

                DrawDataLine(gr, offset, y, delta);

                bytesLeft -= delta * valueSize;
                offset += (UInt64)(delta * valueSize);
                y += lineHeight;

                if (y > Height)
                    break;
            }

            DrawScrollbar(gr);

            // Debug

#if false
            int addressWidth = GetAddressWidth();
            int dataWidth = GetDataWidth();
            int textWidth = GetTextWidth();

            gr.DrawLine(new Pen(Brushes.Aqua), new Point(0, 100), new Point(addressWidth, 100));
            gr.DrawLine(new Pen(Brushes.Red), new Point(addressWidth, 101), new Point(addressWidth + dataWidth, 101));
            gr.DrawLine(new Pen(Brushes.GreenYellow), new Point(addressWidth + dataWidth, 102), new Point(addressWidth + dataWidth + textWidth, 102));
#endif
        }

        private string GetValueText(UInt64 offset)
        {
            string text = "";
            byte valueByte;
            sbyte valueSByte;
            UInt16 valueUInt16;
            Int16 valueInt16;
            UInt32 valueUInt32;
            Int32 valueInt32;
            UInt64 valueUInt64;
            Int64 valueInt64;
            float valueFloat;
            double valueDouble;
            bool res;

            switch (DataMode)
            {
                case DataMode.Byte:
                    switch (DataDisplayMode)
                    {
                        case DataDisplayMode.Hex:
                            res = GetByte(offset, out valueByte);
                            if (res)
                                text = valueByte.ToString("X2").ToLower();
                            else
                                text = "??";
                            break;
                        case DataDisplayMode.Signed:
                            res = GetSByte(offset, out valueSByte);
                            if (res)
                                text = valueSByte.ToString().ToLower();
                            else
                                text = "??";
                            break;
                        case DataDisplayMode.Unsigned:
                            res = GetByte(offset, out valueByte);
                            if (res)
                                text = valueByte.ToString().ToLower();
                            else
                                text = "??";
                            break;
                    }
                    break;
                case DataMode.Bytes2:
                    switch (DataDisplayMode)
                    {
                        case DataDisplayMode.Hex:
                            res = GetUInt16(offset, out valueUInt16);
                            if (res)
                                text = valueUInt16.ToString("X4").ToLower();
                            else
                                text = "????";
                            break;
                        case DataDisplayMode.Signed:
                            res = GetInt16(offset, out valueInt16);
                            if (res)
                                text = valueInt16.ToString().ToLower();
                            else
                                text = "????";
                            break;
                        case DataDisplayMode.Unsigned:
                            res = GetUInt16(offset, out valueUInt16);
                            if (res)
                                text = valueUInt16.ToString().ToLower();
                            else
                                text = "????";
                            break;
                    }
                    break;
                case DataMode.Bytes4:
                    switch (DataDisplayMode)
                    {
                        case DataDisplayMode.Hex:
                            res = GetUInt32(offset, out valueUInt32);
                            if (res)
                                text = valueUInt32.ToString("X8").ToLower();
                            else
                                text = "????????";
                            break;
                        case DataDisplayMode.Signed:
                            res = GetInt32(offset, out valueInt32);
                            if (res)
                                text = valueInt32.ToString().ToLower();
                            else
                                text = "????????";
                            break;
                        case DataDisplayMode.Unsigned:
                            res = GetUInt32(offset, out valueUInt32);
                            if (res)
                                text = valueUInt32.ToString().ToLower();
                            else
                                text = "????????";
                            break;
                    }
                    break;
                case DataMode.Bytes8:
                    switch (DataDisplayMode)
                    {
                        case DataDisplayMode.Hex:
                            res = GetUInt64(offset, out valueUInt64);
                            if (res)
                                text = valueUInt64.ToString("X16").ToLower();
                            else
                                text = "????????????????";
                            break;
                        case DataDisplayMode.Signed:
                            res = GetInt64(offset, out valueInt64);
                            if (res)
                                text = valueInt64.ToString().ToLower();
                            else
                                text = "????????????????";
                            break;
                        case DataDisplayMode.Unsigned:
                            res = GetUInt64(offset, out valueUInt64);
                            if (res)
                                text = valueUInt64.ToString().ToLower();
                            else
                                text = "????????????????";
                            break;
                    }
                    break;
                case DataMode.Float:
                    res = GetUInt32(offset, out valueUInt32);
                    if (res)
                    {
                        valueFloat = BitConverter.ToSingle(BitConverter.GetBytes(valueUInt32), 0);
                        text = valueFloat.ToString().ToLower();
                    }
                    else
                        text = "???????????????";
                    break;
                case DataMode.Double:
                    res = GetUInt64(offset, out valueUInt64);
                    if (res)
                    {
                        valueDouble = BitConverter.ToDouble(BitConverter.GetBytes(valueUInt64), 0);
                        text = valueDouble.ToString().ToLower();
                    }
                    else
                        text = "????????????????????????";
                    break;
            }

            return text;
        }

        private string GetCharText(UInt64 offset)
        {
            string text = "";
            byte value;
            UInt16 valueUInt16;
            bool res;

            if (TextMode == TextMode.Ansi)
            {
                res = GetByte(offset, out value);
                if (res)
                {
                    if (value < 0x20 || value >= 127)
                    {
                        text = ".";
                    }
                    else
                    {
                        byte[] data = new byte[1] { value };
                        text = System.Text.ASCIIEncoding.ASCII.GetString(data);
                    }
                }
                else
                {
                    text = ".";
                }
            }
            else if (TextMode == TextMode.Unicode)
            {
                res = GetUInt16(offset, out valueUInt16);
                if (res)
                {
                    byte[] data = new byte[2] { (byte)(valueUInt16 & 0xff), (byte)(valueUInt16 >> 8) };
                    text = System.Text.ASCIIEncoding.Unicode.GetString(data);
                }
                else
                {
                    text = ".";
                }
            }

            return text;
        }

        private void DrawDataLine(Graphics gr, UInt64 offset, int y, int columns)
        {
            SizeF size;
            UInt64 address = Address + offset;
            string addressText = "0x" + address.ToString("X16");

            // Address

            Point point = new Point(0, y);
            gr.DrawString (addressText, this.Font, new SolidBrush(AddressColor), point);

            int addressWidth = GetAddressWidth();

            // Data

            point = new Point(addressWidth, y);

            int valueSize = GetDataValueSize();

            if (DataMode != DataMode.NoData)
            {
                for (int i = 0; i < columns; i++)
                {
                    string text = GetValueText(offset + (UInt64)(i * valueSize));
                    gr.DrawString(text, this.Font, new SolidBrush(DataColor), point);
                    point.X += GetDataTextSize();
                }
            }

            // Text

            point.X = GetAddressWidth() + GetDataWidth();

            if (TextMode != TextMode.NoText)
            {
                int charSize = GetCharSize();

                for (int i = 0; i < columns * valueSize; i += charSize)
                {
                    string text = GetCharText(offset + (UInt64)i);
                    gr.DrawString(text, this.Font, new SolidBrush(DataColor), point);
                    size = gr.MeasureString(text == " " ? "X" : text, this.Font);
                    point.X += (int)size.Width;
                }
            }

        }

        private bool GetByte (UInt64 offset, out byte value)
        {
            value = 0;
            if ( offset < (UInt64)Data.Length )
            {
                value = Data[offset];
                return true;
            }
            return false;
        }

        private bool GetSByte(UInt64 offset, out sbyte value)
        {
            value = 0;
            if (offset < (UInt64)Data.Length)
            {
                value = (sbyte)Data[offset];
                return true;
            }
            return false;
        }

        private bool GetUInt16(UInt64 offset, out UInt16 value)
        {
            value = 0;
            if (offset < (UInt64)(Data.Length - 1))
            {
                if (BigEndian)
                {
                    value = Data[offset + 1];
                    value |= (UInt16)(Data[offset + 0] << 8);
                }
                else
                {
                    value = Data[offset];
                    value |= (UInt16)(Data[offset + 1] << 8);
                }
                return true;
            }
            return false;
        }

        private bool GetInt16(UInt64 offset, out Int16 value)
        {
            UInt16 uint16Value;
            value = 0;
            bool res = GetUInt16(offset, out uint16Value);
            if (res)
            {
                value = BitConverter.ToInt16(BitConverter.GetBytes(uint16Value), 0);
            }
            return res;
        }

        private bool GetUInt32(UInt64 offset, out UInt32 value)
        {
            value = 0;
            if (offset < (UInt64)(Data.Length - 3))
            {
                if (BigEndian)
                {
                    value = Data[offset + 3];
                    value |= ((UInt32)Data[offset + 2] << 8);
                    value |= ((UInt32)Data[offset + 1] << 16);
                    value |= ((UInt32)Data[offset + 0] << 24);
                }
                else
                {
                    value = Data[offset];
                    value |= ((UInt32)Data[offset + 1] << 8);
                    value |= ((UInt32)Data[offset + 2] << 16);
                    value |= ((UInt32)Data[offset + 3] << 24);
                }
                return true;
            }
            return false;
        }

        private bool GetInt32(UInt64 offset, out Int32 value)
        {
            UInt32 uint32Value;
            value = 0;
            bool res = GetUInt32(offset, out uint32Value);
            if (res)
            {
                value = BitConverter.ToInt32(BitConverter.GetBytes(uint32Value), 0);
            }
            return res;
        }

        private bool GetUInt64(UInt64 offset, out UInt64 value)
        {
            value = 0;
            if (offset < (UInt64)(Data.Length - 7))
            {
                if (BigEndian)
                {
                    value = Data[offset + 7];
                    value |= ((UInt64)Data[offset + 6] << 8);
                    value |= ((UInt64)Data[offset + 5] << 16);
                    value |= ((UInt64)Data[offset + 4] << 24);
                    value |= ((UInt64)Data[offset + 3] << 32);
                    value |= ((UInt64)Data[offset + 2] << 40);
                    value |= ((UInt64)Data[offset + 1] << 48);
                    value |= ((UInt64)Data[offset + 0] << 56);
                }
                else
                {
                    value = Data[offset];
                    value |= ((UInt64)Data[offset + 1] << 8);
                    value |= ((UInt64)Data[offset + 2] << 16);
                    value |= ((UInt64)Data[offset + 3] << 24);
                    value |= ((UInt64)Data[offset + 4] << 32);
                    value |= ((UInt64)Data[offset + 5] << 40);
                    value |= ((UInt64)Data[offset + 6] << 48);
                    value |= ((UInt64)Data[offset + 7] << 56);
                }
                return true;
            }
            return false;
        }

        private bool GetInt64(UInt64 offset, out Int64 value)
        {
            UInt64 uint64Value;
            value = 0;
            bool res = GetUInt64(offset, out uint64Value);
            if (res)
            {
                value = BitConverter.ToInt64(BitConverter.GetBytes(uint64Value), 0);
            }
            return res;
        }

        private int GetLineHeight (Graphics gr)
        {
            SizeF size = gr.MeasureString("X", this.Font);
            return (int)size.Height;
        }

        private int GetAddressWidth ()
        {
            UInt64 maxAddress = 0xFFFFFFFFFFFFFFFF;
            string text = "0x" + maxAddress.ToString("X16") + "X";
            SizeF size = TextRenderer.MeasureText(text, this.Font);
            return (int)size.Width;
        }

        private int GetDataWidth ()
        {
            if (DataMode == DataMode.NoData)
                return 0;
            SizeF sizeChar = TextRenderer.MeasureText("X", this.Font);
            return GetColumns() * GetDataTextSize() + (int)sizeChar.Width;
        }

        private int GetDataTextSize ()
        {
            if (DataMode == DataMode.NoData)
                return 0;

            string text = "";

            switch (DataMode)
            {
                case DataMode.Byte:
                    switch (DataDisplayMode)
                    {
                        case DataDisplayMode.Hex:
                            text = "XX";
                            break;
                        case DataDisplayMode.Signed:
                            text = sbyte.MinValue.ToString();
                            break;
                        case DataDisplayMode.Unsigned:
                            text = byte.MaxValue.ToString();
                            break;
                    }
                    break;
                case DataMode.Bytes2:
                    switch (DataDisplayMode)
                    {
                        case DataDisplayMode.Hex:
                            text = "XXXX";
                            break;
                        case DataDisplayMode.Signed:
                            text = Int16.MinValue.ToString();
                            break;
                        case DataDisplayMode.Unsigned:
                            text = UInt16.MaxValue.ToString();
                            break;
                    }
                    break;
                case DataMode.Bytes4:
                    switch (DataDisplayMode)
                    {
                        case DataDisplayMode.Hex:
                            text = "XXXXXXXX";
                            break;
                        case DataDisplayMode.Signed:
                            text = Int32.MinValue.ToString();
                            break;
                        case DataDisplayMode.Unsigned:
                            text = UInt32.MaxValue.ToString();
                            break;
                    }
                    break;
                case DataMode.Bytes8:
                    switch (DataDisplayMode)
                    {
                        case DataDisplayMode.Hex:
                            text = "XXXXXXXXXXXXXXXX";
                            break;
                        case DataDisplayMode.Signed:
                            text = Int64.MinValue.ToString();
                            break;
                        case DataDisplayMode.Unsigned:
                            text = UInt64.MaxValue.ToString();
                            break;
                    }
                    break;
                case DataMode.Float:
                    text = "XXXXXXXXXXXXXXX";
                    break;
                case DataMode.Double:
                    text = "XXXXXXXXXXXXXXXXXXXXXXXX";
                    break;
            }

            SizeF size = TextRenderer.MeasureText(text, this.Font);
            return (int)size.Width;
        }

        private int GetDataValueSize()
        {
            switch (DataMode)
            {
                case DataMode.Byte:
                    return 1;
                case DataMode.Bytes2:
                    return 2;
                case DataMode.Bytes4:
                    return 4;
                case DataMode.Bytes8:
                    return 8;
                case DataMode.Float:
                    return 4;
                case DataMode.Double:
                    return 8;
            }

            // Если показ данных отключен, то вернем размер 1 байт для правильной работы отображения текста

            return 1;
        }

        private int GetCharSize()
        {
            switch (TextMode)
            {
                case TextMode.Ansi:
                    return 1;
                case TextMode.Unicode:
                    return 2;
            }
            return 0;
        }

        private int GetTextColumnWidth ()
        {
            if (TextMode == TextMode.NoText)
                return 0;

            int valueSize = GetDataValueSize();

            string text = "";

            for(int i=0; i<valueSize; i++)
            {
                text += "X";
            }

            Graphics gr = CreateGraphics();
            SizeF size = gr.MeasureString(text, this.Font);
            //SizeF size = TextRenderer.MeasureText(text, this.Font);
            return (int)size.Width;
        }

        private int GetTextWidth()
        {
            if (TextMode == TextMode.NoText)
                return 0;
            return GetColumns() * GetTextColumnWidth();
        }

        private int GetColumns()
        {
            if ( Columns < 0)
            {
                // По умолчанию расчитаем количество колонок

                int width = GetDataTextSize() + GetTextColumnWidth() + 1;

                int left = Width - ScrollbarWidth - GetAddressWidth() - GetTextColumnWidth();
                return left / width;
            }
            else
            {
                return Math.Min(Columns, 64);
            }
        }


        #region "Scrollbar"

        private int ScrollbarWidth = 16;
        private bool HoverUpArrow = false;
        private bool HoverDownArrow = false;
        private UInt64 ScrollingOffset;

        private Rectangle GetScrollbarRect()
        {
            Rectangle rect = new Rectangle();

            rect.X = Width - ScrollbarWidth;
            rect.Y = 0;
            rect.Width = ScrollbarWidth;
            rect.Height = Height;

            return rect;
        }

        private void DrawScrollbar(Graphics gr)
        {
            Rectangle rect = GetScrollbarRect();

            // Задний фон прокрутки

            gr.FillRectangle(new SolidBrush(ScrollbarColor), rect);

            // Стрелки прокрутки

            DrawScrollArrow(gr, new Point(Width - ScrollbarWidth / 2, ScrollbarWidth / 2), true);
            DrawScrollArrow(gr, new Point(Width - ScrollbarWidth / 2, Height - ScrollbarWidth / 2), false);
        }

        private void DrawScrollArrow (Graphics gr, Point point, bool up)
        {
            Point[] p = new Point[3];

            int triangleSize = ScrollbarWidth / 2;

            if (up)
            {
                p[0].X = point.X;
                p[0].Y = point.Y - triangleSize / 3;

                p[1].X = point.X - ScrollbarWidth / 3;
                p[1].Y = point.Y + triangleSize / 2;

                p[2].X = point.X + ScrollbarWidth / 3;
                p[2].Y = point.Y + triangleSize / 2;
            }
            else
            {
                p[0].X = point.X;
                p[0].Y = point.Y + triangleSize / 3;

                p[1].X = point.X - ScrollbarWidth / 3;
                p[1].Y = point.Y - triangleSize / 2;

                p[2].X = point.X + ScrollbarWidth / 3;
                p[2].Y = point.Y - triangleSize / 2;
            }

            bool hovered = up && HoverUpArrow || !up && HoverDownArrow;

            gr.FillPolygon(new SolidBrush(hovered ? ScrollbarHoverArrowColor : ScrollbarArrowColor), p);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            bool PrevHoverUp = HoverUpArrow;
            bool PrevHoverDown = HoverDownArrow;

            if (InsideScrollbar(new Point(e.X, e.Y)))
            {
                Cursor = Cursors.Default;

                if (e.Y < ScrollbarWidth)
                {
                    HoverUpArrow = true;
                    HoverDownArrow = false;
                }
                else if (e.Y >= Height - ScrollbarWidth)
                {
                    HoverUpArrow = false;
                    HoverDownArrow = true;
                }
                else
                {
                    HoverUpArrow = false;
                    HoverDownArrow = false;
                }
            }
            else
            {
                Cursor = Cursors.IBeam;

                HoverUpArrow = false;
                HoverDownArrow = false;
            }

            bool needRedraw = PrevHoverUp != HoverUpArrow || PrevHoverDown != HoverDownArrow;

            if (needRedraw)
            {
                Invalidate();
            }

            base.OnMouseMove(e);
        }

        private bool InsideScrollbar (Point point)
        {
            Rectangle rect = GetScrollbarRect();
            return rect.Contains(point);
        }

        private void ScrollUp()
        {
            UInt64 bytesPerLine = (UInt64)(GetColumns() * GetDataValueSize());
            UInt64 prev = ScrollingOffset;
            ScrollingOffset -= Math.Min(ScrollingOffset, bytesPerLine);
            if (prev != ScrollingOffset)
                Invalidate();
        }

        private void ScrollDown()
        {
            int bytesLeft = Data.Length - (int)ScrollingOffset;
            if (bytesLeft > GetColumns())
            {
                UInt64 bytesPerLine = (UInt64)(GetColumns() * GetDataValueSize());
                ScrollingOffset += bytesPerLine;
                Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Rectangle rect = GetScrollbarRect();
            if (rect.Contains(new Point(e.X, e.Y)))
            {
                if (e.Y < ScrollbarWidth)
                {
                    ScrollUp();
                }
                else if (e.Y >= Height - ScrollbarWidth)
                {
                    ScrollDown();
                }
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            HoverUpArrow = false;
            HoverDownArrow = false;

            Invalidate();

            base.OnMouseLeave(e);
        }

        private long lastMouseWheelTime = UnixTimeNow();

        private static long UnixTimeNow()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds * 1000 + (long)timeSpan.TotalMilliseconds;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            int delta = (int)(UnixTimeNow() - lastMouseWheelTime);
            bool fastScroll = delta < 100;

            if (e.Delta < 0)
            {
                if (fastScroll)
                {
                    ScrollDown();
                    ScrollDown();
                }

                ScrollDown();
            }
            else
            {
                if (fastScroll)
                {
                    ScrollUp();
                    ScrollUp();
                }

                ScrollUp();
            }

            lastMouseWheelTime = UnixTimeNow();
        }

        #endregion


    }
}
