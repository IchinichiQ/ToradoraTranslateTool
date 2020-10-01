using System.Collections.Generic;
using System.Linq;

namespace OBJEditor
{
    public class OBJHelper
    {
        public Dictionary<int, string> actors;
        OBJ editor;
        public OBJHelper(byte[] script) { editor = new OBJ(script); }

        //Ryuuji「(Aaaaaaaaagh!!!!)」
        public string[] Import()
        {
            string[] strings = editor.Import();

            actors = new Dictionary<int, string>();
            for (int i = 0; i < strings.Length; i++)
            {
                string line = strings[i];
                actors[i] = null;
                if (line.EndsWith("」") && line.Contains('「'))
                {
                    string actor = line.Substring(0, line.IndexOf('「'));
                    line = line.Substring(actor.Length, line.Length - actor.Length);
                    actors[i] = actor;
                }

                strings[i] = line;
            }

            return strings;
        }

        public byte[] Export(string[] strings)
        {
            string[] tmp = new string[strings.Length];
            for (int i = 0; i < strings.Length; i++)
            {
                string Line = strings[i];
                if (actors[i] != null)
                    Line = actors[i] + Line;

                tmp[i] = Line;
            }

            return editor.Export(tmp);
        }
    }
}
