using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManualImageObjectSelector
{
    class ImageRegionInfo
    {
        public string imgName; //local name only filename without path
        public bool isValidObs = true;
        public LinkedList<OrientedRect> regions;
        
        public void remove(int idx)
        {
            regions.Remove(regions.ElementAt(idx));
        }
        public int add(OrientedRect v)
        {
            regions.AddLast(v);
            return regions.Count - 1;
        }
    }
}
