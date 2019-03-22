using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace DebugTools
{
    public class ColorTextControl : Control
    {
        private List<TextString> Strings = new List<TextString>();
        private List<Location> Locations = new List<Location>();

        private Location beginSelection = null;
        private Location endSelection = null;
        private bool selectionStarted = false;
        private int LineScroll = 0;

        // Свойства

        public Color SelectionColor { get; set; }
        public Color ScrollbarColor { get; set; }
        public Color ScrollbarArrowColor { get; set; }
        public Color ScrollbarHoverArrowColor { get; set; }

        public ColorTextControl()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            Cursor = Cursors.IBeam;
        }

        private BufferedGraphics gfx = null;            // для двойной буферизации при отрисовке
        private BufferedGraphicsContext context;

        public void AddString (string text, Color color)
        {
            TextString ts = new TextString();

            ts.text = text;
            ts.color = color;

            Strings.Add(ts);

            Console.WriteLine(Strings.Count.ToString() + ": " + ts.text);
        }

        public void ClearText ()
        {
            LineScroll = 0;
            Strings.Clear();
            Invalidate();
        }

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

            List<Location> tempLocations = new List<Location>();

            Point point = new Point(0, 0);

            bool startHighlight = false;
            int currentLine = 0;

            foreach (TextString ts in Strings)
            {
                if (point.Y > Height)
                    break;

                if (currentLine < LineScroll)
                {
                    currentLine++;
                    continue;
                }

                string text = "";

                Location beginSelectionTemp = beginSelection;
                Location endSelectionTemp = endSelection;

                if (beginSelectionTemp != null && endSelectionTemp != null)
                {
                    if (endSelectionTemp.rect.Y < beginSelectionTemp.rect.Y)
                    {
                        Location temp = endSelectionTemp;
                        endSelectionTemp = beginSelectionTemp;
                        beginSelectionTemp = temp;
                    }
                }

                if (beginSelectionTemp != null && endSelectionTemp != null)
                {
                    if (beginSelectionTemp.textString == ts)
                    {
                        startHighlight = true;
                    }
                }

                Location loca = new Location();

                loca.textString = ts;
                loca.rect = new Rectangle();

                loca.rect.X = point.X;
                loca.rect.Y = point.Y;

                SizeF size;

                for (int i=0; i<ts.text.Length; i++)
                {
                    if (ts.text[i] == '\n')
                    {
                        size = gr.MeasureString(text, this.Font);
                        loca.rect.Width = (int)size.Width;
                        loca.rect.Height = (int)size.Height;

                        if (startHighlight)
                        {
                            Rectangle fillRect = new Rectangle(0, point.Y, (int)size.Width, (int)size.Height);

                            if ( ts == beginSelectionTemp.textString && endSelectionTemp != null )
                            {
                                if (ts == endSelectionTemp.textString)
                                {
                                    if (beginSelectionTemp.offsetChars < endSelectionTemp.offsetChars)
                                    {
                                        fillRect.X = beginSelectionTemp.offsetPixels;
                                        fillRect.Width = endSelectionTemp.offsetPixels - beginSelectionTemp.offsetPixels +
                                            endSelectionTemp.characterWidth;
                                    }
                                    else
                                    {
                                        fillRect.X = endSelectionTemp.offsetPixels;
                                        fillRect.Width = beginSelectionTemp.offsetPixels - endSelectionTemp.offsetPixels;
                                    }
                                }
                                else
                                {
                                    fillRect.X = beginSelectionTemp.offsetPixels;
                                    fillRect.Width -= beginSelectionTemp.offsetPixels;
                                }
                            }
                            else if ( endSelectionTemp != null )
                            {
                                if (ts == endSelectionTemp.textString)
                                    fillRect.Width = endSelectionTemp.offsetPixels + endSelectionTemp.characterWidth;
                            }

                            //gr.DrawRectangle(new Pen(Color.Red), fillRect);
                            gr.FillRectangle(new SolidBrush(SelectionColor), fillRect);
                        }

                        point.X = 0;
                        gr.DrawString(text, this.Font, new SolidBrush(ts.color), point);

                        text = "";
                        point.Y += (int)size.Height;
                    }
                    else
                    {
                        text += ts.text[i];
                    }
                }

                //if (text.Length != 0)
                //{
                //    gr.DrawString(text, this.Font, new SolidBrush(ts.color), point);

                //    size = gr.MeasureString(text, this.Font);
                //    loca.rect.Width = (int)size.Width;
                //    loca.rect.Height = (int)size.Height;
                //}

                if (endSelectionTemp != null)
                {
                    if (endSelectionTemp.textString == ts)
                    {
                        startHighlight = false;
                    }
                }

                tempLocations.Add(loca);

                currentLine++;
            }

            Locations = tempLocations;

            DrawScrollbar(gr);

            //DumpLocations();
            //DrawLocations(gr);
        }

        private void DumpLocations ()
        {
            int i = 0;

            foreach (Location loca in Locations)
            {
                Console.WriteLine(i.ToString() + ": [{0}, {1}, {2}, {3}] {4}",
                    loca.rect.X,
                    loca.rect.Y,
                    loca.rect.Width,
                    loca.rect.Height,
                    loca.textString.text);

            }
        }

        private void DrawLocations(Graphics gr)
        {
            foreach (Location loca in Locations)
            {
                gr.DrawRectangle(new Pen(Color.AliceBlue), loca.rect);
            }
        }

        private Location GetLocation (Point point)
        {
            foreach (Location loca in Locations)
            {
                if (loca.rect.Contains(point))
                {
                    Graphics gr = CreateGraphics();

                    List<RectangleF> rects = MeasureCharacters(gr, this.Font, loca.textString.text);

                    int characterOffset = 0;
                    loca.offsetPixels = 0;

                    foreach (RectangleF rect in rects)
                    {
                        Rectangle screenRect = new Rectangle();

                        screenRect.X = (int)rect.X + loca.rect.X;
                        screenRect.Y = (int)rect.Y + loca.rect.Y;
                        screenRect.Width = (int)rect.Width;
                        screenRect.Height = (int)rect.Height;

                        if (screenRect.Contains(point))
                        {
                            loca.characterWidth = (int)rect.Width;
                            loca.offsetChars = characterOffset;
                            break;
                        }

                        loca.offsetPixels += (int)rect.Width;
                        characterOffset++;
                    }

                    return loca;
                }

            }

            return null;
        }

        // http://csharphelper.com/blog/2015/02/measure-character-positions-when-drawing-long-strings-in-c/

        // Measure the characters in the string.
        private List<RectangleF> MeasureCharacters(Graphics gr,
            Font font, string text)
        {
            List<RectangleF> results = new List<RectangleF>();

            // The X location for the next character.
            float x = 0;

            // Get the character sizes 31 characters at a time.
            for (int start = 0; start < text.Length; start += 32)
            {
                // Get the substring.
                int len = 32;
                if (start + len >= text.Length) len = text.Length - start;
                string substring = text.Substring(start, len);

                // Measure the characters.
                List<RectangleF> rects =
                    MeasureCharactersInWord(gr, font, substring);

                // Remove lead-in for the first character.
                if (start == 0) x += rects[0].Left;

                // Save all but the last rectangle.
                for (int i = 0; i < rects.Count + 1 - 1; i++)
                {
                    RectangleF new_rect = new RectangleF(
                        x, rects[i].Top,
                        rects[i].Width, rects[i].Height);
                    results.Add(new_rect);

                    // Move to the next character's X position.
                    x += rects[i].Width;
                }
            }

            // Return the results.
            return results;
        }

        // Measure the characters in a string with
        // no more than 32 characters.
        private List<RectangleF> MeasureCharactersInWord(
            Graphics gr, Font font, string text)
        {
            List<RectangleF> result = new List<RectangleF>();

            using (StringFormat string_format = new StringFormat())
            {
                string_format.Alignment = StringAlignment.Near;
                string_format.LineAlignment = StringAlignment.Near;
                string_format.Trimming = StringTrimming.None;
                string_format.FormatFlags =
                    StringFormatFlags.MeasureTrailingSpaces;

                CharacterRange[] ranges = new CharacterRange[text.Length];
                for (int i = 0; i < text.Length; i++)
                {
                    ranges[i] = new CharacterRange(i, 1);
                }
                string_format.SetMeasurableCharacterRanges(ranges);

                // Find the character ranges.
                RectangleF rect = new RectangleF(0, 0, 10000, 100);
                Region[] regions =
                    gr.MeasureCharacterRanges(
                        text, font, this.ClientRectangle,
                        string_format);

                // Convert the regions into rectangles.
                foreach (Region region in regions)
                    result.Add(region.GetBounds(gr));
            }

            return result;
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
            else
            {
                Location loca = GetLocation(new Point(e.X, e.Y));

                beginSelection = endSelection = null;

                if (beginSelection == null && loca != null)
                {
                    beginSelection = loca;
                    selectionStarted = true;
                }
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            selectionStarted = false;
            Invalidate();

            base.OnMouseUp(e);
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
                Location loca = GetLocation(new Point(e.X, e.Y));
                if (beginSelection != null && loca != null && selectionStarted)
                {
                    endSelection = loca;
                    Invalidate();
                }

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

        private void DrawScrollArrow(Graphics gr, Point point, bool up)
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

        private bool InsideScrollbar(Point point)
        {
            Rectangle rect = GetScrollbarRect();
            return rect.Contains(point);
        }

        private void ScrollUp()
        {
            LineScroll--;
            if (LineScroll < 0)
                LineScroll = 0;
            else
                Invalidate();
        }

        private void ScrollDown()
        {
            LineScroll++;
            Invalidate();
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

    internal class Location
    {
        public TextString textString;
        public int offsetChars;
        public int offsetPixels;
        public int characterWidth;
        public Rectangle rect;
    }

    public class TextString
    {
        public string text { get; set; }
        public Color color { get; set; }
    }

}
