using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManualImageObjectSelector
{
    class ClassifierResult
    {
        public Dictionary<string, int> labels;
        public Dictionary<string, List<string>> observations;
        public ClassifierResult()
        {
            labels = new Dictionary<string, int>();
            observations = new Dictionary<string, List<string>>();
        }
    }
}
