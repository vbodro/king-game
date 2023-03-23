using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KingGame;
using KingGame.Common;

namespace KingGame.GUI.GDI
{
    /// <summary>
    /// A map of all bitmaps for diamond painting. Each index contains 3 images:
    /// regular look, selected look and a look prior to removal. GDI+ implementation.
    /// </summary>
    class DiamondMap
    {
        public DiamondMap()
        {
            map.Add(new ImgTriple(null, null, null));
            map.Add(new ImgTriple(new Bitmap(ImagesResource.Blue), new Bitmap(ImagesResource.BlueSelected), new Bitmap(ImagesResource.BlueRemoved)));
            map.Add(new ImgTriple(new Bitmap(ImagesResource.Green), new Bitmap(ImagesResource.GreenSelected), new Bitmap(ImagesResource.GreenRemoved)));
            map.Add(new ImgTriple(new Bitmap(ImagesResource.Purple), new Bitmap(ImagesResource.PurpleSelected), new Bitmap(ImagesResource.PurpleRemoved)));
            map.Add(new ImgTriple(new Bitmap(ImagesResource.Red), new Bitmap(ImagesResource.RedSelected), new Bitmap(ImagesResource.RedRemoved)));
            map.Add(new ImgTriple(new Bitmap(ImagesResource.Yellow), new Bitmap(ImagesResource.YellowSelected), new Bitmap(ImagesResource.YellowRemoved)));
        }

        public Bitmap Get(DiamondType type)
        {
            return map[(int)type].normal;
        }

        public Bitmap GetSelected(DiamondType type)
        {
            return map[(int)type].selected;
        }

        public Bitmap GetRemoved(DiamondType type)
        {
            return map[(int)type].removed;
        }
              
        private class ImgTriple
        {
            public ImgTriple(Bitmap normal, Bitmap selected, Bitmap removed)
            {
                this.normal = normal;
                this.selected = selected;
                this.removed = removed;
            }
            public Bitmap normal;
            public Bitmap selected;
            public Bitmap removed;
        }

        private IList<ImgTriple> map = new List<ImgTriple>();
    }
}
